using System.Collections.Generic;

public class Pause
{
    private readonly List<IPauseable> _pauseables = new();

    public bool Paused { get; private set; }

    public void Register(IPauseable pauseable)
    {
        _pauseables.Add(pauseable);
    }
    
    public void Register(List<IPauseable> pauseables)
    {
        _pauseables.AddRange(pauseables);
    }
    
    public void SetPaused(bool paused)
    {
        Paused = paused;
        
        foreach (IPauseable pauseable in _pauseables)
        {
            pauseable.SetPaused(paused);
        }
    }
}