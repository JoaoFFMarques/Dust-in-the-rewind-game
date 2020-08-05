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

    [Header("Map")]
    public MapGenerator m_MapGenerator;
    private PlayerController m_Player;

    public Map Map { get; set; }

    private void Start()
    {
        Initialize();
        DisablePlayer();
        ShowStory(Map.Level, Map.Story);
    }

    private void Initialize()
    {
        Map = m_MapGenerator.Build();

        m_ChronometerUI.SetMaxTime(Map.Time);
        m_MovesUI.SetValue(Map.Moves);
        m_LevelUI.SetValue(Map.Level);

        m_ChronometerUI.transform.parent.gameObject.SetActive(false);
        m_MovesUI.transform.parent.gameObject.SetActive(false);
        m_LevelUI.transform.parent.gameObject.SetActive(false);
        m_ScoreUI.transform.parent.gameObject.SetActive(false);
    }

    private void EnablePlayer()
    {
        if (m_Player)
            m_Player.enabled = true;
    }

    private void DisablePlayer()
    {
        m_PauseUI.enabled = false;
        var player = GameObject.FindGameObjectWithTag("Player");

        if (player)
        {
            m_Player = player.GetComponent<PlayerController>();
            m_Player.enabled = false;
        }
    }

    public void ShowStory(int level, string message)
    {
        m_HintUI.Show($"Level {level}", message);
    }

    public void HideStory()
    {
        m_HintUI.Hide();

        m_ChronometerUI.transform.parent.gameObject.SetActive(true);
        m_MovesUI.transform.parent.gameObject.SetActive(true);
        m_LevelUI.transform.parent.gameObject.SetActive(true);
        m_ScoreUI.transform.parent.gameObject.SetActive(true);
        m_PauseUI.enabled = true;

        EnablePlayer();
    }

    public void GameOver()
    {
        ScreenManager.Instance.LoadLevel("Gameover");
    }
}
