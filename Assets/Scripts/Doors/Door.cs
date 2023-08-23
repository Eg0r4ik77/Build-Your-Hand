using System;
using UnityEngine;

namespace Doors
{
    [RequireComponent(typeof(Animator))]
    public class Door : MonoBehaviour
    {
        private readonly int _openingAnimationHash = Animator.StringToHash("Open");
        
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        protected void Open()
        {
            _animator.Play(_openingAnimationHash);
        }
        
        protected void SetPaused(bool paused)
        {
            _animator.enabled = !paused;
        }
    }
}