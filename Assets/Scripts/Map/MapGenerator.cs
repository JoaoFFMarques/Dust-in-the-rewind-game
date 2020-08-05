using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] m_Grounds;
    public MapObject[] m_Wals;
    public MapObject[] m_Enemies;
    public MapObject[] m_Others;

    private Dictionary<string, GameObject> m_WallPrefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_EnemyPrefabs = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> m_OtherPrefabs = new Dictionary<string, GameObject>();

    private string m_EnemyKeys;
    private string m_OtherKeys;

    public Map Map { get; set; }

    private void Initialize()
    {
        foreach (var obj in m_Wals)
            m_WallPrefabs.Add(obj.symbol, obj.prefab);

        foreach (var obj in m_Enemies)
            m_EnemyPrefabs.Add(obj.symbol, obj.prefab);

        foreach (var obj in m_Others)
            m_OtherPrefabs.Add(obj.symbol, obj.prefab);

        m_EnemyKeys = string.Join("", m_EnemyPrefabs.Keys);
        m_OtherKeys = string.Join("", m_OtherPrefabs.Keys);
    }

    public Map Build()
    {
        Initialize();

        var levelNumber = PlayerPrefs.GetInt("level", 1);
        Map = MapHelper.Load(levelNumber, m_EnemyKeys, m_OtherKeys);

        CreateGroundAndWalls();
        CreateByMapObject(Map.Enemies, m_EnemyPrefabs);
        CreateByMapObject(Map.Others, m_OtherPrefabs);

        return Map;
    }

    private void CreateGroundAndWalls()
    {
        for (int i = 1; i < Map.Row - 1; i++)
        {
            for (int j = 1; j < Map.Column - 1; j++)
            {
                var position = new Vector2(j, -i);
                GameObject prefab = null;

                if (Map.Tiles[i,j] == '.')
                {
                    var index = Random.Range(0, m_Grounds.Length);
                    prefab = m_Grounds[index];
                }
                else
                {
                    var key = Calculate(i, j);
                    prefab = m_WallPrefabs.ContainsKey(key) ? m_WallPrefabs[key] : m_Grounds[0];
                }
                
                Instantiate(prefab, position, Quaternion.identity, transform);
            }
        }
    }

    private string Calculate(int i, int j)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append(Map.Tiles[i - 1, j    ]);
        builder.Append(Map.Tiles[i    , j - 1]);
        builder.Append(Map.Tiles[i    , j    ]);
        builder.Append(Map.Tiles[i    , j + 1]);
        builder.Append(Map.Tiles[i + 1, j    ]);
        return builder.ToString();
    }

    private void CreateByMapObject(Dictionary<string, List<Vector2>> dict, Dictionary<string, GameObject> prefabs)
    {
        foreach (KeyValuePair<string, List<Vector2>> item in dict)
        {
            var prefab = prefabs[item.Key];
            foreach (var position in item.Value)
                Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }
}