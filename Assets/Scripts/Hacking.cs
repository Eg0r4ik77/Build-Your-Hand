using UnityEngine;

public class Hacking : MonoBehaviour
{
    private IHackable _hackable;

    public bool CanHack { get; private set; }

    public void SetHackable(IHackable hackable)
    {
        _hackable = hackable;
        CanHack = hackable != null;
    }

    public void Hack()
    {
        _hackable.ApplyHack();
    }
}