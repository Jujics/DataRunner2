using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class ComboManager
{
    private static int currentCombo = 0;
    private static System.Timers.Timer comboTimer;

    public int _currentCombo
    {
        get { return currentCombo; }
    }

    public void ComboCount()
    {
        if (comboTimer.Enabled)
        {
            comboTimer.Stop();
        }
        currentCombo++;
        SetTimer();
    }
    private static void SetTimer()
    {
        comboTimer = new System.Timers.Timer(2000);
        comboTimer.Elapsed += OnTimedEvent;
        comboTimer.AutoReset = true;
        comboTimer.Enabled = true;
    }

    private static void OnTimedEvent(object source, ElapsedEventArgs e)
    {
        currentCombo = 0;
    }
}
