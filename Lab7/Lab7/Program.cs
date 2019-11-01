using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Frame frame1 = new Frame(1, "Ray-Ban");

            frame1.ToggleFeatures(EFeatures.Aviator | EFeatures.Red);
            Debug.Assert(frame1.Features == (EFeatures.Aviator | EFeatures.Red));

            frame1.ToggleFeatures(EFeatures.Aviator);
            Debug.Assert(frame1.Features == EFeatures.Red);

            frame1.TurnOffFeatures(EFeatures.Aviator | EFeatures.Red);
            Debug.Assert(frame1.Features == 0);

            frame1.TurnOnFeatures(EFeatures.Blue | EFeatures.Black);
            Debug.Assert(frame1.Features == (EFeatures.Blue | EFeatures.Black));

            frame1.TurnOnFeatures(EFeatures.Men | EFeatures.Women);
            Debug.Assert(frame1.Features == (EFeatures.Blue | EFeatures.Black | EFeatures.Men | EFeatures.Women));

            List<Frame> frames = new List<Frame>
            {
                new Frame(2, "Joseph-Marc"),
                new Frame(3, "Derek Cardigan"),
                new Frame(4, "Randy Jackson"),
                new Frame(5, "Evergreen"),
                new Frame(6, "Emporio Armani"),
                new Frame(7, "Carrera"),
                new Frame(8, "Crocs")
            };

            frames[0].TurnOnFeatures(EFeatures.Men | EFeatures.Women | EFeatures.Rectangle | EFeatures.Blue);
            frames[1].TurnOnFeatures(EFeatures.Women | EFeatures.Black);
            frames[2].TurnOnFeatures(EFeatures.Aviator | EFeatures.Red | EFeatures.Black);
            frames[3].TurnOnFeatures(EFeatures.Round);
            frames[4].TurnOnFeatures(EFeatures.Round | EFeatures.Red);
            frames[5].TurnOnFeatures(EFeatures.Men | EFeatures.Blue | EFeatures.Black);
            frames[6].TurnOnFeatures(EFeatures.Black);

            List<Frame> filteredFrames = FilterEngine.FilterFrames(frames, EFeatures.Men);

            Debug.Assert(filteredFrames.Count == 2);
            Debug.Assert(filteredFrames[0].ID == frames[0].ID);
            Debug.Assert(filteredFrames[1].ID == frames[5].ID);

            filteredFrames = FilterEngine.FilterFrames(frames, EFeatures.Men | EFeatures.Red | EFeatures.Aviator);
            Debug.Assert(filteredFrames.Count == 4);
            Debug.Assert(filteredFrames[0].ID == frames[0].ID);
            Debug.Assert(filteredFrames[1].ID == frames[2].ID);
            Debug.Assert(filteredFrames[2].ID == frames[4].ID);
            Debug.Assert(filteredFrames[3].ID == frames[5].ID);

            List<Frame> filteredOutFrames = FilterEngine.FilterOutFrames(frames, EFeatures.Aviator | EFeatures.Women | EFeatures.Red);
            Debug.Assert(filteredOutFrames.Count == 3);
            Debug.Assert(filteredOutFrames[0].ID == frames[3].ID);
            Debug.Assert(filteredOutFrames[1].ID == frames[5].ID);
            Debug.Assert(filteredOutFrames[2].ID == frames[6].ID);

            List<Frame> frames2 = new List<Frame>
            {
                new Frame(9, "Kam Dhillon"),
                frames[0],
                frames[3],
                new Frame(10, "Dior"),
                new Frame(11, "Calvin Klein"),
                frames[5],
                frames[6],
                new Frame(12, "Lacoste"),
            };

            List<Frame> intersect = FilterEngine.Intersect(frames, frames2);

            Debug.Assert(intersect.Count == 4);
            Debug.Assert(intersect.FirstOrDefault(i => i.ID == frames[0].ID) != null);
            Debug.Assert(intersect.FirstOrDefault(i => i.ID == frames[3].ID) != null);
            Debug.Assert(intersect.FirstOrDefault(i => i.ID == frames[5].ID) != null);
            Debug.Assert(intersect.FirstOrDefault(i => i.ID == frames[6].ID) != null);

            List<int> sortKeys = FilterEngine.GetSortKeys(frames, new List<EFeatures> { EFeatures.Aviator, EFeatures.Men, EFeatures.Rectangle, EFeatures.Red });
            Debug.Assert(sortKeys.Count == frames.Count);

            List<Frame> sortedFrames = sort(sortKeys, frames);

            Debug.Assert(sortedFrames[0].ID == frames[2].ID);
            Debug.Assert(sortedFrames[1].ID == frames[0].ID);
            Debug.Assert(sortedFrames[2].ID == frames[5].ID);
            Debug.Assert(sortedFrames[3].ID == frames[4].ID);
            Debug.Assert(sortedFrames[4].ID == frames[1].ID || sortedFrames[4].ID == frames[3].ID || sortedFrames[4].ID == frames[6].ID);
            Debug.Assert(sortedFrames[5].ID == frames[1].ID || sortedFrames[5].ID == frames[3].ID || sortedFrames[5].ID == frames[6].ID);
            Debug.Assert(sortedFrames[6].ID == frames[1].ID || sortedFrames[6].ID == frames[3].ID || sortedFrames[6].ID == frames[6].ID);

            sortKeys = FilterEngine.GetSortKeys(frames, new List<EFeatures> { EFeatures.Rectangle, EFeatures.Black, EFeatures.Women });
            Debug.Assert(sortKeys.Count == frames.Count);

            sortedFrames = sort(sortKeys, frames);

            Debug.Assert(sortedFrames[0].ID == frames[0].ID);
            Debug.Assert(sortedFrames[1].ID == frames[1].ID);
            Debug.Assert(sortedFrames[2].ID == frames[2].ID || sortedFrames[2].ID == frames[5].ID || sortedFrames[2].ID == frames[6].ID);
            Debug.Assert(sortedFrames[3].ID == frames[2].ID || sortedFrames[3].ID == frames[5].ID || sortedFrames[3].ID == frames[6].ID);
            Debug.Assert(sortedFrames[4].ID == frames[2].ID || sortedFrames[4].ID == frames[5].ID || sortedFrames[4].ID == frames[6].ID);
            Debug.Assert(sortedFrames[5].ID == frames[3].ID || sortedFrames[5].ID == frames[4].ID);
            Debug.Assert(sortedFrames[6].ID == frames[3].ID || sortedFrames[6].ID == frames[4].ID);
        }

        private static List<Frame> sort(List<int> sortKeys, List<Frame> frames)
        {
            List<Tuple<int, Frame>> tuples = new List<Tuple<int, Frame>>();
            for (int i = 0; i < sortKeys.Count; i++)
            {
                tuples.Add(new Tuple<int, Frame>(sortKeys[i], frames[i]));
            }

            tuples.Sort((t1, t2) =>
            {
                return t2.Item1 - t1.Item1;
            });

            return tuples.Select(t => t.Item2).ToList();
        }
    }
}
