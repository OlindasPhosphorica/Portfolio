using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// Inherits from Item class. Represents a health potion that can be used to restore health.
    /// </summary>
    public class HealthPot : Item
    {
        /// <summary>
        /// Allows the player to use the health potion, restoring health.
        /// </summary>
        /// <param name="Damage"></param>
        /// <param name="HealthVal"></param>
        public override void UseItem(float Damage, float HealthVal)
        {
            Console.WriteLine($"You used {Name} and restored {HealthVal} health.");
        }

        /// <summary>
        /// drops the health potion from the player's inventory.
        /// </summary>
        public override void DropItem()
        {
            Console.WriteLine($"You dropped the {Name}.");
        }
        /// <summary>
        /// Sets the health potion's properties including name, description, position, and symbol.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="symbol"></param>

        public HealthPot(string name, int x, int y, char symbol = 'H')
            : base(x, y, symbol)
        {
            Name = "Health Potion";
            Description = "A potion that restores health.";
        }
    }
}
