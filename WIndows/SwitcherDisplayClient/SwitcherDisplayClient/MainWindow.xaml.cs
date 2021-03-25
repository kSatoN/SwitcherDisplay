using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SwitcherDisplayClient
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private LinkedList<TextBox> ProgramList = new LinkedList<TextBox>( );
		private LinkedList<TextBox> PreviewList = new LinkedList<TextBox>( );

		public MainWindow( )
		{
			this.InitializeComponent( );
			this.InitializeTextBox( );
		}

		private void InitializeTextBox( )
		{
			TextBox[ ] programArray = {this.TextBoxPg1, this.TextBoxPg2, this.TextBoxPg3, this.TextBoxPg4, this.TextBoxPg5, this.TextBoxPg6, this.TextBoxPg7, this.TextBoxPg8, this.TextBoxPg9};
			this.ProgramList = new LinkedList<TextBox>(programArray);
			TextBox[] previewArray = {this.TextBoxPv1, this.TextBoxPv2, this.TextBoxPv3, this.TextBoxPv4, this.TextBoxPv5, this.TextBoxPv6, this.TextBoxPv7, this.TextBoxPv8, this.TextBoxPv9};
			this.PreviewList = new LinkedList<TextBox>(previewArray);
		}

		private void ButtonStopClick(object sender, RoutedEventArgs e)
		{
			this.ButtonStop.IsEnabled = false;
			this.ButtonRun.IsEnabled = true;
		}

		private void ButtonRunClick(object sender, RoutedEventArgs e)
		{
			this.ButtonRun.IsEnabled = false;
			this.ButtonStop.IsEnabled = true;
		}

	}
}
