﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class TileMap : MonoBehaviour {

    #region variables
    //public GameObject selectedUnit;
    float remainingMovement;
    public bool codeGenerateMap = true;
    LineRenderer path;
    public ClickableTile[,,] map;
    private Actor unit;
    
    Vector3[] position;
    Vector3 tileCoords = new Vector3();

    Vector3 tempCoords = new Vector3();

    private bool canMove;
    private bool endOfMove;

    //int[,] tiles;
    Nodes[,,] graph;
    
    public int mapSizeX = 16;
    public int mapSizeY = 1;
    public int mapSizeZ = 16;
    #endregion
    // Use this for initialization

    void Start() {
        
    }

    private void Awake()
    {
        Application.stackTraceLogType = StackTraceLogType.ScriptOnly;
        initialize();
        TurnBehaviour.OnActorFinishedMove += this.PlayerMoveActions;
    }

    //use this function to initializes variables
    void initialize()
    {
        map = new ClickableTile[mapSizeX,mapSizeY,mapSizeZ];
        endOfMove = true;
        if (GameObject.Find("Path").GetComponent<LineRenderer>() == null)
        {
            Debug.LogError("Null reference, missing path object, add in scene hierarchy");
            return;
        }

        path = GameObject.Find("Path").GetComponent<LineRenderer>();

        //sets unit to the selected unit in the map
        //unit = selectedUnit.GetComponent<Actor>();
        Vector3 coordinates = new Vector3();

        //initializes coordinates vector to selected units transform
        //coordinates.x = (int)selectedUnit.transform.position.x;
        //coordinates.z = (int)selectedUnit.transform.position.z;
        //passes coordinates vector to unit to set units coords
        //unit.setCoords(coordinates);

        //selectedUnit.GetComponent<Actor>().tileX = (int)selectedUnit.transform.position.x;
        //selectedUnit.GetComponent<Actor>().tileZ = (int)selectedUnit.transform.position.z;
        
        //selectedUnit.GetComponent<Actor>().map = this;

        Actor player = GameObject.FindWithTag("Player").GetComponent<Actor>();
        Actor enemy = GameObject.FindWithTag("Enemy").GetComponent<Actor>();

        if (codeGenerateMap)
        {
            GenerateMapData();
            //GenerateMapVisual();
        }
        else
        {
            LoadTileData();
        }

        generatePathFindingGraph();
    }

    void LoadTileData()
    {
        //tiles = new int[mapSizeX, mapSizeZ];
        map = new ClickableTile[mapSizeX, mapSizeY, mapSizeZ];
        GetComponent<TilePosGenerator>().generatePositions();
        ClickableTile[] loadedTiles = GetComponentsInChildren<ClickableTile>();
        
        foreach (ClickableTile ctTile in loadedTiles)
        {
            ctTile.map = this;
            //tiles[(int)ctTile.coords.x, (int)ctTile.coords.z] = ctTile.tileClass;
            setTileAtCoord(ctTile.coords, ctTile);
        }
        
    }

    public ClickableTile getTileAtCoord(Vector3 coords) {
        if (coords.x < 0 || coords.x > mapSizeX)
            return null;

        if (coords.y < 0 || coords.y > mapSizeY)
            return null;

        if (coords.z < 0 || coords.z > mapSizeZ)
            return null;

        return map[(int)coords.x, (int)coords.y, (int)coords.z];
    }

    public void setTileAtCoord(Vector3 coords, ClickableTile cT)
    {
        map[(int)coords.x, (int)coords.y, (int)coords.z] = cT;
    }

    void GenerateMapData()
    {
        //allocate tiles
        //tiles = new int[mapSizeX, mapSizeZ];

        //init tiles
        //for (int x = 0; x < mapSizeX; x++)
        //{
        //    for (int z = 0; z < mapSizeZ; z++)
        //    {
        //        //tiles[x, z] = 0;
                
        //    }
        //}
    }

    void GenerateMapVisual()
    {
        //for (int x = 0; x < mapSizeX; x++)
        //{
        //    for (int z = 0; z < mapSizeZ; z++)
        //    {
        //        //TileType tt = tileTypes[tiles[x, z]];
        //        GameObject go = (GameObject)Instantiate(tt.tileVisualPrefab, new Vector3(x, 0, z), Quaternion.identity);
        //        go.transform.parent = transform;
        //        ClickableTile ct = go.GetComponent<ClickableTile>();
        //        ct.tileX = x;
        //        ct.tileZ = z;
        //        ct.map = this;
        //        map[x, z] = ct;
        //    }
        //}
    }

    /// <summary>
    /// Returns the Global Position for a given Tile Coordinate.
    /// NOTE: Height/y adds 0.5f
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public Vector3 TileCoordToWorldCoord(int x, int z)
    {
        Vector3 coords = new Vector3(x, 0f, z);
        return TileCoordToWorldCoord(coords);
    }

    /// <summary>
    /// Returns the Global Position for a given Tile Coordinate.
    /// NOTE: Height/y adds 0.5f
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public Vector3 TileCoordToWorldCoord(Vector3 input)
    {
        Vector3 worldCoords = getTileAtCoord(input).transform.position;
        worldCoords.y += 0.5f;
        return worldCoords;
    }

    public bool IsValidCoord(Vector3 input)
    {
        if (input.x < 0 || input.x > mapSizeX)
            return false;

        if (input.y < 0 || input.y > mapSizeY)
            return false;

        if (input.z < 0 || input.z > mapSizeZ)
            return false;

        if ( getTileAtCoord(input) == null)
        {
            return false;
        }
 
        return true;
    }

    public float costToEnterTile(Vector3 source, Vector3 target)
    {
        TileType tt = getTileAtCoord(target).tileType;

        if(UnitCanEnterTile(target) == false)
        {
            return Mathf.Infinity;
        }

        float cost = tt.movementCost;
        if(source != target){
            //moving diagonally
            cost += 0.001f;// done to prefer straight lines over diagonal lines
        }
        
       return cost;
        
    }

    public bool UnitCanEnterTile(Vector3 coords)
    {
        if (!IsValidCoord(coords))
            return false;
        //could test units movement type(walk,fly,run etc..)
        return getTileAtCoord(coords).tileType.isWalkable && !getTileAtCoord(coords).isOccupied();
    }

    public void GeneratePathTo(Vector3 targetCoords, GameObject actor)
    {
        unit = actor.GetComponent<Actor>();
        //Vector3 coordinates = new Vector3();
        
        //initializes coordinates vector to selected units transform
        //coordinates.x = (int)unit.transform.position.x;
        //coordinates.z = (int)unit.transform.position.z;
        //passes coordinates vector to unit to set units coords
        //unit.setCoords(coordinates); 
        unit.setPathNull();

        if (UnitCanEnterTile(targetCoords) == false || getTileAtCoord(targetCoords).isOccupied() == true)
        {//tile is not walkable
            //Debug.Log("Unable to generate path");
            return;
        }

        Dictionary<Nodes, float> dist = new Dictionary<Nodes, float>();
        Dictionary<Nodes, Nodes> prev = new Dictionary<Nodes, Nodes>();

        //Actor uXZ = selectedUnit.GetComponent<Actor>();

        List<Nodes> unvisited = new List<Nodes>();

        Nodes source = graph[(int)unit.getCoords().x,(int)unit.getCoords().y,(int)unit.getCoords().z];
        Nodes target = graph[(int)targetCoords.x,(int)targetCoords.y,(int)targetCoords.z];
        dist[source] = 0;
        prev[source] = null;
        
        foreach(Nodes v in graph)
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
            Nodes u = null;

            foreach(Nodes possibleU in unvisited)
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

            foreach (Nodes v in u.neighbors)
            {
                //float alt = dist[u] + u.DistanceTo(v);
                float alt = dist[u] + costToEnterTile(u.coords,v.coords);

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

        List<Nodes> currentPath = new List<Nodes>();

        Nodes curr = target;

        //step through prev chain and add it to path

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        currentPath.Reverse(); //inverts the path
        unit.setCurrentPath(currentPath);
    }

    public List<Nodes> GeneratePathTo(Vector3 targetCoords, Vector3 startingCoords)
    {

        if (UnitCanEnterTile(targetCoords) == false || getTileAtCoord(targetCoords).isOccupied() == true)
        {//tile is not walkable
            //Debug.Log("Unable to generate path");
            return null;
        }

        Dictionary<Nodes, float> dist = new Dictionary<Nodes, float>();
        Dictionary<Nodes, Nodes> prev = new Dictionary<Nodes, Nodes>();

        //Actor uXZ = selectedUnit.GetComponent<Actor>();

        List<Nodes> unvisited = new List<Nodes>();

        Nodes source = graph[(int)startingCoords.x, (int)startingCoords.y, (int)startingCoords.z];
        Nodes target = graph[(int)targetCoords.x, (int)targetCoords.y, (int)targetCoords.z];
        dist[source] = 0;
        prev[source] = null;

        foreach (Nodes v in graph)
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
            Nodes u = null;

            foreach (Nodes possibleU in unvisited)
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

            foreach (Nodes v in u.neighbors)
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
            return null;
        }

        List<Nodes> currentPath = new List<Nodes>();

        Nodes curr = target;

        //step through prev chain and add it to path

        while (curr != null)
        {
            currentPath.Add(curr);
            curr = prev[curr];
        }

        return currentPath;
    }

    void generatePathFindingGraph()
    {
        //init array
        graph = new Nodes[mapSizeX,mapSizeY,mapSizeZ];

        //init a node for each index in array
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                for (int z = 0; z < mapSizeZ; z++)
                {
                    graph[x,y,z] = new Nodes();
                    graph[x,y,z].coords.x = x;
                    graph[x,y,z].coords.y = y;
                    graph[x,y,z].coords.z = z;
                }     
            }
        }

        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                for (int z = 0; z < mapSizeZ; z++)
                {

                //4 way movement

                if (x > 0)
                {
                    graph[x,y,z].neighbors.Add(graph[x - 1,y,z]);
                }
                if (x < mapSizeX - 1)
                {
                    graph[x,y,z].neighbors.Add(graph[x + 1,y,z]);
                }
                if (z > 0)
                {
                    graph[x,y,z].neighbors.Add(graph[x,y,z - 1]);
                }
                if (z < mapSizeZ - 1)
                {
                    graph[x,y,z].neighbors.Add(graph[x,y,z + 1]);
                }
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
    /// DO NOT LOOP. Call this once to have the Actor move to Target.
    /// </summary>
    /// <param name="actor">The Actor you want to move</param>
    /// <param name="target">The Map position you want to move to</param>
    /// <returns></returns>
    public void moveActorAsync(GameObject actor, Vector3 target)
    {
        Actor actorObj = actor.GetComponent<Actor>();
        if (actorObj.getCoords() == target || !UnitCanEnterTile(target))
        {
            Debug.Log("Move Failed: Target is invalid. " + target + " " + actor.name);
            return;
        }

        actorObj.PlaySound("move");
        if (actorObj.useAction())
        {
            actorObj.PlaySound("move");
            StartCoroutine(MoveActorThread(actor, target));
        }
        else
            Debug.Log("No actions remaining.");
        return;
    }

    /// <summary>
    /// DO NOT LOOP. Call this once to have the Actor move to Target.
    /// </summary>
    /// <param name="actor">The Actor you want to move</param>
    /// <param name="target">The Map position you want to move to</param>
    /// <returns></returns>
    public void moveActorAsync(Actor actor, Vector3 target)
    {
        moveActorAsync(actor.gameObject, target);
    }

    /// <summary>
    /// MUST BE IN LOOP. Move the selected actor to selected map position (map coords).
    /// </summary>
    /// <param name="actor">The GameObject you want to move</param>
    /// <param name="target">The map position you want to move to</param>
    /// <returns>False = move not finished.  True = move finished.</returns>
    public bool moveActor(GameObject actor, Vector3 target)
    {
        //selectedUnit = actor;
        GeneratePathTo(target, actor);
        return moveUnit(actor);
    }

    /// <summary>
    /// MUST BE IN LOOP. Move the selected actor to selected map position (map coords).
    /// </summary>
    /// <param name="actor">The actor you want to move</param>
    /// <param name="target">The map position you want to move to</param>
    /// <returns>False = move not finished.  True = move finished.</returns>
    public bool moveActor(Actor actor, Vector3 target)
    {
        return moveActor(actor.gameObject, target);
    }

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
    /// <param name="unit">GameObject of the Actor</param>
    /// <returns>False = move not finished.  True = move finished.</returns>
    public bool moveUnit(GameObject unit)
    {
        return moveUnit(unit.GetComponent<Actor>());
    }

    /// <summary>
    /// Generates path and moves the actor.  Location is not set in the function.
    /// </summary>
    /// <param name="unitObj">The actor you want to move</param>
    /// <returns>False = move not finished.  True = move finished.</returns>
    public bool moveUnit(Actor unitObj)
    {
        if (Vector3.Distance(unitObj.transform.position, TileCoordToWorldCoord(unitObj.getCoords())) < 0.27f)
        {
            AdvancePathing();
        }

        //move unit to next tile
        endOfMove = unitObj.MoveController(unit.transform, TileCoordToWorldCoord(unitObj.getCoords()), unitObj.getSpeed());
        //Debug.Log("endOfMove: " + endOfMove);
        
        if (endOfMove == true) //Anything that happens at end of Actor movement
        {
            unitObj.setRemainingMovement(0); // clears remaining movement of Actor at end of move
            //unitObj.useAction();  //moving to thread
            
            if(unitObj.canAct() == true)
            {
                unitObj.setRemainingMovement(unitObj.getMoveDistance());
            }

            if (unitObj.getCurrentPath() == null)
            {
                path.positionCount = 0; //clears line renderer
            }
        }
        return endOfMove;
    }

    public void PlayerMoveActions()
    {
        Debug.Log("PLAYER HAS MOVED");
    }

    void AdvancePathing()
    {

        //Unit doesn't move if there is no path
        if (unit.getCurrentPath() == null)
        {
            return;
        }

        //Actor runs out of movement points
        if (unit.getRemainingMovement() <= 0)
        {
            return;
        }

        remainingMovement = unit.getRemainingMovement();

        //Get cost from current tile to next tile
        remainingMovement -= costToEnterTile(unit.getCurrentPath()[0].coords,unit.getCurrentPath()[1].coords);

        unit.setRemainingMovement(remainingMovement);
        Debug.Log("ADV path = " + unit.getCurrentPath()[1].coords);
        // Move to the next tile in the sequence
        //unit.tileX = (int)unit.getCurrentPath()[1].coords.x;
        //unit.tileZ = (int)unit.getCurrentPath()[1].coords.z;
        unit.setCoords(unit.getCurrentPath()[1].coords);

        //map[(int)unit.getCurrentPath()[0].coords.x,
        //    (int)unit.getCurrentPath()[0].coords.y,
        //    (int)unit.getCurrentPath()[0].coords.z].setOccupiedFalse();

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
            //getTileAtCoord(unit.getCoords()).setOccupiedTrue();
            //map[(int)unit.getCoords().x, (int)unit.getCoords().y, (int)unit.getCoords().z].setOccupiedTrue();
            Debug.Log("unit coords : " + unit.getCoords());
        }
    }

    public void drawDebugLines()
    {

        if (unit == null)
        {
            return;
        }
        if (unit.getCurrentPath() != null)
        {
            int currNode = 0;

            Vector3[] position = new Vector3[unit.getCurrentPath().Count+1];
            Vector3 start = new Vector3();
            Vector3 end = new Vector3();

            while (currNode < unit.getCurrentPath().Count - 1 &&
                unit.getCurrentPath().Count < unit.getMoveDistance() + 2)
            {
                start = TileCoordToWorldCoord((int)unit.getCurrentPath()[currNode].coords.x, (int)unit.getCurrentPath()[currNode].coords.z) +
                    new Vector3(0, 1f, 0);
                end = TileCoordToWorldCoord((int)unit.getCurrentPath()[currNode + 1].coords.x, (int)unit.getCurrentPath()[currNode + 1].coords.z) +
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
    /// Sets occupied of current actor location to false, and target location to true.
    /// Takes an Actor gameobject, its current coords and target coords as a parameter.
    /// </summary>
    /// <param name="Actor"></param>
    /// <param name="currentCoords"></param>
    /// <param name="targetCoords"></param>
    public void SetOcc(GameObject Actor, Vector3 currentCoords, Vector3 targetCoords)
    {
        Actor unit = Actor.GetComponent<Actor>();
        //Debug.Log("SETOCC INITIALIZED");
        unit.setCoords(targetCoords);
        map[(int)currentCoords.x, (int)currentCoords.y, (int)currentCoords.z].setOccupiedFalse();
        Debug.Log("current coords" + currentCoords);
        map[(int)targetCoords.x, (int)targetCoords.y, (int)targetCoords.z].setOccupiedTrue(Actor);
    }

    private IEnumerator MoveActorThread(GameObject actor, Vector3 target)
    {
        //selectedUnit = actor;
        GeneratePathTo(target, actor);
        List<Nodes> path = actor.GetComponent<Actor>().getCurrentPath();
        string debugout = "Generated Path: ";
        foreach (Nodes node in path)
        {
            debugout += node.coords + " >> ";
        }

        Debug.Log(debugout);

        Vector3 currentCoords = actor.GetComponent<Actor>().getCoords();

        TurnBehaviour.ActorBeginsMoving();
        bool moveDone = moveUnit(actor);

        do
        {
            //Debug.Log("Move Done :" + moveDone);
            moveDone = moveUnit(actor);
            yield return null;
        }
        while (!moveDone);

        

        Vector3 newCoords = actor.GetComponent<Actor>().getCoords();

        Debug.Log("Moved " + actor.name + " to " + target + " from " + currentCoords);
        SetOcc(actor, currentCoords, newCoords);
        //getTileAtCoord(unit.getCoords()).setOccupiedTrue(actor);
        //actor.GetComponent<Actor>().useAction();
        TurnBehaviour.ActorHasJustMoved();
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
    public void setTileCoords(int tileX, int tileZ)
    {
        tileCoords.x = tileX;
        tileCoords.z = tileZ;
    }
    public LineRenderer getLinePath()
    {
        return path;
    }

    public ClickableTile[,,] getMapArray()
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

    public ClickableTile GetTileAt(Vector3 mapPos)
    {
        ClickableTile tile = map[(int)mapPos.x,(int)mapPos.y,(int)mapPos.z];
        return tile;
    }
    #endregion
}