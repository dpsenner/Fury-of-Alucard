
import java.awt.Dimension;
import java.awt.event.*;
import java.io.IOException;
import java.util.ArrayList;
import javax.swing.ImageIcon;
import javax.swing.JButton;
import javax.swing.JLabel;
import javax.swing.JPanel;

/*
 * To change this template, choose Tools | Templates and open the template in
 * the editor.
 */
/**
 *
 * @author Phil
 */
public class MapPanel extends JPanel {

    public static final int BUTTON_SIZE = 30;
    private JLabel map = new JLabel(new ImageIcon("./textures/map.jpg"));
    private ArrayList<City> cities = new ArrayList<>();
    private Dracula dracula = new Dracula();

    public MapPanel() {
	
	/**
	 * DONT LOOK =)
	 */
	cities.add(new City("Lisbon", 26, 617,true,false));
	cities.add(new City("Cadiz", 71, 687,true,false));
	cities.add(new City("Madrid", 124, 611,true,false));
	cities.add(new City("Santander",141,538,false,false));
	cities.add(new City("Grenada",137,680,false,false));
	cities.add(new City("Barcelona",248,608,true,false));
	cities.add(new City("Alicante",196,669,false,false));
	cities.add(new City("Sarragossa",181,586,false,false));
	cities.add(new City("Toulouse",240,554,false,false));
	/**
	 * LOOK AGAIN
	 */
	
	
	this.setPreferredSize(new Dimension(800, 830));
	this.map.setSize(800, 830);
	this.add(map);
	this.setLayout(null);

	for (int i = 0; i < cities.size(); ++i) {
	    cities.get(i).getButton().setBounds(cities.get(i).getX(), cities.get(i).getY(),
		    BUTTON_SIZE, BUTTON_SIZE);
	    this.add(cities.get(i).getButton());
	    cities.get(i).getButton().addActionListener(new CityListener());
	}
	this.addMouseListener(new MouseAdapter() {
	    public void mouseClicked(MouseEvent e) {
		System.out.println(e.getX() + "," + e.getY());
	    }
	    
	});

    }

    private class CityListener implements ActionListener {

	@Override
	public void actionPerformed(ActionEvent e) {
	    try {
		JButton source = (JButton) e.getSource();
		City activeCity = cities.get(WIDTH);
		for (int i = 0; i < cities.size(); ++i) {
		    if (cities.get(i).getButton().getIcon() != null) {
			activeCity = cities.get(i);
		    }
		}
		for (int i = 0; i < cities.size(); ++i) {
		    if (source.equals(cities.get(i).getButton())) {
			if (activeCity.isStreetConnected(cities.get(i).getName())) {
			    cities.get(i).getButton().setContentAreaFilled(true);
			    cities.get(i).getButton().setIcon(dracula.getIcon());
			    activeCity.getButton().setContentAreaFilled(false);
			    activeCity.getButton().setIcon(null);
			} else {
			    activeCity.getButton().setContentAreaFilled(true);
			    activeCity.getButton().setIcon(dracula.getIcon());
			    break;
			}
		    } else {
			cities.get(i).getButton().setContentAreaFilled(false);
			cities.get(i).getButton().setIcon(null);

		    }
		}
	    } catch (IOException ioex) {
		System.out.println("Fuck that shit!");
	    }
	}
    }
}
