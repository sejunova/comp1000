using System;
using System.Collections.Generic;

namespace Lab4
{
    public sealed class MultiSet
    {
        private Dictionary<string, uint> mElements;

        public MultiSet()
        {
            mElements = new Dictionary<string, uint>();
        }

        public void Add(string element)
        {
            if (mElements.ContainsKey(element))
            {
                mElements[element]++;
            }
            else
            {
                mElements[element] = 1;
            }
        }

        public bool Remove(string element)
        {
            if (!mElements.ContainsKey(element))
            {
                return false;
            }
            if (mElements[element] == 1)
            {
                mElements.Remove(element);
            }
            else
            {
                mElements[element]--;
            }
            return true;
        }

        public uint GetMultiplicity(string element)
        {
            if (!mElements.ContainsKey(element))
            {
                return 0;
            }
            else
            {
                return mElements[element];
            }
        }

        public List<string> ToList()
        {
            List<string> setToList = new List<string>();
            foreach (KeyValuePair<string, uint> entry in mElements)
            {
                for (int i = 0; i < entry.Value; i++)
                {
                    setToList.Add(entry.Key);
                }
            }
            setToList.Sort();
            return setToList;
        }

        public MultiSet Union(MultiSet other)
        {
            MultiSet union = new MultiSet();
            HashSet<string> allElements = new HashSet<string>();

            foreach (KeyValuePair<string, uint> entry in mElements)
            {
                allElements.Add(entry.Key);
            }

            foreach (KeyValuePair<string, uint> entry in other.mElements)
            {
                allElements.Add(entry.Key);
            }

            foreach (string element in allElements)
            {
                if (!mElements.ContainsKey(element))
                {
                    union.mElements[element] = other.mElements[element];
                    continue;
                }

                if (!other.mElements.ContainsKey(element))
                {
                    union.mElements[element] = mElements[element];
                    continue;
                }

                if (mElements[element] >= other.mElements[element])
                {
                    union.mElements[element] = mElements[element];
                }
                else
                {
                    union.mElements[element] = other.mElements[element];
                }
            }

            return union;
        }

        public MultiSet Intersect(MultiSet other)
        {
            MultiSet intersection = new MultiSet();

            foreach (KeyValuePair<string, uint> entry in mElements)
            {
                if (!other.mElements.ContainsKey(entry.Key))
                {
                    continue;
                }

                if (entry.Value > other.mElements[entry.Key])
                {
                    intersection.mElements[entry.Key] = other.mElements[entry.Key];
                }
                else
                {
                    intersection.mElements[entry.Key] = entry.Value;
                }
            }
            return intersection;
        }

        public MultiSet Subtract(MultiSet other)
        {
            MultiSet subtract = new MultiSet();
            foreach (KeyValuePair<string, uint> entry in mElements)
            {
                if (!other.mElements.ContainsKey(entry.Key))
                {
                    subtract.mElements[entry.Key] = entry.Value;
                    continue;
                }

                if (entry.Value > other.mElements[entry.Key])
                {
                    subtract.mElements[entry.Key] = entry.Value - other.mElements[entry.Key];
                }
            }
            return subtract;
        }

        public List<MultiSet> FindPowerSet()
        {
            List<MultiSet> powerSet = new List<MultiSet>();
            powerSet.Add(new MultiSet());
            List<string> setList = ToList();

            makePoserSetRecursive(setList, new List<string>(), 0, powerSet, new HashSet<string>());
            return powerSet;
        }

        private static void makePoserSetRecursive(List<string> setList, List<string> currentList, int i, List<MultiSet> powerSet, HashSet<string> occurances)
        {
            for (int j = i; j < setList.Count; j++)
            {
                List<string> currentListCopy = new List<string>(currentList);
                string newElement = setList[j];
                currentListCopy.Add(newElement);
                string currentListToString = String.Join("", currentListCopy);
                if (!occurances.Contains(currentListToString))
                {
                    MultiSet newSet = new MultiSet();
                    foreach (string element in currentListCopy)
                    {
                        newSet.Add(element);
                    }
                    powerSet.Add(newSet);
                    occurances.Add(currentListToString);
                }

                if (j != setList.Count - 1)
                {
                    makePoserSetRecursive(setList, currentListCopy, j + 1, powerSet, occurances);
                }
            }
        }

        public bool IsSubsetOf(MultiSet other)
        {
            foreach (KeyValuePair<string, uint> entry in mElements)
            {
                if (!other.mElements.ContainsKey(entry.Key))
                {
                    return false;
                }

                if (other.mElements[entry.Key] < entry.Value)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsSupersetOf(MultiSet other)
        {
            foreach (KeyValuePair<string, uint> otherEntry in other.mElements)
            {
                if (!mElements.ContainsKey(otherEntry.Key))
                {
                    return false;
                }

                if (otherEntry.Value > mElements[otherEntry.Key])
                {
                    return false;
                }
            }
            return true;
        }
    }
}