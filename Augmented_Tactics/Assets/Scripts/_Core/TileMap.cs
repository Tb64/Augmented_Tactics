using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TileMap : MonoBehaviour {

    public GameObject selectedUnit;
    public TileType[] tileTypes;            //This seems stupid it should be stored in the tile

    public bool codeGenerateMap = true;


    public class Location
    {
        public int coordX;
        public int coordZ;

        public Location()
        {
            coordX = 0;
            coordZ = 0;
        }
    }


    public Location[] Players;
   
    int[,] tiles;
    Node[,] graph;
    
    int mapSizeX = 16;
    int mapSizeZ = 16;


    // Use this for initialization
    void Start() {

        Players = new Location[20];
        for (int index = 0; index < Players.Length; index++)
        {
            Players[index] = new Location();
        }
        //Players = null;
        //setup selectedUnit vars

        selectedUnit.GetComponent<Actor>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Actor>().tileZ = (int)selectedUnit.transform.position.z;
        selectedUnit.GetComponent<Actor>().map = this;

        if(codeGenerateMap)
        {
            GenerateMapData();
            GenerateMapVisual();
        }
        else
        {
            LoadTileData();
        }

        generatePathFindingGraph();

    }

    void LoadTileData()
    {
        tiles = new int[mapSizeX, mapSizeZ];
        ClickableTile[] loadedTiles = GetComponentsInChildren<ClickableTile>();

        foreach (ClickableTile ctTile in loadedTiles)
        {
            ctTile.map = this;
            tiles[ctTile.tileX, ctTile.tileZ] = ctTile.tileClass;
        }

    }

    void GenerateMapData()
    {
        //allocate tiles
        tiles = new int[mapSizeX, mapSizeZ];

        //init tiles
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                tiles[x, z] = 0;
            }

        }

        //tiles[2, 3] = 2;
        //tiles[2, 4] = 2;
        //tiles[2, 5] = 2;
        //tiles[3, 3] = 2;
    }

    void GenerateMapVisual()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int z = 0; z < mapSizeZ; z++)
            {
                TileType tt = tileTypes[tiles[x, z]];

                GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, 0, z), Quaternion.identity);
                go.transform.parent = transform;
                ClickableTile ct = go.GetComponent<ClickableTile>();
                ct.tileX = x;
                ct.tileZ = z;
                ct.map = this;
            }

        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int z)
    {
        return new Vector3(x, 0, z);
    }


    public float costToEnterTile(int sourceX, int sourceY,int targetX, int targetZ)
    {
       TileType tt = tileTypes[tiles[targetX, targetZ]];

        if(UnitCanEnterTile(targetX,targetZ) == false)
        {
            return Mathf.Infinity;
        }

        float cost = tt.movementCost;
        if(sourceX != targetX && sourceY != targetZ){
            //moving diagonally
            cost += 0.001f;// done to prefer straight lines over diagonal lines
        }


       return cost;
        
    }

    public bool UnitCanEnterTile(int x, int z)
    {
        //could test units movement type(walk,fly,run etc..)

        return tileTypes[tiles[x,z]].isWalkable;
    }

    public void GeneratePathTo(int x, int z)
    {
        selectedUnit.GetComponent<Actor>().currentPath = null;

        if(UnitCanEnterTile(x,z) == false)
        {//tile is not walkable
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        Actor uXZ = selectedUnit.GetComponent<Actor>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[uXZ.tileX,uXZ.tileZ];
        Node target = graph[x,z];
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
                float alt = dist[u] + costToEnterTile(u.x,u.z,v.x,v.z);
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
        selectedUnit.GetComponent<Actor>().currentPath = currentPath;
    }

    void generatePathFindingGraph()
    {
        //init array
        graph = new Node[mapSizeX,mapSizeZ];

        //init a node for each index in array
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int Z = 0; Z < mapSizeZ; Z++)
            {
                graph[x, Z] = new Node();

                graph[x, Z].x = x;
                graph[x, Z].z = Z;
            }
        }
                for (int x = 0; x < mapSizeX; x++)
        {
            for(int z = 0; z < mapSizeZ; z++)
            {

                //4 way movement

                if (x > 0)
                {
                    graph[x, z].neighbors.Add(graph[x - 1, z]);
                }
                if (x < mapSizeX - 1)
                {
                    graph[x, z].neighbors.Add(graph[x + 1, z]);
                }
                if (z > 0)
                {
                    graph[x, z].neighbors.Add(graph[x, z - 1]);
                }
                if (z < mapSizeZ - 1)
                {
                    graph[x, z].neighbors.Add(graph[x, z + 1]);
                }

                //8 way movement=============================================================
                //if (x > 0) //try moving left
                //{
                //    graph[x, z].neighbors.Add(graph[x - 1, z]);
                //    if (z > 0)
                //    {
                //        graph[x, z].neighbors.Add(graph[x - 1, z - 1]);
                //    }
                //    if (z < mapSizeZ - 1)
                //    {
                //        graph[x, z].neighbors.Add(graph[x - 1, z + 1]);
                //    }
                //}
                //if (x < mapSizeX - 1) // try moving right
                //{
                //    graph[x, z].neighbors.Add(graph[x + 1, z]);
                //    if (z > 0)
                //    {
                //        graph[x, z].neighbors.Add(graph[x + 1, z - 1]);
                //    }
                //    if (z < mapSizeZ - 1)
                //    {
                //        graph[x, z].neighbors.Add(graph[x + 1, z + 1]);
                //    }
                //}
                //if (z > 0) // try moving up/down
                //{
                //    graph[x, z].neighbors.Add(graph[x, z - 1]);
                //}
                //if (z < mapSizeZ - 1)
                //{
                //    graph[x, z].neighbors.Add(graph[x, z + 1]);
                //}
                //===========================================================================
            }
        }
    }

}