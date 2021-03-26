using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherDisplayClient.Models
{
	public class KeyInfo
	{
		[System.Runtime.InteropServices.DllImport("user32.dll")]

		private static extern short GetKeyState(int nVirtKey);

		public bool IsKeyPress(int key)
		{
			// 押されているときは最上位ビットが1⇒負
			return GetKeyState(key) < 0;
		}

	}
}
