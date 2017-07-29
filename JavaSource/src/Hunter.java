/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Phil
 */
public class Hunter extends Player {
    
    protected int bites = 0;
    
    public Hunter(String name, int maxHealth, int bites){
	super(name, maxHealth);
	this.bites = bites;
    }
}
