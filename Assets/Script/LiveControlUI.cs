using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LiveControlUI : MonoBehaviour
{
    [SerializeField] private IntVariable lives;
    [SerializeField] private int maxLives = 3;
    [SerializeField] private Toggle heartPref;

    private List<Toggle> hearts = new List<Toggle>();

    public UnityEvent onGameOver;

    private void Awake()
    {
        heartPref.isOn = true;
        lives.SetValue(maxLives);
        
        for (int i = 0; i < lives.GetValue(); i++)
        {
            hearts.Add(Instantiate<Toggle>(heartPref, transform));
        }

        lives.onValueChange += OnValueChange;
    }

    private void OnDestroy()
    {
        lives.onValueChange -= OnValueChange;
    }

    private void OnValueChange(int value)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            hearts[i].isOn = i < value;
        }
        
        if(value <= 0) onGameOver?.Invoke();
        
        this.enabled = false;
    }
}
