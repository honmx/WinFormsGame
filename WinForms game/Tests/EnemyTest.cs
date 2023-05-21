using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinForms_game.Entities;

namespace WinForms_game.Tests
{
    [TestFixture]
    public class EnemyTest
    {
        Enemy enemy;

        [SetUp]
        public void SetUp()
        {
            enemy = new Enemy(new PointF(250, 250));
        }

        [Test]
        public void MoveTest()
        {
            enemy.Move();

            Assert.AreEqual(255, enemy.physics.transform.position.X);
        }

        [Test]
        public void GetHitTest()
        {
            enemy.GetHit();

            Assert.AreEqual(75, enemy.hp);
        }
    }
}
