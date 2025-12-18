using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// Abstract base class for items in the game, including weapons and health potions.
    /// </summary>
    public abstract class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public char Symbol { get; set; }
        public abstract void UseItem(float Damage, float HealthVal);
        public abstract void DropItem();

        /// <summary>
        /// Sets the position and symbol of the item.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="symbol"></param>
        public Item(int x, int y, char symbol) 
        {
            X = x;
            Y = y;
            Symbol = symbol;

        }
    }
}
