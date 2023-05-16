using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Entities;

namespace WinForms_game.Controllers
{
    internal static class PlayerController
    {
        static List<Player> players;
        private static List<List<Keys>> playersKeys;

        public static void InitPlayers()
        {
            playersKeys = new List<List<Keys>>(
                new List<Keys>[1]
                {
                    new List<Keys>( new Keys[5] { Keys.W, Keys.D, Keys.S, Keys.A, Keys.Space })
                }
            );

            players = new List<Player>();
            var playersOnMap = GameController.GetByNumber(9);

            for (var i = 0; i < playersOnMap.Count; i++)
            {
                players.Add(new Player(
                    playersKeys[i],
                    playersOnMap[i],
                    i + 1,
                    playersOnMap[i].X < 1280 / 2 ? 1 : -1
                ));
            }
        }

        public static List<Player> GetPlayers()
        {
            return players;
        }
    }
}
