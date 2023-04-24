using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taptiles.Core
{
    [Serializable]
    public class Tile
    {

        public Tile(int value)
        {
            Value = value;
        }

        public int Value { get; set; }
    }
}
