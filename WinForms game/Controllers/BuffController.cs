using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Entities;
using WinForms_game.Properties;

namespace WinForms_game.Controllers
{
    internal static class BuffController
    {
        public static List<Buff> buffs;
        private static List<PointF> buffSpots;
        private static List<Tuple<string, Image>> availableBuffs;

        public static void InitBuffs()
        {
            buffs = new List<Buff>();
            buffSpots = GameController.GetByNumber(7);

            availableBuffs = new List<Tuple<string, Image>>()
            {
                new Tuple<string, Image>( "health", Resources.HealthPotion ),
                new Tuple<string, Image>( "strength", Resources.StrengthPotion )
            };
        }

        public static void SpawnBuffWithChance(double chanceInPercents)
        {
            if (buffs.Count == buffSpots.Count) return;

            var random = new Random();

            if (random.Next(100) > chanceInPercents) return;

            var randomSpotIndex = random.Next(buffSpots.Count);
            var randomSpriteIndex = random.Next(availableBuffs.Count);

            for (var i = 0; i < buffs.Count; i++)
            {
                var currentBuff = buffs[i];

                if (currentBuff.physics.transform.position.X == buffSpots[randomSpotIndex].X)
                {
                    SpawnBuffWithChance(chanceInPercents);
                    return;
                }
            }

            var buff = new Buff(
                buffSpots[randomSpotIndex],
                availableBuffs[randomSpriteIndex].Item1,
                availableBuffs[randomSpriteIndex].Item2
            );

            buffs.Add(buff);
        }

        public static void RemoveBuff(Buff buff)
        {
            buffs.Remove(buff);
        }

        public static List<Buff> GetBuffs()
        {
            return buffs;
        }
    }
}
