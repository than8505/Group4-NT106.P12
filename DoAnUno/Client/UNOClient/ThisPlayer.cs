using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNOClient
{
    class ThisPlayer
    {
		// Tên của người chơi
		public static string Name { get; set; }

		// Lượt của người chơi
		public static int Turn { get; set; }

		// Số lượng thẻ của người chơi
		public static int NumOfCards { get; set; }

		// Danh sách thẻ của người chơi
		public static List<string> Cards { get; } = new List<string>();
	}

    class OtherPlayers
    {
		// Tên của người chơi khác
		public string Name { get; set; }

		// Lượt của người chơi khác (nên đổi thành kiểu int)
		public int Turn { get; set; }

		// Số lượng thẻ của người chơi khác (nên đổi thành kiểu int)
		public int NumOfCards { get; set; }
	}
}
