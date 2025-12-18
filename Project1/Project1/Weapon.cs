using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// Weapon class inheriting from Item, representing a weapon with damage and health values.
    /// </summary>
    public class Weapon : Item
    {
        public new string Name { get; set; }
        public new string Description { get; set; }
        public float Damage { get; set; }
        public float HealthVal { get; set; }
        public new char Symbol { get; set; } = 'W';
        public new int X { get; set; }
        public new int Y { get; set; }

        /// <summary>
        /// Lets the player use the weapon, dealing damage.
        /// </summary>
        /// <param name="Damage"></param>
        /// <param name="HealthVal"></param>
        public override void UseItem(float Damage, float HealthVal)
        {
            Console.WriteLine($"You used the {Name} and dealt {Damage} damage.");
        }

        /// <summary>
        /// Drops the weapon from the player's inventory.
        /// </summary>
        public override void DropItem()
        {
            Console.WriteLine($"You dropped the {Name}.");
        }

        /// <summary>
        /// Sets the weapon's properties including name, description, damage, health value, position, and symbol.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <param name="damage"></param>
        /// <param name="healthVal"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="symbol"></param>
        public Weapon(string name, string description, float damage, float healthVal, int x, int y, char symbol = 'W')
            : base(x, y, symbol)
        {
            Name = name;
            Description = description;
            Damage = damage;
            HealthVal = healthVal;
            X = x;
            Y = y;
            Symbol = symbol;
        }
    }
}
