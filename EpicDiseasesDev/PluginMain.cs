using System;
using System.Collections.Generic;
using System.Reflection;
using System.Drawing;
using Terraria;
using Hooks;
using TShockAPI;
using TShockAPI.DB;
using System.ComponentModel;

namespace EpicDiseases
{
    [APIVersion(1, 11)]
    public class EpicDiseases : TerrariaPlugin
    {
        public override string Name
        {
            get { return "Epic Diseases DEV"; }
        }

        public override string Author
        {
            get { return "Spectrewiz"; }
        }

        public override string Description
        {
            get { return "So far theres pretty much nothing that goes on."; }
        }

        public override Version Version
        {
            get { return Assembly.GetExecutingAssembly().GetName().Version; }
        }

        public static List<Player> Players = new List<Player>();
        public override void Initialize()
        {
            GameHooks.Update += OnUpdate;
            GameHooks.Initialize += OnInitialize;
            NetHooks.GreetPlayer += OnGreetPlayer;
            ServerHooks.Leave += OnLeave;
            ServerHooks.Chat += OnChat;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                GameHooks.Update -= OnUpdate;
                GameHooks.Initialize -= OnInitialize;
                NetHooks.GreetPlayer -= OnGreetPlayer;
                ServerHooks.Leave -= OnLeave;
                ServerHooks.Chat -= OnChat;
            }
            base.Dispose(disposing);
        }

        public EpicDiseases(Main game)
            : base(game)
        {
        }

        public void OnInitialize()
        {
            bool infect = false;

            foreach (Group group in TShock.Groups.groups)
            {
                if (group.Name != "superadmin")
                {
                    if (group.HasPermission("Infect"))
                        infect = true;
                }
            }

            List<string> permlist = new List<string>();
            if (!infect)
                permlist.Add("Infect");
            TShock.Groups.AddPermissions("trustedadmin", permlist);

            Commands.ChatCommands.Add(new Command("Infect", Infect, "infect"));
        }

        public void OnUpdate()
        {
        }

        public void OnGreetPlayer(int who, HandledEventArgs e)
        {
            lock (Players)
                Players.Add(new Player(who));
        }

        public void OnLeave(int ply)
        {
            lock (Players)
            {
                for (int i = 0; i < Players.Count; i++)
                {
                    if (Players[i].Index == ply)
                    {
                        Players.RemoveAt(i);
                        break; //Found the player, break.
                    }
                }
            }
        }

        public void OnChat(messageBuffer msg, int ply, string text, HandledEventArgs e)
        {
        }

        public static void Help(CommandArgs args)
        {
            args.Player.SendMessage("This plugin is completely evil", Color.DarkGray);
        }

        public static void Rules(CommandArgs args)
        {
            args.Player.SendMessage("I am still wondering if this is going to be a Supernatural Plugin, or a Black Plague Plugin.", Color.DarkRed);
        }

        public static void InfectPlayer(string to)
        {
            var playername = Player.GetPlayerByName(to);
            if (TShock.Utils.FindPlayer(to).Count > 0)
            {
                playername.SetState(Infected.Infected);
                foreach (Player player in EpicDiseases.Players)
                {
                    player.TSPlayer.SendMessage("A foul odor fills the air.", Color.DarkGray);
                }
            }
        }

        public static void Infect(CommandArgs args)
        {
            string cmd = "help";
            if (args.Parameters.Count > 0)
            {
                cmd = args.Parameters[0].ToLower();
            }
            switch (cmd)
            {
                case "help":
                    Help(args);
                    break;
                case "rules":
                    Rules(args);
                    break;
                case "iplayer":
                    try
                    {
                        InfectPlayer(args.Parameters[1]);
                        args.Player.SendMessage(string.Format("You stir up Black Magic, and the foul plague sweeps through mankind."), Color.DarkGray);
                    }
                    catch (Exception e)
                    {
                        args.Player.SendMessage(string.Format("Cannot find {0}", args.Parameters[1]), Color.Red);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                    }
                    break;
                case "infectplayer":
                    try
                    {
                        InfectPlayer(args.Parameters[1]);
                        args.Player.SendMessage(string.Format("You stir up Black Magic, and the foul plague sweeps through mankind."), Color.DarkGray);
                    }
                    catch (Exception e)
                    {
                        args.Player.SendMessage(string.Format("Cannot find {0}", args.Parameters[1]), Color.Red);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine(e.Message);
                        Console.ResetColor();
                    }
                    break;
                case "cplayer":

                    break;
                case "cureplayer":

                    break;
                default:
                    Help(args);
                    break;
            }
        }
    }
}