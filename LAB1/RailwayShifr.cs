using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB1
{
    class RailwayShifr : Shifr
    {
        private int totalLevels;
        private List<string> levels = new List<string>();

        private void InitData(string strTotalLevels)
        {
            totalLevels = KeyToInt(strTotalLevels);
            for (int i = 0; i < totalLevels; i++)
            {
                levels.Add("");
            }
        }

        override public string Code(string text, string strTotalLevels)
        {
            InitData(strTotalLevels);


            int len = text.Length;
            int level;
            int period;
            period = 2 * (totalLevels - 1);

            for (int i = 0; i < len; i++)
            {
                level = totalLevels - 1 - Math.Abs(totalLevels - 1 - (i % period));
                levels[level] += text[i];
            }

            string outText = "";
            for (int i = 0; i < totalLevels; i++)
                outText += levels[i];

            return outText;
        }

        override public string Decode(string text, string strTotalLevels)
        {
            InitData(strTotalLevels);

            int len = text.Length;
            int lastKol = 0;
            int level;
            int pos = 0;

            string outText = "";

            int period;
            period = 2 * (totalLevels - 1);

            bool isUp = false;
            if (len % period > totalLevels || len % period == 0)
                isUp = true;
            bool IsInc = false;
            bool notMoreInc = false;

            for (int i = 0; i < totalLevels; i++)
            {
                if (i == 0)
                {
                    lastKol = (len - 1) / period + 1;
                }
                else if (i == totalLevels - 1)
                {
                    lastKol = len - pos;
                }
                else
                {
                    level = len % period;
                    if (level == 0)
                        level = period;
                    if (!notMoreInc && !IsInc && isUp && period - level + 1 <= i)
                    {
                        IsInc = true;
                    }

                    if (i == level && !isUp)
                    {
                        lastKol--;
                    }

                    if (i - 1 == 0)
                        lastKol = (lastKol - 1) * 2 + 1;

                    if (IsInc)
                    {
                        lastKol++;
                        IsInc = false;
                        notMoreInc = true;
                    }
                }



                for (int j = 0; j < lastKol; j++)
                {
                    levels[i] += text[pos];
                    pos++;
                }
            }

            int k = 0;
            isUp = false;
            while (levels[k] != "")
            {
                outText += levels[k][0];
                levels[k] = levels[k].Substring(1);
                if (isUp)
                {
                    k--;
                    if (k == 0)
                        isUp = false;
                }
                else
                {
                    k++;
                    if (k == totalLevels - 1)
                        isUp = true;

                }


            }

            return outText;
        }


    }
}
