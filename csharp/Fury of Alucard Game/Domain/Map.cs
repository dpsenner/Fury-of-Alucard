using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fury_of_Alucard.Domain.Locations;
using Fury_of_Alucard.Domain.Paths;
using Fury_of_Alucard.Common;

namespace Fury_of_Alucard.Domain
{
	public class Map
	{
		#region locations

		#region cities
		#region east
		public readonly CastleDracula CastleDracula = TypeFactory.CreateAutoNotifierInstance<CastleDracula>();
		public readonly SmallCity Galatz = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Galatz", LocationToken.EAST);
		public readonly SmallCity Klausenburg = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Klausenburg", LocationToken.EAST);
		public readonly BigCity Budapest = TypeFactory.CreateAutoNotifierInstance<BigCity>("Budapest", LocationToken.EAST);
		public readonly BigCity Vienna = TypeFactory.CreateAutoNotifierInstance<BigCity>("Vienna", LocationToken.EAST);
		public readonly SmallCity Zagreb = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Zagreb", LocationToken.EAST);
		public readonly SmallCity Szeged = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Szeged", LocationToken.EAST);
		public readonly StJosephStMary StJosephStMary = TypeFactory.CreateAutoNotifierInstance<StJosephStMary>();
		public readonly SmallCity Belgrade = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Belgrade", LocationToken.EAST);
		public readonly SmallCity Sarajevo = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Sarajevo", LocationToken.EAST);
		public readonly BigCity Bucharest = TypeFactory.CreateAutoNotifierInstance<BigCity>("Bucharest", LocationToken.EAST);
		public readonly SmallCity Sofia = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Sofia", LocationToken.EAST);
		public readonly SmallCity Valona = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Valona", LocationToken.EAST);
		public readonly SmallCity Salonica = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Salonica", LocationToken.EAST);
		public readonly BigCity Athens = TypeFactory.CreateAutoNotifierInstance<BigCity>("Athens", LocationToken.EAST);
		public readonly BigCity Varna = TypeFactory.CreateAutoNotifierInstance<BigCity>("Varna", LocationToken.EAST);
		public readonly BigCity Constanta = TypeFactory.CreateAutoNotifierInstance<BigCity>("Constanta", LocationToken.EAST);
		public readonly BigCity Prague = TypeFactory.CreateAutoNotifierInstance<BigCity>("Prague", LocationToken.EAST);
		public readonly SmallCity Bari = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Bari", LocationToken.EAST);
		public readonly SmallCity Cagliari = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Cagliari", LocationToken.EAST);
		public readonly SmallCity Florence = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Florence", LocationToken.EAST);
		public readonly SmallCity Venice = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Venice", LocationToken.EAST);
		public readonly BigCity Naples = TypeFactory.CreateAutoNotifierInstance<BigCity>("Naples", LocationToken.EAST);
		public readonly BigCity Rome = TypeFactory.CreateAutoNotifierInstance<BigCity>("Rome", LocationToken.EAST);
		public readonly BigCity Genoa = TypeFactory.CreateAutoNotifierInstance<BigCity>("Genoa", LocationToken.EAST);
		public readonly BigCity Milan = TypeFactory.CreateAutoNotifierInstance<BigCity>("Milan", LocationToken.EAST);
		#endregion
		#region west
		public readonly BigCity Munich = TypeFactory.CreateAutoNotifierInstance<BigCity>("Munich", LocationToken.WEST);
		public readonly BigCity Leipzig = TypeFactory.CreateAutoNotifierInstance<BigCity>("Leipzig", LocationToken.WEST);
		public readonly BigCity Berlin = TypeFactory.CreateAutoNotifierInstance<BigCity>("Berlin", LocationToken.WEST);
		public readonly BigCity Hamburg = TypeFactory.CreateAutoNotifierInstance<BigCity>("Hamburg", LocationToken.WEST);
		public readonly BigCity Amsterdam = TypeFactory.CreateAutoNotifierInstance<BigCity>("Amsterdam", LocationToken.WEST);
		public readonly BigCity Cologne = TypeFactory.CreateAutoNotifierInstance<BigCity>("Cologne", LocationToken.WEST);
		public readonly BigCity Brussels = TypeFactory.CreateAutoNotifierInstance<BigCity>("Brussels", LocationToken.WEST);
		public readonly BigCity Paris = TypeFactory.CreateAutoNotifierInstance<BigCity>("Paris", LocationToken.WEST);
		public readonly BigCity London = TypeFactory.CreateAutoNotifierInstance<BigCity>("London", LocationToken.WEST);
		public readonly BigCity Manchester = TypeFactory.CreateAutoNotifierInstance<BigCity>("Manchester", LocationToken.WEST);
		public readonly BigCity Liverpool = TypeFactory.CreateAutoNotifierInstance<BigCity>("Liverpool", LocationToken.WEST);
		public readonly BigCity Edinburgh = TypeFactory.CreateAutoNotifierInstance<BigCity>("Edinburgh", LocationToken.WEST);
		public readonly BigCity Nantes = TypeFactory.CreateAutoNotifierInstance<BigCity>("Nantes", LocationToken.WEST);
		public readonly BigCity Bordeaux = TypeFactory.CreateAutoNotifierInstance<BigCity>("Bordeaux", LocationToken.WEST);
		public readonly BigCity Marseilles = TypeFactory.CreateAutoNotifierInstance<BigCity>("Marseilles", LocationToken.WEST);
		public readonly BigCity Barcelona = TypeFactory.CreateAutoNotifierInstance<BigCity>("Barcelona", LocationToken.WEST);
		public readonly BigCity Madrid = TypeFactory.CreateAutoNotifierInstance<BigCity>("Madrid", LocationToken.WEST);
		public readonly BigCity Lisbon = TypeFactory.CreateAutoNotifierInstance<BigCity>("Lisbon", LocationToken.WEST);
		public readonly BigCity Cadiz = TypeFactory.CreateAutoNotifierInstance<BigCity>("Cadiz", LocationToken.WEST);
		public readonly SmallCity Nuremburg = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Nuremburg", LocationToken.WEST);
		public readonly SmallCity Frankfurt = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Frankfurt", LocationToken.WEST);
		public readonly SmallCity Galway = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Galway", LocationToken.WEST);
		public readonly SmallCity Zurich = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Zurich", LocationToken.WEST);
		public readonly SmallCity Geneva = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Geneva", LocationToken.WEST);
		public readonly SmallCity Strasbourg = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Strasbourg", LocationToken.WEST);
		public readonly SmallCity ClermontFerrand = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Clermont-Ferrand", LocationToken.WEST);
		public readonly SmallCity LeHavre = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Le Havre", LocationToken.WEST);
		public readonly SmallCity Toulouse = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Toulouse", LocationToken.WEST);
		public readonly SmallCity Swansea = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Swansea", LocationToken.WEST);
		public readonly SmallCity Plymouth = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Plymouth", LocationToken.WEST);
		public readonly SmallCity Dublin = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Dublin", LocationToken.WEST);
		public readonly SmallCity Saragossa = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Saragossa", LocationToken.WEST);
		public readonly SmallCity Santander = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Santander", LocationToken.WEST);
		public readonly SmallCity Alicante = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Alicante", LocationToken.WEST);
		public readonly SmallCity Granada = TypeFactory.CreateAutoNotifierInstance<SmallCity>("Granada", LocationToken.WEST);
		#endregion
		#endregion

		#region seas
		public readonly ALocation NorthSea = TypeFactory.CreateAutoNotifierInstance<Sea>("North Sea");
		public readonly ALocation EnglishChannel = TypeFactory.CreateAutoNotifierInstance<Sea>("English Channel");
		public readonly ALocation IrishSea = TypeFactory.CreateAutoNotifierInstance<Sea>("Irish Sea");
		public readonly ALocation AtlanticOcean = TypeFactory.CreateAutoNotifierInstance<Sea>("Atlantic Ocean");
		public readonly ALocation BayOfBiscay = TypeFactory.CreateAutoNotifierInstance<Sea>("Bay of Biscay");
		public readonly ALocation MediterraneanSea = TypeFactory.CreateAutoNotifierInstance<Sea>("Mediterranean Sea");
		public readonly ALocation TyrrhenianSea = TypeFactory.CreateAutoNotifierInstance<Sea>("Tyrrhenian Sea");
		public readonly ALocation IonianSea = TypeFactory.CreateAutoNotifierInstance<Sea>("Ionian Sea");
		public readonly ALocation AdriaticSea = TypeFactory.CreateAutoNotifierInstance<Sea>("Adriatic Sea");
		public readonly ALocation BlackSea = TypeFactory.CreateAutoNotifierInstance<Sea>("Black Sea");
		#endregion

		public List<ALocation> Locations { get; private set; }
		#endregion

		#region streets
		public List<APath> Streets { get; private set;}
		#endregion

		public Map()
		{
			Locations = new List<ALocation>();
			#region east
			Locations.Add(Sarajevo);
			Locations.Add(CastleDracula);
			Locations.Add(Galatz);
			Locations.Add(Klausenburg);
			Locations.Add(Constanta);
			Locations.Add(Szeged);
			Locations.Add(Budapest);
			Locations.Add(Bucharest);
			Locations.Add(Belgrade);
			Locations.Add(Varna);
			Locations.Add(Valona);
			Locations.Add(Athens);
			Locations.Add(Salonica);
			Locations.Add(StJosephStMary);
			Locations.Add(Vienna);
			Locations.Add(Zagreb);
			Locations.Add(Sofia);
			Locations.Add(Munich);
			Locations.Add(Prague);
			Locations.Add(Bari);
			Locations.Add(Naples);
			Locations.Add(Rome);
			Locations.Add(Florence);
			Locations.Add(Venice);
			Locations.Add(Genoa);
			Locations.Add(Milan);
			Locations.Add(Cagliari);
			#endregion

			#region west
			// big cities
			Locations.AddRange(new ALocation[] { Munich, Leipzig, Berlin, Hamburg, Amsterdam, Cologne, Brussels, Paris, London, Manchester, Liverpool, Edinburgh, Nantes, Bordeaux, Marseilles, Barcelona, Madrid, Lisbon, Cadiz });
			Locations.AddRange(new ALocation[] { Nuremburg, Frankfurt, Zurich, Geneva, Strasbourg, ClermontFerrand, LeHavre, Toulouse, Swansea, Plymouth, Dublin, Galway, Saragossa, Santander, Alicante, Granada });
			#endregion

			#region seas
			Locations.Add(BlackSea);
			Locations.Add(IonianSea);
			Locations.Add(NorthSea);
			Locations.Add(EnglishChannel);
			Locations.Add(IrishSea);
			Locations.Add(AtlanticOcean);
			Locations.Add(BayOfBiscay);
			Locations.Add(MediterraneanSea);
			Locations.Add(TyrrhenianSea);
			Locations.Add(AdriaticSea);
			#endregion

			// populate streets / habors
			Streets = new List<APath>();
			Streets.Add(new Street(CastleDracula, Klausenburg));
			Streets.Add(new Street(CastleDracula, Galatz));

			Streets.Add(new Street(Galatz, CastleDracula));
			Streets.Add(new Street(Galatz, Constanta));
			Streets.Add(new Street(Galatz, Bucharest));
			Streets.Add(new Street(Galatz, Klausenburg));

			Streets.Add(new Street(Klausenburg, CastleDracula));
			Streets.Add(new Street(Klausenburg, Galatz));
			Streets.Add(new Street(Klausenburg, Bucharest));
			Streets.Add(new Street(Klausenburg, Belgrade));
			Streets.Add(new Street(Klausenburg, Szeged));
			Streets.Add(new Street(Klausenburg, Budapest));

			Streets.Add(new Port(Constanta, BlackSea));
			Streets.Add(new Street(Constanta, Galatz));
			Streets.Add(new Street(Constanta, Bucharest));
			Streets.Add(new EasternRail(Constanta, Bucharest));
			Streets.Add(new Street(Constanta, Varna));

			Streets.Add(new Street(Varna, Constanta));
			Streets.Add(new Port(Varna, BlackSea));
			Streets.Add(new Street(Varna, Sofia));
			Streets.Add(new EasternRail(Varna, Sofia));

			Streets.Add(new Street(Sofia, Bucharest));
			Streets.Add(new Street(Sofia, Varna));
			Streets.Add(new EasternRail(Sofia, Varna));
			Streets.Add(new Street(Sofia, Salonica));
			Streets.Add(new EasternRail(Sofia, Salonica));
			Streets.Add(new Street(Sofia, Valona));
			Streets.Add(new Street(Sofia, Sarajevo));
			Streets.Add(new Street(Sofia, Belgrade));
			Streets.Add(new EasternRail(Sofia, Belgrade));

			Streets.Add(new Street(Salonica, Sofia));
			Streets.Add(new EasternRail(Salonica, Sofia));
			Streets.Add(new Street(Salonica, Valona));
			Streets.Add(new Port(Salonica, IonianSea));

			Streets.Add(new Street(Valona, Athens));
			Streets.Add(new Street(Valona, Salonica));
			Streets.Add(new Street(Valona, Sofia));
			Streets.Add(new Street(Valona, Sarajevo));
			Streets.Add(new Port(Valona, IonianSea));

			Streets.Add(new Street(Athens, Valona));
			Streets.Add(new Street(Athens, Salonica));
			Streets.Add(new Port(Athens, IonianSea));

			Streets.Add(new Street(Sarajevo, Sofia));
			Streets.Add(new Street(Sarajevo, Valona));
			Streets.Add(new Street(Sarajevo, Zagreb));
			Streets.Add(new Street(Sarajevo, StJosephStMary));
			Streets.Add(new Street(Sarajevo, Belgrade));

			Streets.Add(new Street(Belgrade, Bucharest));
			Streets.Add(new Street(Belgrade, Sofia));
			Streets.Add(new EasternRail(Belgrade, Sofia));
			Streets.Add(new Street(Belgrade, Sarajevo));
			Streets.Add(new Street(Belgrade, StJosephStMary));
			Streets.Add(new Street(Belgrade, Szeged));
			Streets.Add(new EasternRail(Belgrade, Szeged));

			Streets.Add(new EasternRail(Bucharest, Constanta));
			Streets.Add(new EasternRail(Bucharest, Galatz));
			Streets.Add(new EasternRail(Bucharest, Szeged));
			Streets.Add(new Street(Bucharest, Constanta));
			Streets.Add(new Street(Bucharest, Galatz));
			Streets.Add(new Street(Bucharest, Klausenburg));
			Streets.Add(new Street(Bucharest, Belgrade));
			Streets.Add(new Street(Bucharest, Sofia));

			Streets.Add(new Street(StJosephStMary, Szeged));
			Streets.Add(new Street(StJosephStMary, Belgrade));
			Streets.Add(new Street(StJosephStMary, Sarajevo));
			Streets.Add(new Street(StJosephStMary, Zagreb));

			Streets.Add(new Street(Zagreb, StJosephStMary));
			Streets.Add(new Street(Zagreb, Sarajevo));
			Streets.Add(new Street(Zagreb, Vienna));
			Streets.Add(new Street(Zagreb, Budapest));
			Streets.Add(new Street(Zagreb, Szeged));
			Streets.Add(new Street(Zagreb, Munich));

			Streets.Add(new Street(Szeged, StJosephStMary));
			Streets.Add(new Street(Szeged, Zagreb));
			Streets.Add(new Street(Szeged, Budapest));
			Streets.Add(new Street(Szeged, Klausenburg));
			Streets.Add(new Street(Szeged, Belgrade));
			Streets.Add(new EasternRail(Szeged, Budapest));
			Streets.Add(new EasternRail(Szeged, Bucharest));
			Streets.Add(new EasternRail(Szeged, Belgrade));

			Streets.Add(new Street(Budapest, Klausenburg));
			Streets.Add(new Street(Budapest, Vienna));
			Streets.Add(new Street(Budapest, Zagreb));
			Streets.Add(new Street(Budapest, Szeged));
			Streets.Add(new EasternRail(Budapest, Vienna));
			Streets.Add(new EasternRail(Budapest, Szeged));

			Streets.Add(new Street(Vienna, Budapest));
			Streets.Add(new Street(Vienna, Zagreb));
			Streets.Add(new Street(Vienna, Munich));
			Streets.Add(new Street(Vienna, Prague));
			Streets.Add(new EasternRail(Vienna, Budapest));
			Streets.Add(new EasternRail(Vienna, Prague));
			Streets.Add(new EasternRail(Vienna, Venice));

			Streets.Add(new Street(Prague, Vienna));
			Streets.Add(new EasternRail(Prague, Vienna));
			Streets.Add(new EasternRail(Prague, Berlin));
			Streets.Add(new Street(Prague, Berlin));
			Streets.Add(new Street(Prague, Nuremburg));

			Streets.Add(new Street(Munich, Vienna));
			Streets.Add(new Street(Munich, Zagreb));
			Streets.Add(new Street(Munich, Venice));
			Streets.Add(new Street(Munich, Milan));
			Streets.Add(new Street(Munich, Zurich));
			Streets.Add(new Street(Munich, Strasbourg));
			Streets.Add(new Street(Munich, Nuremburg));
			Streets.Add(new WesternRail(Munich, Nuremburg));

			Streets.Add(new Street(Berlin, Prague));
			Streets.Add(new Street(Berlin, Hamburg));
			Streets.Add(new Street(Berlin, Leipzig));
			Streets.Add(new EasternRail(Berlin, Prague));
			Streets.Add(new WesternRail(Berlin, Hamburg));
			Streets.Add(new WesternRail(Berlin, Leipzig));

			Streets.Add(new Street(Leipzig, Berlin));
			Streets.Add(new Street(Leipzig, Nuremburg));
			Streets.Add(new Street(Leipzig, Frankfurt));
			Streets.Add(new Street(Leipzig, Cologne));
			Streets.Add(new Street(Leipzig, Hamburg));
			Streets.Add(new WesternRail(Leipzig, Berlin));
			Streets.Add(new WesternRail(Leipzig, Nuremburg));
			Streets.Add(new WesternRail(Leipzig, Frankfurt));

			Streets.Add(new Street(Nuremburg, Leipzig));
			Streets.Add(new Street(Nuremburg, Prague));
			Streets.Add(new Street(Nuremburg, Munich));
			Streets.Add(new Street(Nuremburg, Strasbourg));
			Streets.Add(new Street(Nuremburg, Frankfurt));
			Streets.Add(new WesternRail(Nuremburg, Munich));
			Streets.Add(new WesternRail(Nuremburg, Leipzig));

			Streets.Add(new Port(Hamburg, NorthSea));
			Streets.Add(new Street(Hamburg, Berlin));
			Streets.Add(new Street(Hamburg, Leipzig));
			Streets.Add(new Street(Hamburg, Cologne));
			Streets.Add(new WesternRail(Hamburg, Berlin));

			Streets.Add(new Street(Frankfurt, Leipzig));
			Streets.Add(new Street(Frankfurt, Nuremburg));
			Streets.Add(new Street(Frankfurt, Strasbourg));
			Streets.Add(new Street(Frankfurt, Cologne));
			Streets.Add(new WesternRail(Frankfurt, Leipzig));
			Streets.Add(new WesternRail(Frankfurt, Cologne));
			Streets.Add(new WesternRail(Frankfurt, Strasbourg));

			Streets.Add(new Street(Cologne, Hamburg));
			Streets.Add(new Street(Cologne, Leipzig));
			Streets.Add(new Street(Cologne, Frankfurt));
			Streets.Add(new Street(Cologne, Strasbourg));
			Streets.Add(new Street(Cologne, Brussels));
			Streets.Add(new Street(Cologne, Amsterdam));
			Streets.Add(new WesternRail(Cologne, Frankfurt));
			Streets.Add(new WesternRail(Cologne, Brussels));

			Streets.Add(new Port(Amsterdam, NorthSea));
			Streets.Add(new Street(Amsterdam, Cologne));
			Streets.Add(new Street(Amsterdam, Brussels));

			Streets.Add(new Street(Strasbourg, Cologne));
			Streets.Add(new Street(Strasbourg, Frankfurt));
			Streets.Add(new Street(Strasbourg, Nuremburg));
			Streets.Add(new Street(Strasbourg, Munich));
			Streets.Add(new Street(Strasbourg, Zurich));
			Streets.Add(new Street(Strasbourg, Geneva));
			Streets.Add(new Street(Strasbourg, Paris));
			Streets.Add(new Street(Strasbourg, Brussels));

			Streets.Add(new Street(Brussels, Amsterdam));
			Streets.Add(new Street(Brussels, Cologne));
			Streets.Add(new Street(Brussels, Strasbourg));
			Streets.Add(new Street(Brussels, Paris));
			Streets.Add(new Street(Brussels, LeHavre));
			Streets.Add(new WesternRail(Brussels, Cologne));
			Streets.Add(new WesternRail(Brussels, Paris));

			Streets.Add(new Street(Paris, Brussels));
			Streets.Add(new Street(Paris, Strasbourg));
			Streets.Add(new Street(Paris, Geneva));
			Streets.Add(new Street(Paris, ClermontFerrand));
			Streets.Add(new Street(Paris, Nantes));
			Streets.Add(new Street(Paris, LeHavre));
			Streets.Add(new WesternRail(Paris, Brussels));
			Streets.Add(new WesternRail(Paris, Marseilles));
			Streets.Add(new WesternRail(Paris, Bordeaux));
			Streets.Add(new WesternRail(Paris, LeHavre));

			Streets.Add(new Port(LeHavre, EnglishChannel));
			Streets.Add(new Street(LeHavre, Brussels));
			Streets.Add(new Street(LeHavre, Paris));
			Streets.Add(new Street(LeHavre, Nantes));
			Streets.Add(new WesternRail(LeHavre, Paris));

			Streets.Add(new Port(Nantes, BayOfBiscay));
			Streets.Add(new Street(Nantes, LeHavre));
			Streets.Add(new Street(Nantes, Paris));
			Streets.Add(new Street(Nantes, ClermontFerrand));
			Streets.Add(new Street(Nantes, Bordeaux));

			Streets.Add(new Street(ClermontFerrand, Paris));
			Streets.Add(new Street(ClermontFerrand, Geneva));
			Streets.Add(new Street(ClermontFerrand, Marseilles));
			Streets.Add(new Street(ClermontFerrand, Toulouse));
			Streets.Add(new Street(ClermontFerrand, Bordeaux));
			Streets.Add(new Street(ClermontFerrand, Nantes));

			Streets.Add(new Street(Geneva, Zurich));
			Streets.Add(new Street(Geneva, Marseilles));
			Streets.Add(new Street(Geneva, ClermontFerrand));
			Streets.Add(new Street(Geneva, Paris));
			Streets.Add(new Street(Geneva, Strasbourg));
			Streets.Add(new EasternRail(Geneva, Milan));

			Streets.Add(new Street(Zurich, Munich));
			Streets.Add(new Street(Zurich, Milan));
			Streets.Add(new Street(Zurich, Marseilles));
			Streets.Add(new Street(Zurich, Geneva));
			Streets.Add(new Street(Zurich, Strasbourg));
			Streets.Add(new EasternRail(Zurich, Milan));
			Streets.Add(new WesternRail(Zurich, Strasbourg));

			Streets.Add(new Street(Milan, Munich));
			Streets.Add(new Street(Milan, Venice));
			Streets.Add(new Street(Milan, Genoa));
			Streets.Add(new Street(Milan, Marseilles));
			Streets.Add(new Street(Milan, Zurich));
			Streets.Add(new EasternRail(Milan, Zurich));
			Streets.Add(new EasternRail(Milan, Geneva));
			Streets.Add(new EasternRail(Milan, Genoa));
			Streets.Add(new EasternRail(Milan, Florence));

			Streets.Add(new EasternRail(Venice, Vienna));
			Streets.Add(new Street(Venice, Munich));
			Streets.Add(new Street(Venice, Milan));
			Streets.Add(new Street(Venice, Genoa));
			Streets.Add(new Street(Venice, Florence));
			Streets.Add(new Port(Venice, AdriaticSea));

			Streets.Add(new Port(Genoa, TyrrhenianSea));
			Streets.Add(new EasternRail(Genoa, Milan));
			Streets.Add(new Street(Genoa, Venice));
			Streets.Add(new Street(Genoa, Florence));
			Streets.Add(new Street(Genoa, Marseilles));
			Streets.Add(new Street(Genoa, Milan));

			Streets.Add(new EasternRail(Florence, Milan));
			Streets.Add(new EasternRail(Florence, Rome));
			Streets.Add(new Street(Florence, Venice));
			Streets.Add(new Street(Florence, Rome));
			Streets.Add(new Street(Florence, Genoa));

			Streets.Add(new Port(Rome, TyrrhenianSea));
			Streets.Add(new EasternRail(Rome, Florence));
			Streets.Add(new EasternRail(Rome, Bari));
			Streets.Add(new Street(Rome, Florence));
			Streets.Add(new Street(Rome, Bari));
			Streets.Add(new Street(Rome, Naples));

			Streets.Add(new Port(Naples, TyrrhenianSea));
			Streets.Add(new EasternRail(Naples, Bari));
			Streets.Add(new Street(Naples, Rome));
			Streets.Add(new Street(Naples, Bari));

			Streets.Add(new Port(Bari, AdriaticSea));
			Streets.Add(new EasternRail(Bari, Naples));
			Streets.Add(new Street(Bari, Rome));
			Streets.Add(new Street(Bari, Naples));

			Streets.Add(new Port(Cagliari, MediterraneanSea));
			Streets.Add(new Port(Cagliari, TyrrhenianSea));

			Streets.Add(new Port(Marseilles, MediterraneanSea));
			Streets.Add(new WesternRail(Marseilles, Paris));
			Streets.Add(new Street(Marseilles, Genoa));
			Streets.Add(new Street(Marseilles, Milan));
			Streets.Add(new Street(Marseilles, Zurich));
			Streets.Add(new Street(Marseilles, Geneva));
			Streets.Add(new Street(Marseilles, ClermontFerrand));
			Streets.Add(new Street(Marseilles, Toulouse));

			Streets.Add(new Street(Toulouse, Marseilles));
			Streets.Add(new Street(Toulouse, Barcelona));
			Streets.Add(new Street(Toulouse, Saragossa));
			Streets.Add(new Street(Toulouse, Bordeaux));
			Streets.Add(new Street(Toulouse, ClermontFerrand));

			Streets.Add(new Port(Bordeaux, BayOfBiscay));
			Streets.Add(new WesternRail(Bordeaux, Paris));
			Streets.Add(new WesternRail(Bordeaux, Saragossa));
			Streets.Add(new Street(Bordeaux, Nantes));
			Streets.Add(new Street(Bordeaux, ClermontFerrand));
			Streets.Add(new Street(Bordeaux, Toulouse));
			Streets.Add(new Street(Bordeaux, Saragossa));

			Streets.Add(new WesternRail(Barcelona, Saragossa));
			Streets.Add(new WesternRail(Barcelona, Alicante));
			Streets.Add(new Port(Barcelona, MediterraneanSea));
			Streets.Add(new Street(Barcelona, Toulouse));
			Streets.Add(new Street(Barcelona, Saragossa));

			Streets.Add(new WesternRail(Saragossa, Bordeaux));
			Streets.Add(new WesternRail(Saragossa, Barcelona));
			Streets.Add(new WesternRail(Saragossa, Madrid));
			Streets.Add(new Street(Saragossa, Bordeaux));
			Streets.Add(new Street(Saragossa, Toulouse));
			Streets.Add(new Street(Saragossa, Barcelona));
			Streets.Add(new Street(Saragossa, Alicante));
			Streets.Add(new Street(Saragossa, Madrid));
			Streets.Add(new Street(Saragossa, Santander));

			Streets.Add(new Port(Santander, BayOfBiscay));
			Streets.Add(new WesternRail(Santander, Madrid));
			Streets.Add(new Street(Santander, Saragossa));
			Streets.Add(new Street(Santander, Madrid));
			Streets.Add(new Street(Santander, Lisbon));

			Streets.Add(new WesternRail(Madrid, Santander));
			Streets.Add(new WesternRail(Madrid, Saragossa));
			Streets.Add(new WesternRail(Madrid, Alicante));
			Streets.Add(new WesternRail(Madrid, Lisbon));
			Streets.Add(new Street(Madrid, Lisbon));
			Streets.Add(new Street(Madrid, Santander));
			Streets.Add(new Street(Madrid, Saragossa));
			Streets.Add(new Street(Madrid, Alicante));
			Streets.Add(new Street(Madrid, Granada));
			Streets.Add(new Street(Madrid, Cadiz));

			Streets.Add(new Port(Alicante, MediterraneanSea));
			Streets.Add(new WesternRail(Alicante, Barcelona));
			Streets.Add(new WesternRail(Alicante, Madrid));
			Streets.Add(new Street(Alicante, Saragossa));
			Streets.Add(new Street(Alicante, Madrid));
			Streets.Add(new Street(Alicante, Granada));

			Streets.Add(new Street(Granada, Alicante));
			Streets.Add(new Street(Granada, Madrid));
			Streets.Add(new Street(Granada, Cadiz));

			Streets.Add(new Port(Cadiz, AtlanticOcean));
			Streets.Add(new Street(Cadiz, Granada));
			Streets.Add(new Street(Cadiz, Lisbon));

			Streets.Add(new Port(Lisbon, AtlanticOcean));
			Streets.Add(new Street(Lisbon, Santander));
			Streets.Add(new Street(Lisbon, Madrid));
			Streets.Add(new Street(Lisbon, Cadiz));

			Streets.Add(new Port(Galway, AtlanticOcean));
			Streets.Add(new Street(Galway, Dublin));

			Streets.Add(new Port(Dublin, IrishSea));
			Streets.Add(new Street(Dublin, Galway));

			Streets.Add(new Port(Edinburgh, NorthSea));
			Streets.Add(new WesternRail(Edinburgh, Manchester));
			Streets.Add(new Street(Edinburgh, Manchester));

			Streets.Add(new WesternRail(Manchester, Edinburgh));
			Streets.Add(new WesternRail(Manchester, Liverpool));
			Streets.Add(new WesternRail(Manchester, London));
			Streets.Add(new Street(Manchester, Edinburgh));
			Streets.Add(new Street(Manchester, Liverpool));
			Streets.Add(new Street(Manchester, London));

			Streets.Add(new Port(Liverpool, IrishSea));
			Streets.Add(new WesternRail(Liverpool, Manchester));
			Streets.Add(new Street(Liverpool, Manchester));
			Streets.Add(new Street(Liverpool, Swansea));

			Streets.Add(new Port(Swansea, IrishSea));
			Streets.Add(new WesternRail(Swansea, London));
			Streets.Add(new Street(Swansea, Liverpool));
			Streets.Add(new Street(Swansea, London));

			Streets.Add(new Port(Plymouth, EnglishChannel));
			Streets.Add(new Port(Plymouth, London));

			Streets.Add(new Port(London, EnglishChannel));
			Streets.Add(new WesternRail(London, Manchester));
			Streets.Add(new WesternRail(London, Swansea));
			Streets.Add(new Street(London, Manchester));
			Streets.Add(new Street(London, Swansea));
			Streets.Add(new Street(London, Plymouth));

			// sea travel
			Streets.Add(new Ship(BlackSea, IonianSea));
			Streets.Add(new Port(BlackSea, Constanta));
			Streets.Add(new Port(BlackSea, Varna));

			Streets.Add(new Ship(IonianSea, BlackSea));
			Streets.Add(new Ship(IonianSea, AdriaticSea));
			Streets.Add(new Ship(IonianSea, TyrrhenianSea));
			Streets.Add(new Port(IonianSea, Salonica));
			Streets.Add(new Port(IonianSea, Athens));
			Streets.Add(new Port(IonianSea, Valona));
			
			Streets.Add(new Ship(AdriaticSea, IonianSea));
			Streets.Add(new Port(AdriaticSea, Bari));
			Streets.Add(new Port(AdriaticSea, Venice));

			Streets.Add(new Ship(TyrrhenianSea, IonianSea));
			Streets.Add(new Ship(TyrrhenianSea, MediterraneanSea));
			Streets.Add(new Port(TyrrhenianSea, Cagliari));
			Streets.Add(new Port(TyrrhenianSea, Genoa));
			Streets.Add(new Port(TyrrhenianSea, Rome));
			Streets.Add(new Port(TyrrhenianSea, Naples));

			Streets.Add(new Ship(MediterraneanSea, TyrrhenianSea));
			Streets.Add(new Ship(MediterraneanSea, AtlanticOcean));
			Streets.Add(new Port(MediterraneanSea, Alicante));
			Streets.Add(new Port(MediterraneanSea, Barcelona));
			Streets.Add(new Port(MediterraneanSea, Marseilles));
			Streets.Add(new Port(MediterraneanSea, Cagliari));

			Streets.Add(new Ship(AtlanticOcean, MediterraneanSea));
			Streets.Add(new Ship(AtlanticOcean, BayOfBiscay));
			Streets.Add(new Ship(AtlanticOcean, EnglishChannel));
			Streets.Add(new Ship(AtlanticOcean, IrishSea));
			Streets.Add(new Ship(AtlanticOcean, NorthSea));
			Streets.Add(new Port(AtlanticOcean, Galway));
			Streets.Add(new Port(AtlanticOcean, Lisbon));
			Streets.Add(new Port(AtlanticOcean, Cadiz));

			Streets.Add(new Ship(BayOfBiscay, AtlanticOcean));
			Streets.Add(new Port(BayOfBiscay, Nantes));
			Streets.Add(new Port(BayOfBiscay, Bordeaux));
			Streets.Add(new Port(BayOfBiscay, Santander));

			Streets.Add(new Ship(IrishSea, AtlanticOcean));
			Streets.Add(new Port(IrishSea, Liverpool));
			Streets.Add(new Port(IrishSea, Swansea));
			Streets.Add(new Port(IrishSea, Dublin));

			Streets.Add(new Ship(EnglishChannel, AtlanticOcean));
			Streets.Add(new Ship(EnglishChannel, NorthSea));
			Streets.Add(new Port(EnglishChannel, London));
			Streets.Add(new Port(EnglishChannel, LeHavre));
			Streets.Add(new Port(EnglishChannel, Plymouth));

			Streets.Add(new Ship(NorthSea, EnglishChannel));
			Streets.Add(new Ship(NorthSea, AtlanticOcean));
			Streets.Add(new Port(NorthSea, Hamburg));
			Streets.Add(new Port(NorthSea, Amsterdam));
			Streets.Add(new Port(NorthSea, Edinburgh));
		}

		public void MoveCharacter(ACharacter c, ALocation newLocation)
		{
			ALocation currentLocation = c.Position;
			RemoveCharacterFromLocation(c, currentLocation);
			c.Position = newLocation;
			// make sure the character is at the location
			c.PositionOffsetX = c.Position.X;
			c.PositionOffsetY = c.Position.Y;
			AddCharacterToLocation(c, newLocation);
		}

		private void AddCharacterToLocation(ACharacter c, ALocation newLocation)
		{
			if (newLocation != null)
			{
				List<ACharacter> chars = new List<ACharacter>();
				foreach (ACharacter c2 in newLocation.Characters)
				{
					chars.Add(c2);
				}
				chars.Add(c);
				newLocation.Characters = chars;
			}
		}

		private static void RemoveCharacterFromLocation(ACharacter c, ALocation currentLocation)
		{
			if (currentLocation != null)
			{
				List<ACharacter> chars = new List<ACharacter>();
				foreach (ACharacter c2 in currentLocation.Characters)
				{
					if (c2 != c)
					{
						chars.Add(c2);
					}
				}
				currentLocation.Characters = chars;
			}
		}
	}
}
