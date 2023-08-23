using System;

public class Pause
{
    private static Pause _instance;
    
    public event Action<bool> OnPaused;

    public static Pause Instance => _instance ??= new Pause();

    public void SetPaused(bool paused)
    {
        OnPaused.Invoke(paused);
    }
}