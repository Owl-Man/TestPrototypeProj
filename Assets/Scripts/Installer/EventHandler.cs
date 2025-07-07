using System;
using UnityEngine;

public class EventHandler : MonoBehaviour
{
    public event Action onDefeatEnemy;
    
    public event Action onStartNewWave;

    public event Action onWin;

    public void DefeatEnemy()
    {
        onDefeatEnemy?.Invoke();
    }

    public void StartNewWave()
    {
        Debug.Log("StartNewWave");
        onStartNewWave?.Invoke();
    }

    public void Win()
    {
        onWin?.Invoke();
    }
}