using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TileMap : MonoBehaviour {


    #region variables
    public GameObject selectedUnit;
    public TileType[] tileTypes;            //This seems stupid it should be stored in the tile
    float remainingMovement;
    public bool codeGenerateMap = true;
    LineRenderer path;
    public ClickableTile[,] map;
    private Actor unit;
    public class Location
    {
        public int coordX;
        public int coordZ;

        public Vector3 coords = new Vector3();

        public Location()
        {
            coordX = 0;
            coordZ = 0;
            
        }
    }
    Vector3[] position;
    Vector3 tileCoords = new Vector3();
    public Location[] Players;

    Vector3 tempCoords = new Vector3();

    bool canMove;
    bool endOfMove;

    int[,] tiles;
    Node[,] graph;
    
    public int mapSizeX = 16;
    public int mapSizeZ = 16;
    #endregion
    // Use this for initialization

    void Start() {

        initialize();
    
    }

    //use this function to initializes variables
    void initialize()
    {
        
        map = new ClickableTile[mapSizeX, mapSizeZ];
        canMove = false;
        if (GameObject.Find("Path").GetComponent<LineRenderer>() == null)
        {
            Debug.LogError("Null reference, missing path object, add in scene hierarchy");
            return;
        }
        path = GameObject.Find("Path").GetComponent<LineRenderer>();

        //number of players in the map
        Players = new Location[20];
        
        //initialize players array
        for (int index = 0; index < Players.Length; index++)
        {
            Players[index] = new Location();
        }


        //sets unit to the selected unit in the map
        unit = selectedUnit.GetComponent<Actor>();
        Vector3 coordinates = new Vector3();

        //initializes coordinates vector to selected units transform
        coordinates.x = (int)selectedUnit.transform.position.x;
        coordinates.z = (int)selectedUnit.transform.position.z;
        //passes coordinates vector to unit to set units coords
        unit.setCoords(coordinates);

        selectedUnit.GetComponent<Actor>().tileX = (int)selectedUnit.transform.position.x;
        selectedUnit.GetComponent<Actor>().tileZ = (int)selectedUnit.transform.position.z;
        
        selectedUnit.GetComponent<Actor>().map = this;


        Actor player = GameObject.FindWithTag("Player").GetComponent<Actor>();
        Actor enemy = GameObject.FindWithTag("Enemy").GetComponent<Actor>();



        if (codeGenerateMap)
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
        map = new ClickableTile[mapSizeX, mapSizeZ];

        ClickableTile[] loadedTiles = GetComponentsInChildren<ClickableTile>();
        
        foreach (ClickableTile ctTile in loadedTiles)
        {
            ctTile.map = this;
            tiles[ctTile.tileX, ctTile.tileZ] = ctTile.tileClass;
            map[ctTile.tileX, ctTile.tileZ] = ctTile;
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
                map[x, z] = ct;
                

            }
            
        }
    }

    public Vector3 TileCoordToWorldCoord(int x, int z)
    {
        //Vector3 output = map[x, z].GetGameObject().transform.position;
        //return output;
        return new Vector3(x, 0, z);
    }

    public Vector3 TileCoordToWorldCoord(Vector3 input)
    {
        //Vector3 output = map[(int)input.x, (int)input.z].GetGameObject().transform.position;
        //return output;
        return new Vector3(input.x, 0f, input.z);
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
        return map[x,z].tileTypes.isWalkable && map[x, z].occupied == false;
    }

    public void GeneratePathTo(int x, int z)
    {

        unit = selectedUnit.GetComponent<Actor>();
        Vector3 coordinates = new Vector3();
        
        //initializes coordinates vector to selected units transform
        coordinates.x = (int)selectedUnit.transform.position.x;
        coordinates.z = (int)selectedUnit.transform.position.z;
        //passes coordinates vector to unit to set units coords
        unit.setCoords(coordinates); 
        unit.setPathNull();

        if (UnitCanEnterTile(x,z) == false || map[x,z].occupied == true)
        {//tile is not walkable
            //Debug.Log("Unable to generate path");
            return;
        }

        Dictionary<Node, float> dist = new Dictionary<Node, float>();
        Dictionary<Node, Node> prev = new Dictionary<Node, Node>();

        //Actor uXZ = selectedUnit.GetComponent<Actor>();

        List<Node> unvisited = new List<Node>();

        Node source = graph[unit.tileX,unit.tileZ];
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

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }
       
        currentPath.Reverse(); //inverts the path
        unit.setCurrentPath(currentPath);
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

                #region 8wayMovement
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
                #endregion
            }
        }
    }

    #region Movement

    /// <summary>
    /// Generates path and moves the actor the map currently has selected.
    /// </summary>
    /// <returns>False = move not finished.  True = move finished.</returns>
    public bool moveUnit()
    {
        return moveUnit(unit);
    }

    /// <summary>
    /// Generates path and moves the actor.  Location is not set in the function.
    /// </summary>
    /// <param name="unitObj">The actor you want to move</param>
    /// <returns>False = move not finished.  True = move finished.</returns>
    public bool moveUnit(Actor unitObj)
    {
        if (Vector3.Distance(unitObj.transform.position, TileCoordToWorldCoord(unitObj.tileX, unitObj.tileZ)) < 0.1f)
        {
            AdvancePathing();

        }

        //move unit to next tile
        endOfMove = unitObj.MoveController(unit.transform, TileCoordToWorldCoord(unitObj.tileX, unitObj.tileZ), unitObj.getSpeed());
        //transform.position = Vector3.MoveTowards(transform.position, map.TileCoordToWorldCoord(tileX, tileZ), speed * Time.deltaTime);


        if (endOfMove == true) //Anything that happens at end of Actor movement
        {
            unitObj.setRemainingMovement(0); // clears remaining movement of Actor at end of move

            if (unitObj.getCurrentPath() == null)
            {
                path.positionCount = 0; //clears line renderer
            }

        }

        return endOfMove;

    }

    void AdvancePathing()
    {

        //Unit doesn't move if there is no path
        if (unit.getCurrentPath() == null)
        {
            return;
        }

        if (unit.getCanMove() == false)
        {
            return;
        }

        //Actor runs out of movement points
        if (unit.getRemainingMovement() <= 0)
        {
            return;
        }

        remainingMovement = unit.getRemainingMovement();

        // Get cost from current tile to next tile
        remainingMovement -= costToEnterTile(unit.getCurrentPath()[0].x, unit.getCurrentPath()[0].z,
            unit.getCurrentPath()[1].x, unit.getCurrentPath()[1].z);

        unit.setRemainingMovement(remainingMovement);

        // Move to the next tile in the sequence
        unit.tileX = unit.getCurrentPath()[1].x;
        unit.tileZ = unit.getCurrentPath()[1].z;
        map[unit.getCurrentPath()[0].x,unit.getCurrentPath()[0].z].setOccupiedFalse();
        // Remove the old "current" tile from the pathfinding list
        unit.getCurrentPath().RemoveAt(0);



        if (unit.getCurrentPath().Count == 1)
        {
            //standing on same tile clicked on
            unit.setPathNull();
        }

        //checks if path is null, then sets tile under actor to occupied
        if (unit.getCurrentPath() == null)
        {
            map[unit.tileX, unit.tileZ].occupied = true;
        }
    }

    public void drawDebugLines()
    {
        
        if (unit.getCurrentPath() != null)
        {
            int currNode = 0;
            Vector3[] position = new Vector3[unit.getCurrentPath().Count+1];
            Vector3 start = new Vector3();
            Vector3 end = new Vector3();

            
            while (currNode < unit.getCurrentPath().Count - 1 &&
                unit.getCurrentPath().Count < unit.getMoveDistance() + 2)
            {
                start = TileCoordToWorldCoord(unit.getCurrentPath()[currNode].x, unit.getCurrentPath()[currNode].z) +
                    new Vector3(0, 1f, 0);
                end = TileCoordToWorldCoord(unit.getCurrentPath()[currNode + 1].x, unit.getCurrentPath()[currNode + 1].z) +
                    new Vector3(0, 1f, 0);

                Debug.DrawLine(start, end, Color.red);

                path.positionCount = unit.getCurrentPath().Count + 1;

                position[currNode] = start;

                path.SetPositions(position);

                currNode++;
                if (currNode == unit.getCurrentPath().Count - 1)
                {
                    //sets the last vector
                    position[currNode] = end;
                    //points the line into the tile
                    position[currNode + 1] = end - new Vector3(0, .5f, 0);
                    path.SetPositions(position);
                }
            }
        }
    }

    /// <summary>
    /// Move the selected actor to selected map position (map coords).
    /// </summary>
    /// <param name="actor">The actor you want to move</param>
    /// <param name="target">The map position you want to move to</param>
    /// <returns>False = move not finished.  True = move finished.</returns>
    public bool moveActor(GameObject actor, Vector3 target)
    {
        selectedUnit = actor;
        GeneratePathTo((int)target.x, (int)target.z);
        actor.GetComponent<Actor>().NextTurn();
        return moveUnit();
    }

    #endregion

    #region mouseEvents

    private void OnMouseEnter()
    {
        if (unit.getCurrentPath() != null)
        {

            position = new Vector3[unit.getCurrentPath().Count];
            path.SetPositions(position);
        }
    }

    private void OnMouseExit()
    {
        path.positionCount = 0;
    }

    #endregion

    #region setGets
    public void setTileCoords(int tileX, int TileZ)
    {
        tileCoords.x = tileX;
        tileCoords.z = TileZ;
    }
    public LineRenderer getLinePath()
    {
        return path;
    }

    public ClickableTile[,] getMapArray()
    {
        return map;
    }
    
    public bool getEndOfMove()
    {
        return endOfMove;
    }

    public void setTempCoords(int x, int z)
    {
        tempCoords.x = x;
        tempCoords.z = z;
    }
    public Vector3 getTempCoords()
    {
        return tempCoords;
    }
    #endregion
}