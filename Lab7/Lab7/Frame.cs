namespace Lab7
{
    class Frame
    {
        public uint mId;
        public string mName;
        public EFeatures mFeatures;

        public Frame(uint id, string name)
        {
            mId = id;
            mName = name;
            mFeatures = EFeatures.Default;
        }
        public uint ID
        {
            get { return mId; }
        }
        public string Name
        {
            get { return mName; }
        }
        public EFeatures Features
        {
            get { return mFeatures; }
        }
        public void ToggleFeatures(EFeatures features)
        {
            mFeatures ^= features;
        }
        public void TurnOnFeatures(EFeatures features)
        {
            mFeatures |= features;
        }
        public void TurnOffFeatures(EFeatures features)
        {
            mFeatures &= ~features;
        }
    }
}
