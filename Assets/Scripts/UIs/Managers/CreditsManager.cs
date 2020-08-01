using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CreditsManager : MonoBehaviour
{
    [Header("UI")]
    public TextAsset m_TextFile;
    public Text m_TextUI;

    [Header("Settings")]
    public float m_FunctionFontSize = 40.0f;
    public float m_NameFontSize = 24.0f;
    public float m_Speed = 20.0f;

    [Header("Debug")]
    public Credits m_Credits;

    private void Start() 
    {
        string json = m_TextFile.text;
        m_Credits = JsonUtility.FromJson<Credits>(json);
        StringBuilder sb = new StringBuilder();

        foreach (Function function in m_Credits.functions)
        {
            sb.Append($"<b><size={m_FunctionFontSize}>{function.title}</size></b>");
            sb.Append($"<size={m_FunctionFontSize}>\n</size>");
            foreach (string name in function.people)
            {
                sb.Append($"<b><size={m_NameFontSize}>{name}</size></b>");
                sb.Append($"<size={m_NameFontSize}>\n</size>");
            }
            sb.Append($"<size={m_FunctionFontSize}>\n</size>");
        }

        m_TextUI.text = sb.ToString();
        Canvas.ForceUpdateCanvases();
    }

    public void Update()
    {
        m_TextUI.transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
    }
}

[Serializable]
public class Credits
{
    public Function[] functions;
}

[Serializable]
public class Function 
 {
    public string title;
    public string[] people;
}