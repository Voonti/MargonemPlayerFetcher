using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public static class GlobalParameters
    {
        public static readonly int PLAYERS_MIN_LVL = 25;
        public static readonly Regex EVENT_REGEX = new Regex("[0-9][0-9][0-9][0-9]");
        public static readonly Regex LICYTY_REGEX = new Regex("(Licytacja)");
        public static readonly string PLAYER_CLIENT = "PlayerClient";
        public static readonly string ITEM_CLIENT = "ItemClient";
    }
}
