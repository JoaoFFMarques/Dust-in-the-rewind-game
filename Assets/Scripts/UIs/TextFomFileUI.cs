using UnityEngine;
using UnityEngine.UI;

public class TextFomFileUI : MonoBehaviour
{
    public TextAsset m_TextAsset;
    private Text m_TextUI;

    [Header("Move")]
    public bool m_UseMove = true;
    public float m_Speed = 20.0f;


    private void Start()
    {
        m_TextUI = GetComponent<Text>();
        m_TextUI.text = m_TextAsset.text;
    }

    public void Update()
    {
        m_TextUI.transform.Translate(Vector3.up * m_Speed * Time.deltaTime);
    }
}
