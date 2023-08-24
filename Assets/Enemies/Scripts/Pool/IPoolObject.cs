namespace Enemies.Pool
{
    public interface IPoolObject
    {
        public bool InUse { get; set; }
        public void Clear();
    }
}