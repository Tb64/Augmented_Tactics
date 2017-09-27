using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    #region variables
    private TileMap map;
    private Actor unit;
    public List<Node> currentPath = null;

    #endregion




    private void Start()
    {
        initialize();

    }

    void initialize()
    {
        //use this to initialize vars, avoids clutter in start

        unit = map.getSelectedUnit().GetComponent<Actor>();

        if (map == null)
        {
            Debug.LogError("Missing map, make sure to include map in level hierarchy");
            return;
        }
        //grabs tilemap from the scene and stores it in map
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<TileMap>();
    }

    //Draws pathing lines
    public void drawDebugLines()
    {

        if (currentPath != null)
        {
            //begin at 0
            int currNode = 0;

            Vector3[] position = new Vector3[currentPath.Count];

            Vector3 end = new Vector3();

            while (currNode < currentPath.Count - 1 && currentPath.Count < unit.getMoveDistance() + 2)
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].z) +
                    new Vector3(0, 1f, 0);
                end = map.TileCoordToWorldCoord(currentPath[currNode + 1].x, currentPath[currNode + 1].z) +
                    new Vector3(0, 1f, 0);

                Debug.DrawLine(start, end, Color.red);

                path.positionCount = currentPath.Count - 1;
                position[currNode] = start;
                path.SetPositions(position);
                currNode++;

                if (currNode == currentPath.Count - 1)
                {
                    position[currNode] = end;
                }

            }
        }
    }


    public float costToEnterTile(Vector3 source, Vector3 target)
    {
        TileType type = tileTypes[tiles[target.x, target.z]];

        if (UnitCanEnterTile(targetX, targetZ) == false)
        {
            return Mathf.Infinity;
        }

        float cost = type.movementCost;
        if (source.x != target.x && source.z != target.z)
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
