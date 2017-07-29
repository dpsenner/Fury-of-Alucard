
import javax.swing.JButton;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Phil
 */
public class Location {
    
    protected String name = ""; 
    protected JButton button = new JButton();
    protected int x;
    protected int y;

    
    public Location(String name, int x, int y){
	this.name = name;
	this.x = x;
	this.y = y;
    }
    
    
    
    /**
     * @return the name
     */
    public String getName() {
	return name;
    }

    /**
     * @param name the name to set
     */
    public void setName(String name) {
	this.name = name;
    }

    /**
     * @return the button
     */
    public JButton getButton() {
	return button;
    }

    /**
     * @param button the button to set
     */
    public void setButton(JButton button) {
	this.button = button;
    }

    /**
     * @return the x
     */
    public int getX() {
	return x;
    }

    /**
     * @param x the x to set
     */
    public void setX(int x) {
	this.x = x;
    }

    /**
     * @return the y
     */
    public int getY() {
	return y;
    }

    /**
     * @param y the y to set
     */
    public void setY(int y) {
	this.y = y;
    }
    
    
}
