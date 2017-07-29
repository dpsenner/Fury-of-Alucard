# nearest.py - search tags fixed by a changeset
#
# Copyright 2008-2010 Gilles Moris <gilles.moris@free.fr>
#
# This software may be used and distributed according to the terms
# of the GNU General Public License, incorporated herein by reference.

"""find the first tags backward or forward in history from a given revision.

Multiple values correspond to tags on different braanches, named or not.
However, if a tag is an ancestor of a previously found, it is not displayed.
The found revisions are listed by closest dates.

The output format also gives the longuest path to the tag and the named branch
on which the tag has been found.
The longest path better matches the efforts between the revision and the tag.

The tags can be filtered with a match or regexp option.
The Shell pattern style match is prefered over regexps for the following reasons:
- easier to use for beginners
- the . (dot) characters does not convey any meaning and version tags contain a
  lot of those.
- search is rooted by default
- regexp probably overkills anyway: we're searching tags, not complex text
"""

import fnmatch, re
from mercurial import commands, extensions, util
from mercurial.node import nullrev
from mercurial.i18n import _

def nearest(ui, repo, rev=None, **opts):
    """find the first tags backward or forward in history from a given revision

    By default, tags are searched in both directions, except if overriden by a
    command option. If no revision is given, then the working directory
    revision is assumed.

    Tags are searched on all branches, ordered by dates. However, tags that are
    ancestors (resp. descendants) of the first tags found are not reported, i.e.
    there should be no merge between the tags and the searched root revision.
    It is still possible to override this behavior by selecting only tags on a
    named branch, or by filtering on the tag name. If the --first option is
    given, only the tag with the closest date is given anyway.

    Tags can be filtered by named branch, or using regexp or shell matching
    patterns. Case are ignored for pattern matching.
    """

    ctx = rev is not None and repo[rev] or repo['.']
    if ctx.rev() == nullrev:
        raise util.Abort('impossible to get nearest tags for nullrev')

    branches = frozenset(opts.get('branch', []))
    if opts.get('local', False):
        tagtypes = frozenset(['global', 'local'])
    else:
        tagtypes = frozenset(['global'])
    match = opts.get('match', '')
    regexp = opts.get('regexp', '')
    if regexp:
        regexp = re.compile(regexp, re.IGNORECASE)

    tags = filtertags(repo, ctx, tagtypes, branches, match, regexp)
    if tags:
        ui.write('on tag: %s @%d [%s]\n' % (':'.join(tags), ctx.rev(), ctx.branch()))
        return 0

    cache = {}
    if not opts.get('previous_tags', True) and not opts.get('next_tags', True):
        ptags = ntags = True
    else:
        ptags = opts.get('previous_tags', True)
        ntags = opts.get('next_tags', True)

    if ptags:
        ptags = nearesttags(repo, ctx.rev(), 'prev', tagtypes, branches, match, regexp, cache)
        for r, v in sorted(ptags.items(), key=lambda i: i[1], reverse=True):
            ui.write('previous tag: %s @%d (-%d [%s])\n' % (v[2], r, v[1], repo[r].branch()))
            if opts.get('first', False):
                break
    if ntags:
        ntags = nearesttags(repo, ctx.rev(), 'next', tagtypes, branches, match, regexp, cache)
        for r, v in sorted(ntags.items(), key=lambda i: i[1], reverse=True):
            ui.write('next tag: %s @%d (+%d [%s])\n' % (v[2], r, v[1], repo[r].branch()))
            if opts.get('first', False):
                break

    return 0

def filtertags(repo, ctx, tagtypes, branches, match, regexp):
    if not branches or ctx.branch() in branches:
        tags = ctx.tags()
        if not tags:
            return None
        tags = [t for t in ctx.tags() if repo.tagtype(t) in tagtypes]
        if match:
            tags = [t for t in tags if fnmatch.fnmatchcase(t, match)]
        if regexp:
            tags = [t for t in tags if regexp.search(t)]
    else:
        tags = None
    return tags

def nearesttags(repo, rev, dir, tagtypes, branches, match, regexp, cache):
    '''compute an array[rev] of the last/next tags tuple, each tuple being
    (date of the last tag, distance to that tag, the tag(s)).
    '''
    if dir != 'prev' and dir != 'next':
        raise util.Abort('internal error: argument should be prev or next')

    if dir == 'prev':
        ntmap = cache.setdefault('prevmap', {})
    elif dir == 'next':
        ntmap = cache.setdefault('nextmap', {})

    if rev in ntmap:
        return ntmap[rev]

    if dir == 'prev':
        infants = lambda r: [ p for p in repo.changelog.parentrevs(r) ]
        date = lambda c: c.date()[0]
        rg = lambda r, v: range(min(v), r + 1)
    elif dir == 'next':
        # ctx.children() is way too slow compared to ctx.parents(), may be
        # two orders of magnitude. So we build our own cache to replace it.
        if 'children' not in cache:
            chldcache = cache['children'] = [[]] * len(repo)
            chldrev = cache['chldrev'] = len(repo)
        else:
            chldcache = cache.get('children')
            chldrev = cache.get('childrev')
        for r in xrange(rev + 1, chldrev):
            for p in repo[r].parents():
                pr = p.rev()
                if pr == nullrev:
                    continue
                if not chldcache[pr]:
                    chldcache[pr] = [r]
                else:
                    chldcache[pr].append(r)
        cache['chldrev'] = rev + 1
        infants = lambda r: chldcache[r]
        date = lambda c: - c.date()[0]
        rg = lambda r, v: range(max(v), r - 1, -1)

    todo = [rev]
    deps = {}       # dependence at tag nodes
    visit = set()
    while todo:
        r = todo.pop()
        ctx = repo[r]
        tags = filtertags(repo, ctx, tagtypes, branches, match, regexp)

        if r in ntmap:
            continue
        visit.add(r)
        if tags:
            ntmap[r] = { r: (date(ctx), 0, ':'.join(sorted(tags)))}
            continue

        for pr in infants(r):
            if pr != nullrev and pr not in visit:
                todo.append(pr)

    for r in rg(rev, visit):
        ctx = repo[r]

        lp = []     # list of input parents
        for pr in infants(r):
            if pr in ntmap:
                lp.extend(ntmap[pr].items())
            elif pr != nullrev:
                lp = None
                break
        if lp is None:
            continue
        lp.sort(reverse=(dir == 'prev'))

        ld = set() # local dependences
        m = {}     # merged tag
        for k, v in lp:
            if k not in ld and (k not in m or v[1] + 1 > m[k][1]):
                m[k] = v[0], v[1] + 1, v[2]
                if k in deps:
                    ld.update(deps[k])

        if r not in ntmap:
            tags = filtertags(repo, ctx, tagtypes, branches, match, regexp)
            if tags:
                ntmap[r] = { r: (date(ctx), 0, ':'.join(sorted(tags)))}

        if r in ntmap:
            ld.update(m.keys())
            deps[r] = ld
        else:
            ntmap[r] = m

    return ntmap[rev]

def summary(orig, ui, repo, *args, **kwargs):
    r = orig(ui, repo, *args, **kwargs)
    nearest(ui, repo, local=True)
    return r

def uisetup(ui):
    extensions.wrapcommand(commands.table, 'summary', summary)


cmdtable = {
'nearest':
        (nearest,
        [
        ('p', 'previous-tags', False, _('search the lastest tags')),
        ('n', 'next-tags', False, _('search the oldest tags')),
        ('1', 'first', False, _('display only the first tag found')),
        ('b', 'branch', [], _('restrict search to branch name (can be repeated)')),
        ('l', 'local', False, _('search also for the local tags')),
        ('',  'match', '', _('search for the following shell pattern match'
                             ' of the tag')),
        ('',  'regexp','', _('search for the following regular expression'))
        ],
        _('hg nearest [-pn1l] [-b branch] [REV]')),
}

