using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using Fury_of_Alucard.Domain;
using Fury_of_Alucard.Remoting;

namespace Fury_of_Alucard
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public GameManager Manager { get; private set; }

		public ACharacter SelectedCharacter { get; private set; }

		public ClientActionHandler Remoting { get; private set; }

		public MainWindow()
		{
			try
			{
				// start remoting
				Remoting = new ClientActionHandler();
				Remoting.HandleEcho += new Action(Remoting_HandleEcho);
				Remoting.HandleMoveCharacter += new Action<string, string, double, double>(Remoting_HandleMoveCharacter);
				Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);

				// initialize manager
				Manager = new GameManager();

				// initialize the map
				Manager.InitializeMap();

				// get the character locations
				Remoting.DoGetCharacterLocations();

				InitializeComponent();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				Application.Current.Shutdown();
			}
		}

		void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			 // dispose of the remoting
		}

		private void MapGrid_MouseDown(object sender, MouseEventArgs e)
		{
			Point relativeXY = e.GetPosition(MapGrid);
			Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "{0} ===> , {1}, {2}", relativeXY, relativeXY.X / MapGrid.ActualWidth, relativeXY.Y / MapGrid.ActualHeight));
		}

		private void Window_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				Remoting.DoGetCharacterLocations();
			}
			else if (e.Key == Key.F12)
			{
				// toggle fullscreen
				switch (WindowState)
				{
					case System.Windows.WindowState.Maximized:
						WindowState = System.Windows.WindowState.Normal;
						WindowStyle = System.Windows.WindowStyle.SingleBorderWindow;
						break;
					case System.Windows.WindowState.Normal:
						WindowState = System.Windows.WindowState.Maximized;
						WindowStyle = System.Windows.WindowStyle.None;
						break;
				}
			}
		}

		private void Character_MouseDown(object sender, MouseButtonEventArgs e)
		{
			// mouse down on a player
			object context = (sender as Border).DataContext;
			ACharacter c = context as ACharacter;

			if (c == null)
				return;

			InvertHighlighting(SelectedCharacter);

			if (SelectedCharacter == c)
			{
				// deselect
				SelectedCharacter = null;
				return;
			}

			SelectedCharacter = c;
			InvertHighlighting(SelectedCharacter);
		}

		private void InvertHighlighting(ACharacter c)
		{
			if (c == null)
				return;

			c.IsHighlighted = !c.IsHighlighted;
			foreach (ALocation loc in Manager.Game.GetReachableCities(c))
			{
				// highlight the location
				loc.IsSelectable = !loc.IsSelectable;
				if (loc.IsSelectable)
					loc.IsHighlighted = true;
				else
					loc.IsHighlighted = false;
			}
		}

		private void Location_MouseDown(object sender, MouseButtonEventArgs e)
		{
			// mouse down on a location
			object context = (sender as FrameworkElement).DataContext;
			ALocation l = context as ALocation;

			if (l != null)
			{
				if (SelectedCharacter != null)
				{
					if (Manager.Game.GetReachableCities(SelectedCharacter).Contains(l))
					{
						// place the character where he clicked
						Point clicked = e.GetPosition(MapGrid);
						Remoting.DoMoveCharacter(SelectedCharacter.Name, l.Name, (clicked.X / MapGrid.ActualWidth), (clicked.Y / MapGrid.ActualHeight));
					}
				}
			}
		}

		private void Polygon_MouseEnter(object sender, MouseEventArgs e)
		{
			object context = (sender as FrameworkElement).DataContext;
			ALocation l = context as ALocation;
			l.IsHighlighted = true;
		}

		private void Polygon_MouseLeave(object sender, MouseEventArgs e)
		{
			object context = (sender as FrameworkElement).DataContext;
			ALocation l = context as ALocation;
			if (SelectedCharacter != null)
			{
				if (Manager.Game.GetReachableCities(SelectedCharacter).Contains(l))
				{
					// dont remove the highlighting
				}
				else
				{
					l.IsHighlighted = false;
				}
			}
			else
			{
				l.IsHighlighted = false;
			}
		}

		void Remoting_HandleMoveCharacter(string character, string location, double xoffset, double yoffset)
		{
			Console.WriteLine("HandleMoveCharacter({0}, {1})", character, location);
			foreach (ACharacter c in Manager.Game.Characters)
			{
				if (c.Name == character)
				{
					foreach (ALocation l in Manager.Game.Map.Locations)
					{
						if (l.Name == location)
						{
							if (SelectedCharacter == c)
							{
								InvertHighlighting(SelectedCharacter);
							}

							Manager.Game.Map.MoveCharacter(c, l);

							if (xoffset >= 0)
								c.PositionOffsetX = xoffset;
							if (yoffset >= 0)
								c.PositionOffsetY = yoffset;

							if (SelectedCharacter == c)
							{
								SelectedCharacter = null;
							}
						}
					}
				}
			}
		}

		void Remoting_HandleEcho()
		{
			Console.WriteLine("HandleEcho()");
		}
	}
}
