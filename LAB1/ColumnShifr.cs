using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB1
{
    class ColumnShifr : Shifr
    {
        override public string Code(string text, string key)
        {
            int len = key.Length;
            int[] numbers = PrepairMass(key);
            string outText = "";
            int i, num;

            for (i = 0; i < len; i++)
            {
                num = numbers[i];
                while (num < text.Length)
                {
                    outText += text[num];
                    num += len;
                }

            }

            return outText;

        }

        public override string Decode(string text, string key)
        {
            int len = key.Length;
            int[] numbers = PrepairMass(key);
            char[] outText_Char = new char[text.Length];
            int i, j = 0;
            int num;

            for (i = 0; i < len; i++)
            {
                num = numbers[i];
                while (num < text.Length)
                {
                    outText_Char[num] = text[j];
                    j++;
                    num += len;
                }
            }


            string outText = new string(outText_Char);
            return outText;
        }

        private int[] PrepairMass(string key)
        {
            int len = key.Length;
            int[] numbers = new int[len];
            int[] ASCII_key = new int[len];
            int minPos;
            int i, j;

            i = 0;
            foreach (char c in key)
            {
                ASCII_key[i] = (int)c;
                i++;
            }

            for (i = 0; i < len; i++)
            {
                j = 0;
                while (ASCII_key[j] == 0)
                {
                    j++;
                }
                minPos = j;

                for (j = minPos; j < len; j++)
                {
                    if (ASCII_key[j] != 0 && ASCII_key[j] < ASCII_key[minPos])
                    {
                        minPos = j;
                    }
                }

                numbers[i] = minPos;
                ASCII_key[minPos] = 0;
            }
            return numbers;

        }
    }
}
