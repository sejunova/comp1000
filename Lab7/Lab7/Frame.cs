namespace Lab7
{
    public class Frame
    {
        public uint ID { get; }
        public string Name { get; }
        private EFeatures mFeatures;

        public Frame(uint id, string name)
        {
            ID = id;
            Name = name;
            mFeatures = EFeatures.Default;
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
