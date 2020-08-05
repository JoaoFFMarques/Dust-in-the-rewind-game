using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HintTextUI : MonoBehaviour
{
    public Text m_TitleTextUI;
    public Text m_MessageTextUI;
    public Button m_ConfirmButton;
    public FadeInOut m_FadeEffect;
    public MoveUI m_MoveEffect;

    public void Show(string title, string message)
    {        
        m_TitleTextUI.text = title;
        m_MessageTextUI.text = message;
        m_FadeEffect.Show();
    }

    public void AddListener(UnityAction callback)
    {
        m_ConfirmButton.onClick.RemoveAllListeners();
        m_ConfirmButton.onClick.AddListener(callback);
    }

    public void Hide()
    {        
        m_MoveEffect.Disable();
        m_FadeEffect.Hide();
    }
}
