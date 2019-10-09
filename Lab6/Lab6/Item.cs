using System;
using System.Collections.Generic;
using System.Text;

namespace Lab6
{
    public class Item
    {
        private EType mType;
        private double mWeight;
        private double mVolume;
        private bool mIsToxicWaste;

        public EType Type
        {
            get { return mType; }
        }
        public double Weight
        {
            get { return mWeight; }
        }
        public double Volume
        {
            get { return mVolume; }
        }
        public bool IsToxicWaste
        {
            get { return mIsToxicWaste; }
        }

        public Item(EType type, double weight, double volume, bool isToxicWaste)
        {
            mType = type;
            mWeight = weight;
            mVolume = volume;
            mIsToxicWaste = isToxicWaste;
        }
    }
}
