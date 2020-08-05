using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public int Level { get; set; }
    public string Story { get; set; }
    public float Time { get; set; }
    public string File { get; set; }
    public int Moves { get; set; }
    public Dictionary<string, List<Vector2>> Enemies { get; set; } = new Dictionary<string, List<Vector2>>();
    public Dictionary<string, List<Vector2>> Others { get; set; } = new Dictionary<string, List<Vector2>>();
    public char[,] Tiles { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
}