using NodeCanvas.BehaviourTrees;
using UnityEngine;

public class Reaction : MonoBehaviour
{
    private static readonly int ReactHash = Animator.StringToHash("React");
    
    private Animator _animator;
    private BehaviourTreeOwner _behaviourTreeOwner;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _behaviourTreeOwner = GetComponent<BehaviourTreeOwner>();
    }

    public void Initialize(Animator animator, BehaviourTreeOwner behaviourTreeOwner)
    {
        _animator = animator;
        _behaviourTreeOwner = behaviourTreeOwner;
    }

    public void React()
    {
        TrySetBehaviourTreeOwnerEnabled(false);
        _animator.SetTrigger(ReactHash);
    }

    private void OnReactionAnimation()
    {
        TrySetBehaviourTreeOwnerEnabled(true);
    }

    private void TrySetBehaviourTreeOwnerEnabled(bool enabled)
    {
        if (_behaviourTreeOwner)
        {
            _behaviourTreeOwner.enabled = enabled;
        }   
    }
}