using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Animator))]
    public class HighlightingPanel : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _player.Damaged += Highlight;
        }

        private void OnDestroy()
        {
            _player.Damaged -= Highlight;
        }

        private void Highlight(float health)
        {
            _animator.Play("RedFlashing");
        }
    }
}