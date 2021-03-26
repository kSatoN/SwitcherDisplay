using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		private IReadOnlyList<TextBox> ProgramList = new List<TextBox>( );
		private IReadOnlyList<TextBox> PreviewList = new List<TextBox>( );
		private bool Running = false;

		public MainWindow( )
		{
			this.InitializeComponent( );
			this.InitializeTextBox( );
		}

		private void InitializeTextBox( )
		{
			this.ProgramList = new List<TextBox> {this.TextBoxPg1, this.TextBoxPg2, this.TextBoxPg3, this.TextBoxPg4, this.TextBoxPg5, this.TextBoxPg6, this.TextBoxPg7, this.TextBoxPg8, this.TextBoxPg9};
			this.PreviewList = new List<TextBox> {this.TextBoxPv1, this.TextBoxPv2, this.TextBoxPv3, this.TextBoxPv4, this.TextBoxPv5, this.TextBoxPv6, this.TextBoxPv7, this.TextBoxPv8, this.TextBoxPv9};
		}

		private void ButtonStopClick(object sender, RoutedEventArgs e)
		{
			this.Toggle(false);
		}

		private void ButtonRunClick(object sender, RoutedEventArgs e)
		{
			this.Toggle(true);
			Models.KeyInfo keyInfo = new Models.KeyInfo( );
			this.Poll(keyInfo);
		}

		private void Toggle(bool toState)
		{
			Debug.WriteLine("状態を変えるよ");
			if (toState)
			{
				this.ButtonRun.IsEnabled = false;
				this.ButtonStop.IsEnabled = true;
				this.Running = true;
				return;
			}
			this.ButtonStop.IsEnabled = false;
			this.ButtonRun.IsEnabled = true;
			this.Running = false;
		}

		private async void Poll(Models.KeyInfo keyInfo)
		{
			await Task.Run(( ) =>
			{
				while (this.Running)
				{
					// ToDo: キーの取得（非同期あいえる）
					int key = this.GetNumberKey(keyInfo);
					// デバッグここから
					Debug.WriteLine(key);
					// デバッグここまで
				}
			});
			return;
		}

		private int GetNumberKey(Models.KeyInfo keyInfo)
		{
			// space bar
			if (keyInfo.IsKeyPress(0x20))
			{
				return -2;
			}
			// return key
			if (keyInfo.IsKeyPress(0x0d))
			{
				return -3;
			}
			// 数字キー（キーボード上部＆テンキー）※ 同時押しの場合は0以外の最も小さい数字
			if (keyInfo.IsKeyPress('1') || keyInfo.IsKeyPress(0x61))
			{
				return 1;
			}
			if (keyInfo.IsKeyPress('2') || keyInfo.IsKeyPress(0x62))
			{
				return 2;
			}
			if (keyInfo.IsKeyPress('3') || keyInfo.IsKeyPress(0x63))
			{
				return 3;
			}
			if (keyInfo.IsKeyPress('4') || keyInfo.IsKeyPress(0x64))
			{
				return 4;
			}
			if (keyInfo.IsKeyPress('5') || keyInfo.IsKeyPress(0x65))
			{
				return 5;
			}
			if (keyInfo.IsKeyPress('6') || keyInfo.IsKeyPress(0x66))
			{
				return 6;
			}
			if (keyInfo.IsKeyPress('7') || keyInfo.IsKeyPress(0x67))
			{
				return 7;
			}
			if (keyInfo.IsKeyPress('8') || keyInfo.IsKeyPress(0x68))
			{
				return 8;
			}
			if (keyInfo.IsKeyPress('9') || keyInfo.IsKeyPress(0x69))
			{
				return 9;
			}
			if (keyInfo.IsKeyPress('0') || keyInfo.IsKeyPress(0x60))
			{
				return 0;
			}
			return -1;
		}

	}
}
