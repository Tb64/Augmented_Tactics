using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;
    public float speed;
    public int moveSpeed;
    float step;
    float remainingMovement;
    public List<Node> currentPath = null;

    void Update()
    {
        if(currentPath != null)
        {
            int currNode = 0;
            
            while( currNode < currentPath.Count - 1 )
            {
                Vector3 start = map.TileCoordToWorldCoord(currentPath[currNode].x, currentPath[currNode].y) +
                    new Vector3(0,0,-1f);
                Vector3 end = map.TileCoordToWorldCoord(currentPath[currNode+1].x, currentPath[currNode+1].y) +
                    new Vector3(0, 0, -1f);

                Debug.DrawLine(start, end,Color.red);

                currNode++;
            }
        }
        //MoveNextTile();
        //step = speed * Time.deltaTime;
        //transform.position = Vector3.MoveTowards(transform.position, map.TileCoordToWorldCoord(tileX, tileY), step);
        //transform.position = Vector3.Lerp(transform.position, new Vector3(3,3), step);

      
        if (Vector3.Distance(transform.position, map.TileCoordToWorldCoord(tileX, tileY)) < 0.1f)
            AdvancePathing();

        //move unit to next tile
        transform.position = Vector3.MoveTowards(transform.position, map.TileCoordToWorldCoord(tileX, tileY), speed * Time.deltaTime);

    }


    void AdvancePathing()
    {

       
        if (currentPath == null)
            return;

        if (remainingMovement <= 0)
            return;


        // Get cost from current tile to next tile
        remainingMovement -= map.costToEnterTile(currentPath[0].x, currentPath[0].y, currentPath[1].x, currentPath[1].y);

        // Move us to the next tile in the sequence
        tileX = currentPath[1].x;
        tileY = currentPath[1].y;

        // Remove the old "current" tile from the pathfinding list
        currentPath.RemoveAt(0);

        if (currentPath.Count == 1)
        {
            //stading on same tile clicked on
            currentPath = null;
        }
    }

    public void NextTurn()
    {
        // Make sure to wrap-up any outstanding movement left over.
        //while (currentPath != null && remainingMovement > 0)
        //{
        //    AdvancePathing();
        //}

        // Reset our available movement points.
        remainingMovement = moveSpeed;
    }
}
