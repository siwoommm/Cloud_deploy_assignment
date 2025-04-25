using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class MapNode
{
    public string Id;
    public Vector2 Position;
    public int Floor;
    public List<string> Neighbors = new List<string>();

    public MapNode(string id, Vector2 pos, int floor)
    {
        Id = id;
        Position = pos;
        Floor = floor;
    }
}
