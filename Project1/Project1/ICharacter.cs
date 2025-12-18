using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    /// <summary>
    /// Interface for characters in the game, including properties and methods for name, health, position, symbol, attacking, and taking damage.
    /// </summary>
    public interface ICharacter
    {
        string Name { get; set; }
        int Health { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        char Symbol { get; set; }
        void Attack(ICharacter target);
        void Damage(int amount);

    }
}
