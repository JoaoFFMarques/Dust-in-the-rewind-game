using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class MapHelper : MonoBehaviour
{
    public static readonly char EMPTY = '-';
    public static readonly char GROUND = '.';
    public static readonly char WALL = '#';

    public static Map Load(int number, string enemyKeys, string otherKeys)
    {
        TextAsset file = Resources.Load<TextAsset>($"Level/{number}");
        Map map = new Map() { Level = number };
        string[] lines = file.text.Split('\n');

        int index = 0;
        while (index < lines.Length)
        {
            if (TryStringByKey(lines[index], "Story", out string story))
            {
                map.Story = story;
            }
            else if (TryStringByKey(lines[index], "File", out string tilemapName))
            {
                map.File = tilemapName;
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

                map.Row = row + 2;
                map.Column = column + 2;
                map.Grounds = new char[row + 2, column + 2];
                map.Walls = new char[row + 2, column + 2];

                for (int i = 0; i < map.Row; i++)
                {
                    for (int j = 0; j < map.Column; j++)
                    {
                        map.Grounds[i, j] = EMPTY;
                        map.Walls[i, j] = EMPTY;
                    }
                }

                for (int i = 0; i < row; i++)
                {
                    char[] tiles = lines[++index].ToCharArray();
                    for (int j = 0; j < column; j++)
                    {
                        if (enemyKeys.Contains(tiles[j].ToString()))
                        {
                            var tile = tiles[j].ToString();
                            if (!map.Enemies.ContainsKey(tile))
                                map.Enemies.Add(tile, new List<Vector2>());

                            map.Enemies[tile].Add(new Vector2(j + 1, i + 1));
                            map.Grounds[i + 1, j + 1] = GROUND;
                            map.Walls[i + 1, j + 1] = EMPTY;
                        }
                        else if (otherKeys.Contains(tiles[j].ToString()))
                        {
                            var tile = tiles[j].ToString();
                            if (!map.Others.ContainsKey(tile))
                                map.Others.Add(tile, new List<Vector2>());

                            map.Others[tile].Add(new Vector2(j + 1, i + 1));
                            map.Grounds[i + 1, j + 1] = GROUND;
                            map.Walls[i + 1, j + 1] = EMPTY;
                        }
                        else if (tiles[j] == GROUND)
                        {
                            map.Grounds[i + 1, j + 1] = GROUND;
                            map.Walls[i + 1, j + 1] = EMPTY;
                        }
                        else if (tiles[j] == WALL)
                        {
                            map.Grounds[i + 1, j + 1] = GROUND;
                            map.Walls[i + 1, j + 1] = WALL;
                        }
                    }
                }
            }

            index++;
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