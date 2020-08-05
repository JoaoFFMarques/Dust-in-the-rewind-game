using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Prfabs")]
    public GameObject m_Player;
    public GameObject[] m_Grounds;
    public GameObject[] m_Walls;
    public GameObject m_RewindBlock;
    public GameObject[] m_Enemies;

    private Map m_Map;

    private void Start()
    {
        var levelNumber = PlayerPrefs.GetInt("level");
        m_Map = MapHelper.Load(levelNumber);
    }

    private void CreateMap()
    {

    }

    private void CreateEnemies()
    {

    }

    private void CreateOthers()
    {

    }
}


/*
    0   1   2
    7   8   3
    6   5   4   
*/