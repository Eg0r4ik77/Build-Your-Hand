using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Pool
{
    public abstract class Pool
    {
        private readonly int _size;
        private readonly List<IPoolObject> _objects = new();

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

        protected abstract IPoolObject Create();

        public IPoolObject Get()
        {
            if (HasUnusedObject(out IPoolObject poolObject))
            {
                return poolObject;
            }
            
            return null;
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

        private void Add(IPoolObject poolObject)
        {
            _objects.Add(poolObject);
        }
        
        public void Release(IPoolObject poolObject)
        {
            poolObject.Clear();
            poolObject.InUse = false;
        }
    }
}