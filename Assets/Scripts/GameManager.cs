using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    public Pause m_PauseUI;
    public HintTextUI m_HintUI;
    public ChronometerUI m_ChronometerUI;
    public CounterUI m_MovesUI;
    public LevelUI m_LevelUI;
    public ScoreUI m_ScoreUI;

    private int m_CurrentLevel;

    private void Start()
    {
        m_PauseUI.enabled = false;
        ShowStory();
    }

    private void LoadLevel()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            GameOver();
    }

    public void ShowStory()
    {
        m_HintUI.Show("Level 1", "Teste");
    }

    public void HideStory()
    {
        m_HintUI.Hide();

        m_ChronometerUI.transform.parent.gameObject.SetActive(true);
        m_MovesUI.transform.parent.gameObject.SetActive(true);
        m_LevelUI.transform.parent.gameObject.SetActive(true);
        m_ScoreUI.transform.parent.gameObject.SetActive(true);

        m_PauseUI.enabled = true;
    }

    public void GameOver()
    {
        ScreenManager.Instance.LoadLevel("Gameover");
    }
}
