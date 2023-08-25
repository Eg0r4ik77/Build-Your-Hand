using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Animator))]
    public class HighlightingPanel : MonoBehaviour, IPauseable
    {
        [SerializeField] private Player _player;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _player.Damaged += Highlight;
            _player.Died += StopHighlighting;
        }

        private void OnDestroy()
        {
            _player.Damaged -= Highlight;
            _player.Died -= StopHighlighting;
        }
        
        public void SetPaused(bool paused)
        {
            _animator.enabled = !paused;
        }

        private void Highlight()
        {
            _animator.Play("RedFlashing");
        }

        private void StopHighlighting()
        {
            _animator.InterruptMatchTarget();
        }
    }
}