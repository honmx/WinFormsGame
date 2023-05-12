using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Entities;
using WinForms_game.Properties;

namespace WinForms_game.Controllers
{
    internal static class BoxController
    {
        public static List<Box> boxes;
        public static void InitBoxes()
        {
            var boxesOnMap = GameController.GetByNumber(2);
            boxes = new List<Box>();

            for (var i = 0; i < boxesOnMap.Count; i++)
            {
                boxes.Add(new Box(
                    boxesOnMap[i],
                    Resources.Box
                ));
            }
        }

        public static List<Box> GetBoxes()
        {
            return boxes;
        }
    }
}
