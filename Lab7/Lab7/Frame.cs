namespace Lab7
{
    public class Frame
    {
        public uint ID { get; }
        public string Name { get; }
        public EFeatures Features { get; set; }

        public Frame(uint id, string name)
        {
            ID = id;
            Name = name;
            Features = EFeatures.Default;
        }
        public void ToggleFeatures(EFeatures features)
        {
            Features ^= features;
        }
        public void TurnOnFeatures(EFeatures features)
        {
            Features |= features;
        }
        public void TurnOffFeatures(EFeatures features)
        {
            Features &= ~features;
        }
    }
}
