using System.Collections.Generic;
using UnityEngine;

public class MapHelper : MonoBehaviour
{
    public static readonly string building = ".#";
    public static readonly string enemies = "BSMAEGP";
    public static readonly string others = "@*";

    public static Map Load(int levelNumber)
    {
        return Load($"level_{levelNumber}");
    }

    public static Map Load(string filename)
    {
        TextAsset file = Resources.Load<TextAsset>($"Level/{filename}");
        Map map = new Map();
        string[] lines = file.text.Split('\n');

        int index = 0;
        while (index < lines.Length)
        {
            if (TryStringByKey(lines[index], "Story", out string story))
            {
                map.Story = story;
            }
            else if (TryFloatByKey(lines[index], "Time", out float time))
            {
                map.Time = time;
            }
            else if (TryIntByKey(lines[index], "Moves", out int moves))
            {
                map.Moves = moves;
            }
            else if (TryStringByKey(lines[index], "Level", out string level))
            {
                string[] size = level.Split(',');
                int column = int.Parse(size[0]);
                int row = int.Parse(size[1]);
                map.Tiles = new char[column, row];

                for (int y = 0; y < row; y++)
                {
                    char[] tiles = lines[++index].ToCharArray();
                    for (int x = 0; x < column; x++)
                    {
                        if (enemies.Contains(tiles[column].ToString()))
                        {
                            var tile = tiles[column].ToString();
                            if (!map.Enemies.ContainsKey(tile))
                                map.Enemies.Add(tile, new List<Vector2>());

                            map.Enemies[tile].Add(new Vector2(x, y));
                            map.Tiles[y, x] = building[0];
                        }
                        else if (others.Contains(tiles[column].ToString()))
                        {
                            var tile = tiles[column].ToString();
                            if (!map.Others.ContainsKey(tile))
                                map.Others.Add(tile, new List<Vector2>());

                            map.Others[tile].Add(new Vector2(x, y));
                            map.Tiles[y, x] = building[0];
                        }
                        else
                        {
                            map.Tiles[y, x] = tiles[column];
                        }
                    }
                }
            }
        }

        return map;
    }

    public static bool TryStringByKey(string line, string key, out string value)
    {
        value = null;
        if (line.StartsWith(key))
        {
            value = line.Replace($"{key}:", "");
            return true;
        }

        return false;
    }

    public static bool TryIntByKey(string line, string key, out int value)
    {
        value = 0;
        if (TryStringByKey(line, key, out string temp))
            return int.TryParse(temp.Trim(), out value);

        return false;
    }

    public static bool TryFloatByKey(string line, string key, out float value)
    {
        value = 0;
        if (TryStringByKey(line, key, out string temp))
            return float.TryParse(temp.Trim(), out value);

        return false;
    }
}