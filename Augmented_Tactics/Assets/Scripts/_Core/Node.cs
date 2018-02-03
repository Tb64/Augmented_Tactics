using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {
    public List<Node> neighbors;

    public Vector3 coords;

    public Node()
    {
        neighbors = new List<Node>();
    }
    /// <summary>
    /// DistanceTo returns distance between source coordinate and target coordinate
    /// </summary>
    /// <param name="n"> n is target destination </param>
    /// <returns></returns>
    public float DistanceTo(Node n)
    {
        return Vector3.Distance(coords, n.coords); // n.coords is target destination
    }

    
}
