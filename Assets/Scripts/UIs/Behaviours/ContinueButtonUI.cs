using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonUI : MonoBehaviour
{
    private Selectable m_Selectable;
    public string m_Key = "continue";
    public bool m_UseDelete = true;

    private void Awake()
    {
        m_Selectable = GetComponent<Selectable>();
    }

    private void OnEnable()
    {
        if (m_UseDelete)
            Destroy(gameObject);
        else
            m_Selectable.interactable = PlayerPrefs.HasKey(m_Key);
    }
}
