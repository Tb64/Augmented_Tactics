using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes {
    public List<Nodes> neighbors;

    public Vector3 coords;

    public Nodes()
    {
        neighbors = new List<Nodes>();
    }
    /// <summary>
    /// DistanceTo returns distance between source coordinate and target coordinate
    /// </summary>
    /// <param name="n"> n is target destination </param>
    /// <returns></returns>
    public float DistanceTo(Nodes n)
    {
        return Vector3.Distance(coords, n.coords); // n.coords is target destination

    }

    
}
