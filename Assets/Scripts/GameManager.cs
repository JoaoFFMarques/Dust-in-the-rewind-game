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

    [Header("Generator")]
    public StageGenerator m_Generator;


    public bool m_GameStarts;
    private GameObject m_Player;

    private int m_CurrentLevel;

    private void Start()
    {
        m_Generator.LoadLevel();
        m_CurrentLevel = StageGenerator.m_CurrentLevel;
        m_Generator.gameObject.SetActive(false);
        m_Player = GameObject.FindGameObjectWithTag("Player");
        m_Player.SetActive(false);
        m_PauseUI.enabled = false;
        ShowStory();
        m_ChronometerUI.SetMaxTime(m_Generator.m_Time);
        m_MovesUI.m_Value = m_Generator.m_Moves;
        m_LevelUI.m_Value = StageGenerator.m_CurrentLevel;
    }

    private void Update()
    {
        if (m_Player.GetComponent<PlayerController>().m_End)
            GameOver();
    }

    public void ShowStory()
    {
        m_HintUI.Show("Level"+ m_CurrentLevel, m_Generator.m_Story);
    }

    public void HideStory()
    {
        m_HintUI.Hide();

        m_ChronometerUI.transform.parent.gameObject.SetActive(true);
        m_MovesUI.transform.parent.gameObject.SetActive(true);
        m_LevelUI.transform.parent.gameObject.SetActive(true);
        m_ScoreUI.transform.parent.gameObject.SetActive(true);
        m_Generator.gameObject.SetActive(true);
        m_Player.gameObject.SetActive(true);
        m_PauseUI.enabled = true;
        m_GameStarts = true;        
    }

    public void GameOver()
    {
        ScreenManager.Instance.LoadLevel("Gameover");
        m_GameStarts = false;
    }
}
