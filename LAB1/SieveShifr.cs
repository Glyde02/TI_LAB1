using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB1
{
    class SieveShifr : Shifr
    {
        private const int size = 5;
        bool[,] keyMatrix = new bool[size, size];
        char[,] textMatrix = new char[size, size];

        public override string Code(string text, string Key)
        {
            int i, j, k, index = 0;
            keyMatrix = KeyToMatrix(Key);
            string outText = "";

            while (text.Length % (size * size) != 0)
                text += ' ';

            for (k = 0; k < text.Length / (size * size); k++)
            {
                MakeRotation(true, text, ref index, ref outText);
                for (i = 0; i < size; i++)
                    for (j = 0; j < size; j++)
                        outText += textMatrix[i, j];
            }
            return outText;
        }

        public override string Decode(string text, string Key)
        {
            while (text.Length % (size * size) != 0)
                text += ' ';

            int i, j, k, index = 0;
            string outText = "";
            keyMatrix = KeyToMatrix(Key);



            for (k = 0; k < text.Length / (size * size); k++)
            {
                for (i = 0; i < size; i++)
                    for (j = 0; j < size; j++)
                    {
                        textMatrix[i, j] = text[index++];
                    }

                MakeRotation(false, text, ref index, ref outText);

            }

            return outText;
        }

        private bool[,] KeyToMatrix(string key)
        {
            bool[,] bufMatrix = new bool[5, 5];
            int i = 0, j = 0, k;
            int len = key.Length;

            for (k = 0; k < len; k++)
            {
                if (key[k] == '1')
                    bufMatrix[i, j] = true;
                else
                    bufMatrix[i, j] = false;

                if (j < 4)
                    j++;
                else
                {
                    j = 0;
                    i++;
                }
            }

            return bufMatrix;
        }

        private void MakeRotation(bool isCode, string text, ref int index, ref string outText)
        {
            int i, j;

            keyMatrix[2, 2] = true;

            for (i = 0; i < size; i++)
                for (j = 0; j < size; j++)
                {
                    if (keyMatrix[i, j])
                        if (isCode)
                            textMatrix[i, j] = text[index++];
                        else
                            outText += textMatrix[i, j];
                }

            keyMatrix[2, 2] = false;

            for (j = 0; j < size; j++)
                for (i = size - 1; i >= 0; i--)
                {
                    if (keyMatrix[i, j])
                        if (isCode)
                            textMatrix[j, 4 - i] = text[index++];
                        else
                            outText += textMatrix[j, size - 1 - i];
                }

            for (i = size - 1; i >= 0; i--)
                for (j = size - 1; j >= 0; j--)
                {
                    if (keyMatrix[i, j])
                        if (isCode)
                            textMatrix[4 - i, 4 - j] = text[index++];
                        else
                            outText += textMatrix[size - 1 - i, size - 1 - j];
                }

            for (j = size - 1; j >= 0; j--)
                for (i = 0; i < size; i++)
                {
                    if (keyMatrix[i, j])
                        if (isCode)
                            textMatrix[4 - j, i] = text[index++];
                        else
                            outText += textMatrix[size - 1 - j, i];
                }
        }

    }
}
