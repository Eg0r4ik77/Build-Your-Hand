using System;

namespace Economy
{
    public class ResourcesWallet
    {
        public float ResourcesSum { get; private set; }

        public event Action<float> Changed;

        public void Add(float value)
        {
            ResourcesSum += value;
            Changed?.Invoke(ResourcesSum);
        }
        
        public void Remove(float value)
        {
            ResourcesSum -= value;
            Changed?.Invoke(ResourcesSum);
        }
    }
}