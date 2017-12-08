using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode {
    public List<MapNode> neighbors;

    public Vector3 coords;

    public MapNode()
    {
        neighbors = new List<MapNode>();
    }
    /// <summary>
    /// DistanceTo returns distance between source coordinate and target coordinate
    /// </summary>
    /// <param name="n"> n is target destination </param>
    /// <returns></returns>
    public float DistanceTo(MapNode n)
    {
        return Vector3.Distance(coords, n.coords); // n.coords is target destination

    }

    
}
