using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Card
    {
        public static string faceUpCard = "";
        public static string[] idCard =
        {
            "r0", "r1", "r2", "r3", "r4", "r5", "r1_", "r2_", "r3_", "r4_", "r5_",
                "b0", "b1", "b2", "b3", "b4", "b5", "b1_", "b2_", "b3_", "b4_", "b5_",
                "y0", "y1", "y2", "y3", "y4", "y5", "y1_", "y2_", "y3_", "y4_", "y5_",
                "g0", "g1", "g2", "g3", "g4", "g5", "g1_", "g2_", "g3_", "g4_", "y5_",
                "Rv_r", "Rv_r_X", "Rv_b", "Rv_b_X", "Rv_y", "Rv_y_X", "Rv_g", "Rv_g_X",
                "s_r", "s_r_X", "s_b", "s_b_X", "s_y", "s_y_X", "s_g", "s_g_X",
                "wd", "wd_X", "wd_Y", "wd_Z",
                "df", "df_X", "df_Y", "df_Z",
                "dt_r", "dt_r_X", "dt_b", "dt_b_X", "dt_y", "dt_y_X", "dt_g", "dt_g_X"
        };
    }

    class Discard
    {
        public static List<string> disCard = new List<string>();
    }
}
