namespace Economy
{
    public class Wallet
    {
        private float _totalResourcesSum;

        public float ResourcesSum => _totalResourcesSum;

        public void Add(Resource resource)
        {
            _totalResourcesSum += resource.Value;
        }
    }
}