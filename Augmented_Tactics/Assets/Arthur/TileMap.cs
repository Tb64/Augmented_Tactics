using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TileMap : MonoBehaviour {

    public GameObject selectedUnit;
    public TileType[] tileTypes;


    int[,] tiles;
    Node[,] graph;
    

    int mapSizeX = 64;
    int mapSizeY = 64;


    // Use this for initialization
    void Start() {
        //setup selectedUnit vars

        selectedUnit.GetComponent<Unit>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Unit>().tileY = (int)selectedUnit.transform.position.y;
        selectedUnit.GetComponent<Unit>().map = this;

        GenerateMapData();
        generatePathFindingGraph();
        GenerateMapVisual();
       
        
    }

   

    void GenerateMapData()
    {
        //allocate tiles
        tiles = new int[mapSizeX, mapSizeY];

        //init tiles
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = 0;
            }

        }

        tiles[2, 3] = 2;
        tiles[2, 4] = 2;
        tiles[2, 5] = 2;
        tiles[3, 3] = 2;
    }

    void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = tileTypes[tiles[x, y]];

                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, y, 0), Quaternion.identity);

                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;
                ct.tileY = y;
                ct.map = this;
            }

        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int y)
    {
        return new Vector3(x, y, 0);
    }


    public float costToEnterTile(int sourceX, int sourceY,int targetX, int targetY)
    {
       TileType tt = tileTypes[tiles[targetX, targetY]];

        if(UnitCanEnterTile(targetX,targetY) == false)
        {
            return Mathf.Infinity;
        }

        float cost = tt.movementCost;
        if(sourceX != targetX && sourceY != targetY){
            //moving diagonally
            cost += 0.001f;// done to prefer straight lines over diagonal lines
        }


       return cost;
        
    }

    public bool UnitCanEnterTile(int x, int y)
    {
        //could test units movement type(walk,fly,run etc..)

        return tileTypes[tiles[x,y]].isWalkable;
    }

    public void GeneratePathTo(int x, int y)
    {
        selectedUnit.GetComponent<Unit>().currentPath = null;

        if(UnitCanEnterTile(x,y) == false)
        {//tile is not walkable
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        Unit uXY = selectedUnit.GetComponent<Unit>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[uXY.tileX,uXY.tileY];
        Node target = graph[x,y];
        dist[source] = 0;
        prev[source] = null;
        
        foreach(Node v in graph)
        {
            if(v != source)
            {
                dist[v] = Mathf.Infinity;
                prev[v] = null;
            }
            unvisited.Add(v);
        }

        while(unvisited.Count > 0)
        {
            //u is unvisited node with the smallest distance
            Node u = null;

            foreach(Node possibleU in unvisited)
            {
                if(u == null || dist[possibleU] < dist[u])
                {
                    u = possibleU;
                }
            }

            if(u == target)
            {
                break;
            }

            unvisited.Remove(u);

            foreach (Node v in u.neighbors)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + costToEnterTile(u.x,u.y,v.x,v.y);
                if ( alt < dist[v])
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
        while(curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        
        currentPath.Reverse(); //inverts the path
        selectedUnit.GetComponent<Unit>().currentPath = currentPath;
    }

    void generatePathFindingGraph()
    {
        //init array
        graph = new Node[mapSizeX,mapSizeY];

        //init a node for each index in array
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node();

                graph[x, y].x = x;
                graph[x, y].y = y;
            }
        }
                for (int x = 0; x < mapSizeX; x++)
        {
            for(int y = 0; y < mapSizeY; y++)
            {
                
                //4 way movement

                //if (x > 0)
                //{
                //    graph[x, y].neighbors.Add(graph[x - 1, y]);
                //}
                //if (x < mapSizeX - 1)
                //{
                //    graph[x, y].neighbors.Add(graph[x + 1, y]);
                //}
                //if (y > 0)
                //{
                //    graph[x, y].neighbors.Add(graph[x, y - 1]);
                //}
                //if (y < mapSizeY - 1)
                //{
                //    graph[x, y].neighbors.Add(graph[x, y + 1]);
                //}

                //8 way movement
                if (x > 0) //try moving left
                {
                    graph[x, y].neighbors.Add(graph[x - 1, y]);
                    if (y > 0)
                    {
                        graph[x, y].neighbors.Add(graph[x - 1, y - 1]);
                    }
                    if (y < mapSizeY - 1)
                    {
                        graph[x, y].neighbors.Add(graph[x - 1, y + 1]);
                    }
                }
                if (x < mapSizeX - 1) // try moving right
                {
                    graph[x, y].neighbors.Add(graph[x + 1, y]);
                    if (y > 0)
                    {
                        graph[x, y].neighbors.Add(graph[x + 1, y - 1]);
                    }
                    if (y < mapSizeY - 1)
                    {
                        graph[x, y].neighbors.Add(graph[x + 1, y + 1]);
                    }
                }
                if (y > 0) // try moving up/down
                {
                    graph[x, y].neighbors.Add(graph[x, y - 1]);
                }
                if (y < mapSizeY - 1)
                {
                    graph[x, y].neighbors.Add(graph[x, y + 1]);
                }
            }
        }
    }

}