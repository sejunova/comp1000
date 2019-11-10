﻿using System.Collections.Generic;
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

            MakeStepsRecursive(allSteps, noise, 0, exitPoints);
            return allSteps;
        }

        private static void MakeStepsRecursive(List<int> allSteps, INoise noise, int level, Dictionary<Tuple<int, int>, int> exitPoints)
        {
            List<int> steps = new List<int>(allSteps);

            int insertIndex = 1;
            for (int i = 0; i < steps.Count - 1; i++)
            {
                if (Math.Abs(steps[i] - steps[i + 1]) > 10)
                {
                    Tuple<int, int> key = new Tuple<int, int>(steps[i], steps[i + 1]);
                    if (level != 0)
                    {
                        if (exitPoints.ContainsKey(key))
                        {
                            level = exitPoints[key] + 1;
                        }
                        else
                        {
                            exitPoints[key] = -1;
                            level = 0;
                        }
                    }

                    int p1 = (int)((1 - 0.2) * steps[i] + 0.2 * steps[i + 1]) + noise.GetNext(level);
                    int p2 = (int)((1 - 0.4) * steps[i] + 0.4 * steps[i + 1]) + noise.GetNext(level);
                    int p3 = (int)((1 - 0.6) * steps[i] + 0.6 * steps[i + 1]) + noise.GetNext(level);
                    int p4 = (int)((1 - 0.8) * steps[i] + 0.8 * steps[i + 1]) + noise.GetNext(level);

                    allSteps.Insert(insertIndex++, p1);
                    allSteps.Insert(insertIndex++, p2);
                    allSteps.Insert(insertIndex++, p3);
                    allSteps.Insert(insertIndex++, p4);

                    exitPoints[new Tuple<int, int>(steps[i], p1)] = level;
                    exitPoints[new Tuple<int, int>(p1, p2)] = level;
                    exitPoints[new Tuple<int, int>(p2, p3)] = level;
                    exitPoints[new Tuple<int, int>(p3, p4)] = level;
                    exitPoints[new Tuple<int, int>(p4, steps[i + 1])] = level;
                    MakeStepsRecursive(allSteps, noise, level + 1, exitPoints);
                    break;
                }
                insertIndex++;
            }
        }
    }
}
