using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwitcherDisplayClient.Models
{
	internal class SwitchState
	{
		internal int Program {get; set;} = 1;
		internal int Preview {get; set;} = 1;

		internal string ToJson( )
		{
			return "{\r\n" +
				"\t\"program\": " + this.Program.ToString( ) + "\r\n" +
				"\t\"preview\": " + this.Preview.ToString( ) + "\r\n" +
				"}\r\n";
		}

	}
}
