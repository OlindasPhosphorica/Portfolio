using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// Monster class implementing ICharacter interface.
    /// </summary>
    public class Monster : ICharacter
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public string MonsterType { get; set; }
        public char Symbol { get; set; } = 'M';
        public int X { get; set; }
        public int Y { get; set; }
        

        /// <summary>
        /// Constructor to set the monster's name, health, and type.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="health"></param>
        /// <param name="monsterType"></param>
        public Monster(string name, int health, string monsterType) 
        {
            Name = name;
            Health = 80;
            MonsterType = "Ogre";
        }
        /// <summary>
        /// Sets the monster's health after taking damage.
        /// </summary>
        /// <param name="amount"></param>
        public void Damage(int amount)
        {
            Health -= amount;
            if (Health < 0) Health = 0;
            Console.WriteLine($"{Name} the {MonsterType} took {amount} damage and has {Health} health left.");
        }
        /// <summary>
        /// Sets the monster to attack a target character, dealing random damage between 20 and 40.
        /// </summary>
        /// <param name="target"></param>
        public void Attack(ICharacter target)
        {
            Random rand = new Random();
            int damage = rand.Next(20, 40);
            Console.WriteLine($"{Name} the {MonsterType} attacks {target.Name} for {damage} damage!");
            target.Damage(damage);
        }
    }
}
