using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB1
{
    class CezarShifr : Shifr
    {
        public override string Code(string text, string key)
        {
            int shift = KeyToInt(key);
            int i;
            char[] outTextArr = new char[text.Length];

            for (i = 0; i < text.Length; i++)
            {
                if (text[i] >= 'A' && text[i] <= 'Z')
                {
                    outTextArr[i] = (char)((text[i] + shift - 'A') % ('Z' - 'A' + 1) + 'A');
                    if (outTextArr[i] < 'A')
                        outTextArr[i] = (char)(outTextArr[i] + ('Z' - 'A' + 1));
                }
                else if (text[i] >= 'a' && text[i] <= 'z')
                {
                    outTextArr[i] = (char)((text[i] + shift - 'a') % ('Z' - 'A' + 1) + 'a');
                    if (outTextArr[i] < 'a')
                        outTextArr[i] = (char)(outTextArr[i] + ('Z' - 'A' + 1));
                }
                else if (text[i] >= 'А' && text[i] <= 'Я')
                {
                    outTextArr[i] = (char)((text[i] + shift - 'А') % ('Я' - 'А' + 1) + 'А');
                    if (outTextArr[i] < 'А')
                        outTextArr[i] = (char)(outTextArr[i] + ('Я' - 'А' + 1));
                }
                else if (text[i] >= 'а' && text[i] <= 'я')
                {
                    outTextArr[i] = (char)((text[i] + shift - 'а') % ('Я' - 'А' + 1) + 'а');
                    if (outTextArr[i] < 'а')
                        outTextArr[i] = (char)(outTextArr[i] + ('Я' - 'А' + 1));
                }
                else
                    outTextArr[i] = text[i];

            }

            string outText = new string(outTextArr);
            return outText;
        }


        public override string Decode(string text, string key)
        {
            int shift = KeyToInt(key);
            if (shift < 0)
            {
                return Code(text, key.Substring(1));
            }
            else
                return Code(text, '-' + key);
        }

    }
}
