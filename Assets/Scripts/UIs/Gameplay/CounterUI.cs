﻿using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class CounterUI : MonoBehaviour
{
    public int m_Value;
    public string m_Mask = "00";
    private Text m_Text;

    private void Start()
    {
       m_Text = GetComponent<Text>();
       UpdateUI();
    }
    
    public void Increase(int value)
    {
        m_Value += value;
        UpdateUI();
    }

    public void Decrease(int value)
    {
        m_Value -= value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        m_Text.text = m_Value.ToString(m_Mask);
    }
}