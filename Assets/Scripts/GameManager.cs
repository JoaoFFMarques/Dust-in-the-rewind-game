using Cinemachine;
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

    [Header("Camera")]
    public CinemachineTargetGroup m_TargetGroup;
    public float m_PlayerWeight = 2.0f;
    public float m_PortalWeight = 1.0f;
    public float m_PortalRadius = 2.0f;
    public float m_PlayerRadius = 1.0f;
    
    public Map Map { get; set; }

    private void Start()
    {
        Initialize();
        SetTargetGroup();
        DisablePlayer();
        ShowStory(Map.Level, Map.Story);
    }

    private void SetTargetGroup()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        var portal = GameObject.FindGameObjectWithTag("Portal");
        m_TargetGroup.AddMember(player.transform, m_PlayerWeight, m_PlayerRadius);
        m_TargetGroup.AddMember(portal.transform, m_PortalWeight, m_PortalRadius);
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
