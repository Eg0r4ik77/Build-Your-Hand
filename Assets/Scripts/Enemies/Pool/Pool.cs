using System.Collections.Generic;

namespace Enemies.Pool
{
    public abstract class Pool
    {
        private readonly int _size;
        private readonly List<IPoolObject> _objects = new();

        public List<IPoolObject> Objects => _objects;

        protected Pool(int size)
        {
            _size = size;
        }

        public void Initialize() 
        {
            for (int i = 0; i < _size; i++)
            {
                IPoolObject poolObject = Create();
                Add(poolObject);
            }
        }
        
        public IPoolObject Get()
        {
            if (HasUnusedObject(out IPoolObject poolObject))
            {
                return poolObject;
            }
            
            return null;
        }

        protected abstract IPoolObject Create();

        protected void Release(IPoolObject poolObject)
        {
            poolObject.Clear();
            poolObject.InUse = false;
        }
        
        private void Add(IPoolObject poolObject)
        {
            _objects.Add(poolObject);
        }

        private bool HasUnusedObject(out IPoolObject unusedObject)
        {
            foreach (IPoolObject poolObject in _objects)
            {
                if (poolObject.InUse == false)
                {
                    poolObject.InUse = true;
                    unusedObject = poolObject;
                    return true;
                }
            }

            unusedObject = default;
            return false;
        }
    }
}