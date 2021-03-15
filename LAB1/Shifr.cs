using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB1
{
    abstract class Shifr
    {
        public abstract string Code(string text, string key);
        public abstract string Decode(string text, string key);

        public int KeyToInt(string key)
        {
            return Int32.Parse(key);
        }
    }
}