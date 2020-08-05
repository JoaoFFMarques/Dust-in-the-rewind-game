using System.Collections.Generic;
using UnityEngine;

public class Map
{
    public string Story { get; set; }
    public float Time { get; set; }
    public int Moves { get; set; }
    public Dictionary<string, List<Vector2>> Enemies { get; set; } = new Dictionary<string, List<Vector2>>();
    public Dictionary<string, List<Vector2>> Others { get; set; } = new Dictionary<string, List<Vector2>>();
    public char[,] Tiles { get; set; }
}