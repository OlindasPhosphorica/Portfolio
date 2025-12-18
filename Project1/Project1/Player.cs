using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// Player class implementing ICharacter interface.
    /// </summary>
    public class Player : ICharacter
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public char Symbol { get; set; } = 'P';
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Sets the player's name and health.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        public Player(string name, int health)
        {
            Name = name;
            Health = health;

        }
        /// <summary>
        /// Sets the player's health after taking damage.
        /// </summary>
        /// <param name="amount"></param>
        public void Damage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
            Console.WriteLine($"{Name} took {amount} damage and has {Health} health left.");
        }
        /// <summary>
        /// Simulates an attack on a target character, dealing random damage between 10 and 30.
        /// </summary>
        /// <param name="target"></param>
        public void Attack(ICharacter target)
        {
            Random rand = new Random();
            int damage = rand.Next(10, 30);
            Console.WriteLine($"{Name} attacks {target.Name} for {damage} damage!");
            target.Damage(damage);
        }
    }
}
