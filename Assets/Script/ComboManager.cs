using System;
using System.Timers;

public class ComboManager
{
    private static int currentCombo = 0;
    private static Timer comboTimer;
    private static readonly object lockObject = new object();

    public int CurrentCombo
    {
        get { lock (lockObject) { return currentCombo; } }
    }

    public void ComboCount()
    {
        lock (lockObject)
        {
            if (comboTimer == null)
            {
                comboTimer = new Timer(2000);
                comboTimer.Elapsed += OnTimedEvent;
                comboTimer.AutoReset = false; 
            }

            comboTimer.Stop();
            currentCombo++;
            comboTimer.Start();
        }
    }

    private void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        lock (lockObject)
        {
            currentCombo = 0;
            comboTimer?.Dispose();
            comboTimer = null;
        }
    }

    public void Reset()
    {
        lock (lockObject)
        {
            currentCombo = 0;
            comboTimer?.Stop();
            comboTimer?.Dispose();
            comboTimer = null;
        }
    }
}