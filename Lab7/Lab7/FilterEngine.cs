using System.Collections.Generic;

namespace Lab7
{
    class FilterEngine
    {
        public static List<Frame> FilterFrames(List<Frame> frames, EFeatures features)
        {
            List<Frame> filteredFrames = new List<Frame>();

            foreach (Frame frame in frames)
            {
                if ((frame.Features & features) != 0)
                {
                    filteredFrames.Add(frame);
                }
            }
            return filteredFrames;
        }
        public static List<Frame> FilterOutFrames(List<Frame> frames, EFeatures features)
        {
            List<Frame> filteredOutFrames = new List<Frame>();

            foreach (Frame frame in frames)
            {
                if ((features & ~frame.Features) == features)
                {
                    filteredOutFrames.Add(frame);
                }
            }
            return filteredOutFrames;
        }
        public static List<Frame> Intersect(List<Frame> frames1, List<Frame> frames2)
        {
            List<Frame> intersect = new List<Frame>();

            foreach (Frame frame1 in frames1)
            {
                foreach (Frame frame2 in frames2)
                {
                    if (frame1.ID == frame2.ID && frame1.Name == frame2.Name && frame1.Features == frame2.Features)
                    {
                        intersect.Add(frame1);
                    }
                }
            }
            return intersect;
        }
        public static List<int> GetSortKeys(List<Frame> frames, List<EFeatures> features)
        {
            List<int> keys = new List<int>();
            foreach (Frame frame in frames)
            {
                int key = 0;
                for (int i = 0; i < features.Count; i++)
                {
                    if ((frame.Features & features[i]) != 0)
                    {
                        key |= 1 << (features.Count - i);
                    }
                }
                keys.Add(key);
            }
            return keys;
        }
    }
}
