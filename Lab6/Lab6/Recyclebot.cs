using System.Collections.Generic;

namespace Lab6
{
    class Recyclebot
    {
        private List<Item> mRecycleItems;
        private List<Item> mNonRecycleItems;

        public Recyclebot()
        {
            mRecycleItems = new List<Item>();
            mNonRecycleItems = new List<Item>();
        }
        public List<Item> RecycleItems
        {
            get { return mRecycleItems; }
        }
        public List<Item> NonRecycleItems
        {
            get { return mNonRecycleItems; }
        }

        public void Add(Item item)
        {
            if (item.Type != EType.Paper && item.Type != EType.Furniture && item.Type != EType.Electronics)
            {
                mRecycleItems.Add(item);
            }
            else
            {
                if (item.Weight >= 2 && item.Weight < 5)
                {
                    mRecycleItems.Add(item);
                }
                else
                {
                    mNonRecycleItems.Add(item);
                }
            }

        }
        public List<Item> Dump()
        {
            List<Item> dumpList = new List<Item>();
            for (int i = 0; i < mNonRecycleItems.Count; i++)
            {
                bool bFirstConditionalProposal;
                Item item = mNonRecycleItems[i];

                // 첫 p->q 조건명제
                if (item.Volume == 10 || item.Volume == 11 || item.Volume == 15)
                {
                    bFirstConditionalProposal = true;
                }
                else
                {
                    if (item.IsToxicWaste == true)
                    {
                        bFirstConditionalProposal = true;
                    }
                    else
                    {
                        bFirstConditionalProposal = false;
                    }
                }

                // 두번째 q->r 조건명제
                if (bFirstConditionalProposal == false)
                {
                    dumpList.Add(item);
                }
                else
                {
                    if (item.Type == EType.Furniture || item.Type == EType.Electronics)
                    {
                        dumpList.Add(item);
                    }
                }
            }
            return dumpList;
        }
    }
}
