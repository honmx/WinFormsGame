using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using WinForms_game.Entities;
using WinForms_game.Properties;

namespace WinForms_game.Tests
{

    [TestFixture]
    public class PlayerTest
    {
        Player player;

        [SetUp]
        public void SetUp()
        {
            player = new Player(
                new List<Keys>() { Keys.W, Keys.D, Keys.S, Keys.A, Keys.Space },
                new PointF(500, 250),
                1,
                1
            );
        }

        [Test]
        public void MovingTest()
        {
            player.Move(15);

            Assert.AreEqual(515, player.physics.transform.position.X);
        }

        [Test]
        public void JumpingTest()
        {
            player.Jump();

            Assert.AreNotEqual(250, player.physics.transform.position.Y);
        }

        [Test]
        public void GetHitTest()
        {
            player.GetHit();

            Assert.AreEqual(90, player.GetHp());
        }

        [Test]
        public void HealTest()
        {
            player.hp = 50;

            player.Heal();

            Assert.AreEqual(100, player.GetHp());
        }

        [Test]
        public void CollisionTest()
        {
            var enemy = new Enemy(new PointF(500, 250));

            Assert.AreEqual(true, player.physics.isCollided(enemy.physics.transform.position, enemy.physics.transform.size));
        }
    }
}