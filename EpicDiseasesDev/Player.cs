using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TShockAPI;
using Terraria;

namespace EpicDiseases
{
    public class Player
    {
        public int Index { get; set; }
        public TSPlayer TSPlayer { get { return TShock.Players[Index]; } }

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

        protected Infected State = Infected.Clear;

        public Infected Clear()
        {
            return State;
        }
        public void SetState(Infected state)
        {
            State = state;
        }
        public void SendMessage(string message, int colorR, int colorG, int colorB)
        {
            NetMessage.SendData((int)PacketTypes.ChatText, Index, -1, message, 255, colorR, colorG, colorB);
        }
    }

    public enum Infected
    {
        Clear,
        Infected
    }
}
