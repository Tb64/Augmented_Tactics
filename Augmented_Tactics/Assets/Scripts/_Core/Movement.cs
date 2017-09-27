using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {

    #region variables
    private TileMap map;
    public ClickableTile[,] genMap;
    private Actor unit;
    public List<Node> currentPath = null;
    LineRenderer path;
    

    #endregion




    private void Start()
    {
        initialize();

    }

    void initialize()
    {
        //use this to initialize vars, avoids clutter in start
        genMap = new ClickableTile[map.mapSizeX, map.mapSizeZ];
        unit = map.getSelectedUnit().GetComponent<Actor>();

        if (map == null)
        {
            Debug.LogError("Missing map, make sure to include map in level hierarchy");
            return;
        }
        //grabs tilemap from the scene and stores it in map
        map = GameObject.FindGameObjectWithTag("Map").GetComponent<TileMap>();

        if (GameObject.Find("Path").GetComponent<LineRenderer>() == null)
        {
            Debug.LogError("Missing path, make sure to include in level hierarchy");
            return;
        }
        path = GameObject.Find("Path").GetComponent<LineRenderer>();
    }

    //Draws pathing lines
    public void drawDebugLines()
    {

        if (currentPath != null)
        {
            //begin at 0
            int currNode = 0;

            Vector3[] position = new Vector3[currentPath.Count];
            Vector3 start = new Vector3();
            Vector3 end = new Vector3();

            while (currNode < currentPath.Count - 1 && currentPath.Count < unit.getMoveDistance() + 2)
            {
                //starting vector
                start = currentPath[currNode].coords + new Vector3(0, 1f, 0);
                //ending vector(next in array)
                end = currentPath[currNode + 1].coords + new Vector3(0, 1f, 0);
                
                //draws lines in scene view to visualize path
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
        
        
        if (UnitCanEnterTile(source.x, target.z) == false)
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
        return genMap[x, z].tileTypes.isWalkable && genMap[x, z].isOccupied() == false;
    }

    public void GeneratePathTo(Vector3 coords)
    {
        Actor player = GameObject.FindWithTag("Player").GetComponent<Actor>();
        Actor enemy = GameObject.FindWithTag("Enemy").GetComponent<Actor>();
        List<Node> currentPath = new List<Node>();

        int x = (int)coords.x;
        int z = (int)coords.z;

        genMap[enemy.tileX, enemy.tileZ].setOccupied();
        genMap[player.tileX, player.tileZ].setOccupied();

        currentPath = null;

        if (UnitCanEnterTile(x, z) == false || genMap[x, z].isOccupied() == true)
        {//tile is not walkable
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[unit.tileX, unit.tileZ];
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
                float alt = dist[u] + costToEnterTile(u.coords, v.coords);
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

        Node curr = target;


        //step through prev chain and add it to path
        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }



        currentPath.Reverse(); //inverts the path
       // selectedUnit.GetComponent<Actor>().currentPath = currentPath;
    }

    private void OnMouseExit()
    {
        path.positionCount = 0;
    }
}
