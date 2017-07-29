
import javax.swing.ImageIcon;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Phil
 */
public abstract class Player {
    protected String name = null;
    protected ImageIcon icon = null;
    protected int health = 0;
    protected int maxHealth = 0;
    
    public Player(String name, int maxHealth){
	this.name = name;
	this.icon = new ImageIcon("textures/" + name + ".jpg");
	this.maxHealth = maxHealth;
	System.out.println("textures/" + name + ".jpg");
    }

    /**
     * @return the icon
     */
    public ImageIcon getIcon() {
	return icon;
    }

    /**
     * @param icon the icon to set
     */
    public void setIcon(ImageIcon icon) {
	this.icon = icon;
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
}
