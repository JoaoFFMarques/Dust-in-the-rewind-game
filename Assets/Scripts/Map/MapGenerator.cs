using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Default")]
    public string m_GroundHash = ".........";
    public string m_WallHash = "#########";

    [Header("Prefabs")]
    public MapObject[] m_Tiles;
    public MapObject[] m_Enemies;
    public MapObject[] m_Others;

    private Dictionary<string, GameObject> m_TilePrefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_EnemyPrefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_OtherPrefabs = new Dictionary<string, GameObject>();

    private string m_EnemyKeys;
    private string m_OtherKeys;

    private Map m_Map;

    private void Initialize()
    {
        foreach (var tile in m_Tiles)
            foreach (var hash in tile.hash)
                m_TilePrefabs.Add(hash, tile.prefab);

        foreach (var enemy in m_Enemies)
            foreach (var hash in enemy.hash)
                m_EnemyPrefabs.Add(hash, enemy.prefab);

        foreach (var other in m_Others)
            foreach (var hash in other.hash)
                m_OtherPrefabs.Add(hash, other.prefab);

        m_EnemyKeys = string.Join("", m_EnemyPrefabs.Keys);
        m_OtherKeys = string.Join("", m_OtherPrefabs.Keys);
    }

    public Map Build()
    {
        Initialize();

        //var levelNumber = PlayerPrefs.GetInt("level", 1);
        var levelNumber = 1;
        m_Map = MapHelper.Load(levelNumber, m_EnemyKeys, m_OtherKeys);

        CreateGroundAndWalls();
        CreateByMapObject(m_Map.Enemies, m_EnemyPrefabs);
        CreateByMapObject(m_Map.Others, m_OtherPrefabs);

        return m_Map;
    }

    private void CreateGroundAndWalls()
    {
        for (int i = 1; i < m_Map.Row - 1; i++)
        {
            for (int j = 1; j < m_Map.Column - 1; j++)
            {
                var position = new Vector2(j, -i);

                if (m_Map.Grounds[i,j] == MapHelper.GROUND)
                {
                    var key = Calculate(m_Map.Grounds, i, j);
                    InstantiateTile(key, position, m_GroundHash);
                }

                if (m_Map.Walls[i, j] == MapHelper.WALL)
                {
                    var key = Calculate(m_Map.Walls, i, j);
                    InstantiateTile(key, position, m_WallHash);
                }
            }
        }
    }

    private void InstantiateTile(string key, Vector2 position, string defaultKey)
    {
        var prefab = m_TilePrefabs.ContainsKey(key) ? m_TilePrefabs[key] : m_TilePrefabs[defaultKey];
        Instantiate(prefab, position, Quaternion.identity, transform);
    }

    private string Calculate(char[,] tiles, int i, int j)
    {
        StringBuilder builder = new StringBuilder();

        for (int ii = -1; ii < 2; ii++)
            for (int jj = -1; jj < 2; jj++)
                builder.Append(tiles[i + ii, j + jj]);

        return builder.ToString();
    }

    private void CreateByMapObject(Dictionary<string, List<Vector2>> dict, Dictionary<string, GameObject> prefabs)
    {
        foreach (KeyValuePair<string, List<Vector2>> item in dict)
        {
            var mirror = new Vector2(1, -1);
            var prefab = prefabs[item.Key];
            foreach (var position in item.Value)
            {
                Instantiate(prefab, position * mirror, Quaternion.identity, transform);
            }
        }
    }
}