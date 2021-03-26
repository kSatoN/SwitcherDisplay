using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SwitcherDisplayClient.Models
{
	public class SwitchManager
	{
		private readonly IReadOnlyList<TextBox> ProgramList;
		private readonly IReadOnlyList<TextBox> PreviewList;
		private readonly SwitchState SwitchState = new SwitchState( );
		private int PreviousKey = -1;

		public SwitchManager(IReadOnlyList<TextBox> programList, IReadOnlyList<TextBox> previewList)
		{
			this.ProgramList = programList;
			this.PreviewList = previewList;
		}

		/// <summary>カメラの選択状態を切替えます。非同期呼び出し可能です。</summary>
		/// <param name="keyNumber">押されているキーの番号（space：-2，return：-3）</param>
		public void ChangeSelection(int keyNumber)
		{
			// 前回と同じ数字キー⇒何もしない
			if (0 < keyNumber && keyNumber == this.PreviousKey)
			{
				return;
			}
			this.UpdateState(keyNumber);
			this.Send( );
			this.UpdateColor( );
			// 最後に前回キーの更新
			if (0 < keyNumber)
			{
				this.PreviousKey = keyNumber;
			}
		}

		/// <summary>Stateを更新します。</summary>
		/// <param name="keyNumber">押されているキーの番号（space：-2，return：-3）</param>
		private void UpdateState(int keyNumber)
		{
			// 0キーの場合，何もしない
			if (keyNumber == 0)
			{
				return;
			}
			// space・returnでない場合
			if (0 < keyNumber)
			{
				this.SwitchState.Preview = keyNumber;
				return;
			}
			// space・returnの場合
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

		/// <summary>画面の色を現在の状態に更新します。</summary>
		private void UpdateColor( )
		{
			// TODO: 色の更新
		}

	}
}
