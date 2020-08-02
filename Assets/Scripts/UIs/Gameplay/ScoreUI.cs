using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public int m_Score;
    public string m_Mask = "0000";
    private Text m_Text;
    private const string m_Key = "score";
    public bool m_AutoSave = true;

    private void OnEnable()
    {
        m_Score = PlayerPrefs.GetInt(m_Key, m_Score);
    }

    private void OnDestroy()
    {
        if (m_AutoSave)
            Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt(m_Key, m_Score);
    }

    public void Clear()
    {
        PlayerPrefs.DeleteKey(m_Key);
        m_Score = 0;
        UpdateUI();
    }

    private void Start()
    {
        m_Text = GetComponent<Text>();
        UpdateUI();
    }

    public void AddScore(int score)
    {
        m_Score += score;
    }

    private void UpdateUI()
    {
        m_Text.text = m_Score.ToString(m_Mask);
    }
}