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
        public float DistanceTo(Node n)
        {
            return Vector3.Distance(new Vector2(coords.x, coords.z),
                new Vector2(n.coords.x, n.coords.z));
        }

    
}
