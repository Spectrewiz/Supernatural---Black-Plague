using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;

namespace EpicDiseases
{
    class Player
    {
        public int Index { get; set; }
        public TSPlayer TSPlayer { get { return TShock.Players[Index]; } }
        //Add other variables here - MAKE SURE YOU DON'T MAKE THEM STATIC

        public Player(int index)
        {
            Index = index;
        }

        public static Player GetPlayerById(int index)
        {
            foreach (Player player in EpicDiseases.Players)
            {
                if (player != null && player.Index == index)
                    return player;
            }
            return null;
        }

        // Return player by Name
        public static Player GetPlayerByName(string name)
        {
            var player = TShock.Utils.FindPlayer(name)[0];
            if (player != null)
            {
                foreach (Player ply in EpicDiseases.Players)
                {
                    if (ply.TSPlayer == player)
                    {
                        return ply;
                    }
                }
            }
            return null;
        }
    }
}
