using System;
using System.Collections.Generic;

namespace Lab11
{
    public static class FrequencyTable
    {
        public static List<Tuple<Tuple<int, int>, int>> GetFrequencyTable(int[] data, int maxBinCount)
        {
            if (data.Length == 0 || maxBinCount <= 0)
            {
                return new List<Tuple<Tuple<int, int>, int>>();
            }

            List<Tuple<Tuple<int, int>, int>> ret = new List<Tuple<Tuple<int, int>, int>>();

            int min = data[0];
            int max = data[0];

            for (int i = 1; i < data.Length; i++)
            {
                if (data[i] > max)
                {
                    max = data[i];
                }
                if (data[i] < min)
                {
                    min = data[i];
                }
            }

            int tableCount;
            int interval;
            int start;
            int end;
            int index;
            int[] tableValues;

            double tempInterval = (double)(max - min) / maxBinCount;
            interval = (int)tempInterval + 1;
            if (((max + 1) - min) % interval == 0)
            {
                tableCount = ((max + 1) - min) / interval;
            }
            else
            {
                tableCount = ((max + 1) - min) / interval + 1;
            }
            tableValues = new int[tableCount];
            for (int i = 0; i < data.Length; i++)
            {
                index = (data[i] - min) / interval;
                tableValues[index] += 1;
            }

            start = min;
            end = min + interval;
            for (int i = 0; i < tableValues.Length; i++)
            {
                ret.Add(new Tuple<Tuple<int, int>, int>(new Tuple<int, int>(start, end), tableValues[i]));
                start = end;
                end += interval;
            }
            return ret;
        }
    }
}
