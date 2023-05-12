using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Entities;
using WinForms_game.Properties;

namespace WinForms_game.Controllers
{
    internal static class WallsController
    {
        static List<Wall> walls;

        public static void InitWalls()
        {
            var wallsOnMap = GameController.GetByNumber(1);
            walls = new List<Wall>();

            for (var i = 0; i < wallsOnMap.Count; i++)
            {
                walls.Add(new Wall(
                    wallsOnMap[i],
                    Resources.Wall
                ));
            }
        }

        public static List<Wall> GetWalls()
        {
            return walls;
        }
    }
}
