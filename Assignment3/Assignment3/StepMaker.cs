using System.Collections.Generic;
using System.Linq;
using System;

namespace Assignment3
{
    public static class StepMaker
    {
        public static List<int> MakeSteps(int[] steps, INoise noise)
        {
            List<int> allSteps = steps.ToList();
            Dictionary<Tuple<int, int>, int> exitPoints = new Dictionary<Tuple<int, int>, int>();

            for (int i = 0; i < allSteps.Count - 1; i++)
            {
                Tuple<int, int> key = new Tuple<int, int>(allSteps[i], allSteps[i + 1]);
                exitPoints[key] = 0;
            }
            makeStepsRecursive(allSteps, noise, exitPoints);
            return allSteps;
        }

        private static int lerp(decimal step1, decimal step2, double point)
        {
            return (int)(step1 + (step2 - step1) * (decimal)point);
        }

        private static void makeStepsRecursive(List<int> allSteps, INoise noise, Dictionary<Tuple<int, int>, int> exitPoints)
        {
            List<int> steps = new List<int>(allSteps);

            int insertIndex = 1;
            for (int i = 0; i < steps.Count - 1; i++)
            {
                if (Math.Abs(steps[i] - steps[i + 1]) > 10)
                {
                    Tuple<int, int> key = new Tuple<int, int>(steps[i], steps[i + 1]);
                    int level = exitPoints[key];

                    decimal step1 = (decimal)steps[i];
                    decimal step2 = (decimal)steps[i + 1];
                    int p1 = lerp(step1, step2, 0.2) + noise.GetNext(level);
                    int p2 = lerp(step1, step2, 0.4) + noise.GetNext(level);
                    int p3 = lerp(step1, step2, 0.6) + noise.GetNext(level);
                    int p4 = lerp(step1, step2, 0.8) + noise.GetNext(level);

                    allSteps.Insert(insertIndex++, p1);
                    allSteps.Insert(insertIndex++, p2);
                    allSteps.Insert(insertIndex++, p3);
                    allSteps.Insert(insertIndex++, p4);

                    exitPoints[new Tuple<int, int>(steps[i], p1)] = level + 1;
                    exitPoints[new Tuple<int, int>(p1, p2)] = level + 1;
                    exitPoints[new Tuple<int, int>(p2, p3)] = level + 1;
                    exitPoints[new Tuple<int, int>(p3, p4)] = level + 1;
                    exitPoints[new Tuple<int, int>(p4, steps[i + 1])] = level + 1;
                    makeStepsRecursive(allSteps, noise, exitPoints);
                    break;
                }
                insertIndex++;
            }
        }
    }
}
