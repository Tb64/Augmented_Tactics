using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    private TileMap map;

    private void Start()
    {
        if (map == null)
        {
            return;
        }
        //grabs tilemap from the scene and stores it in map
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<TileMap>();

    }


    public float costToEnterTile(Vector3 source, Vector3 target)
    {
        TileType tt = tileTypes[tiles[target.x, target.z]];

        if (UnitCanEnterTile(targetX, targetZ) == false)
        {
            return Mathf.Infinity;
        }

        float cost = tt.movementCost;
        if (sourceX != targetX && sourceY != targetZ)
        {
            //moving diagonally
            cost += 0.001f;// done to prefer straight lines over diagonal lines
        }

        return cost;

    }


    public bool UnitCanEnterTile(int x, int z)
    {
        //could test units movement type(walk,fly,run etc..)
        return map[x, z].tileTypes.isWalkable && map[x, z].isOccupied() == false;
    }

    public void GeneratePathTo(Vector3 coords)
    {
        Actor player = GameObject.FindWithTag("Player").GetComponent<Actor>();
        Actor enemy = GameObject.FindWithTag("Enemy").GetComponent<Actor>();

        map[enemy.tileX, enemy.tileZ].setOccupied();
        map[player.tileX, player.tileZ].setOccupied();


        selectedUnit.GetComponent<Actor>().currentPath = null;

        if (UnitCanEnterTile(x, z) == false || map[x, z].occupied == true)
        {//tile is not walkable
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        Actor uXZ = selectedUnit.GetComponent<Actor>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[uXZ.tileX, uXZ.tileZ];
        Node target = graph[x, z];
        dist[source] = 0;
        prev[source] = null;

        foreach (Node v in graph)
        {
            if (v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisited.Add(v);
        }

        while (unvisited.Count > 0)
        {
            //u is unvisited node with the smallest distance
            Node u = null;

            foreach (Node possibleU in unvisited)
            {
                if (u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if (u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbors)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + costToEnterTile(u.x, u.z, v.x, v.z);
                if (alt < dist[v])
                {
                    dist[v] = alt;
                    prev[v] = u;
                }
            }
        }

        //check if there is no route
        if (prev[target] == null)
        {
            //no route to target
            return;
        }

        List<Node> currentPath = new List<Node>();
        Node curr = target;


        //step through prev chain and add it to path
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }



        currentPath.Reverse(); //inverts the path
        selectedUnit.GetComponent<Actor>().currentPath = currentPath;
    }
}
