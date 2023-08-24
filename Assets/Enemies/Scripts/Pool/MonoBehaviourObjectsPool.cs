using UnityEngine;

namespace Enemies.Pool
{
    public abstract class MonoBehaviourObjectsPool : Pool
    {
        private readonly Transform _rootTransform;

        protected MonoBehaviourObjectsPool(int size, Transform rootTransform)
            : base(size)
        {
            _rootTransform = rootTransform;
        }

        protected void AttachToRoot(Transform objectTransform)
        {
            objectTransform.SetParent(_rootTransform);
            objectTransform.position = Vector3.zero;
        }
    }
}