
import javax.swing.JFrame;

/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

/**
 *
 * @author Phil
 */
public class NewMain {

    MapPanel mapPanel = new MapPanel();
    
    public NewMain(){
	JFrame frame = new JFrame("Sprint");
	frame.add(mapPanel);
	frame.setBounds(10, 10, 800, 830);
	frame.setDefaultCloseOperation(JFrame.DISPOSE_ON_CLOSE);
	frame.setVisible(true);
	
	
    }
    
    
    public static void main(String[] args) {
	new NewMain();
    }
}
