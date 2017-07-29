
import java.io.BufferedReader;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import javax.swing.JButton;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Phil
 */
public class City extends Location{
    
    private boolean isBig = false;
    private boolean isEast;
    
    
    public City(String name, int x, int y, boolean isBig, boolean isEast){
	super(name, x, y);
	this.isBig = isBig;
	this.isEast = isEast;
	//Makes Button invisible
	this.button.setBorderPainted(false);
	this.button.setContentAreaFilled(false);
    }


    
    public boolean isStreetConnected(String neighbour) throws FileNotFoundException, IOException{
	BufferedReader reader = new BufferedReader(new FileReader("./data/cities/" + getName() + "_street.txt"));
	String line = reader.readLine();
	while(line != null && !line.equals("")){
	    if (line.equals(neighbour))
		return true;
	    line = reader.readLine();
	}
	return false;
    }

    /**
     * @return the isBig
     */
    public boolean isIsBig() {
	return isBig;
    }

    /**
     * @param isBig the isBig to set
     */
    public void setIsBig(boolean isBig) {
	this.isBig = isBig;
    }

    /**
     * @return the isEast
     */
    public boolean isIsEast() {
	return isEast;
    }

    /**
     * @param isEast the isEast to set
     */
    public void setIsEast(boolean isEast) {
	this.isEast = isEast;
    }
}
