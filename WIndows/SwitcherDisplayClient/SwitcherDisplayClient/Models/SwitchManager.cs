using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace SwitcherDisplayClient.Models
{
	public class SwitchManager
	{
		private readonly IReadOnlyList<TextBox> ProgramList;
		private readonly IReadOnlyList<TextBox> PreviewList;
		private readonly SwitchState SwitchState = new SwitchState( );

		public SwitchManager(IReadOnlyList<TextBox> programList, IReadOnlyList<TextBox> previewList)
		{
			this.ProgramList = programList;
			this.PreviewList = previewList;
		}

		/// <summary>開始処理をします。</summary>
		public void Start( )
		{
			this.SwitchState.IsRunning = true;
			this.Send( );
		}

		/// <summary>終了処理をします。</summary>
		public void Stop( )
		{
			this.SwitchState.IsRunning = false;
			this.Send( );
		}

		/// <summary>カメラの選択状態を切替えます。非同期呼び出し可能です。</summary>
		/// <param name="keyNumber">押されているキーの番号（space：-2，return：-3）</param>
		public void ChangeSelection(int keyNumber)
		{
			// 今のプレビューと同じキーであれば何もしない
			if (keyNumber == this.SwitchState.Preview)
			{
				return;
			}
			bool isReturn = keyNumber < -1;
			this.UpdateState(keyNumber);
			this.Send( );
			this.UpdateColor(isReturn);
		}

		/// <summary>Stateを更新します。</summary>
		/// <param name="keyNumber">押されているキーの番号（space：-2，return：-3）</param>
		private void UpdateState(int keyNumber)
		{
			bool isReturn = keyNumber < -1;
			bool isNumber = 0 < keyNumber;
			// 無効キーの場合，何もしない
			if (!isReturn && !isNumber)
			{
				return;
			}
			// 数字キーの場合
			if (isNumber)
			{
				this.SwitchState.Preview = keyNumber;
				return;
			}
			// return・spaceの場合
			int previewCamera = this.SwitchState.Program;
			this.SwitchState.Program = this.SwitchState.Preview;
			this.SwitchState.Preview = previewCamera;
		}

		/// <summary>現在の状態をJSONで送信します。</summary>
		private void Send( )
		{
			string json = this.SwitchState.ToJson( );
			Debug.WriteLine(json);
			// TODO: ネットワークで送信
		}

		/// <summary>1度色をリセットしてから，現在の状態に色づけします。</summary>
		private void UpdateColor(bool isReturn)
		{
			if (isReturn)
			{
				List<bool> programs = this.ProgramList.Select(textBox => this.ResetTextBoxColor(textBox)).ToList( );
				this.ActiveTextBoxProgram(this.SwitchState.Program);
			}
			List<bool> previes = this.PreviewList.Select(textBox => this.ResetTextBoxColor(textBox)).ToList( );
			this.ActiveTextBoxPreview(this.SwitchState.Preview);
		}

		/// <summary>テキストボックスの色をリセットします。</summary>
		/// <param name="textBox">色をリセットするテキストボックス</param>
		private bool ResetTextBoxColor(TextBox textBox)
		{
			textBox.Dispatcher.Invoke(new Action(( ) =>
			{
				textBox.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x60, 0x60, 0x60));
				textBox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
			}));
			return true;
		}

		/// <summary>Programのテキストボックスを有効化します。</summary>
		/// <param name="number">何カメを有効化するか。</param>
		private bool ActiveTextBoxProgram(int number)
		{
			TextBox textBox = this.ProgramList[number - 1];
			textBox.Dispatcher.Invoke(new Action(( ) =>
			{
				textBox.Background = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0x52, 0x52));
				textBox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
			}));
			return true;
		}

		/// <summary>Previewのテキストボックスを有効化します。</summary>
		/// <param name="number">何カメを有効化するか。</param>
		private bool ActiveTextBoxPreview(int number)
		{
			TextBox textBox = this.PreviewList[number - 1];
			textBox.Dispatcher.Invoke(new Action(( ) =>
			{
				textBox.Background = new SolidColorBrush(Color.FromArgb(0xff, 0x8d, 0xf2, 0x74));
				textBox.Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0x00, 0x00, 0x00));
			}));
			return true;
		}

	}
}
