using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int tileX;
    public int tileY;
    public TileMap map;
    public float speed;

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
        MoveNextTile();
    }

    public void MoveNextTile()
    {//remove old current/first node from path
        if (currentPath == null)
        {
            return;
        }
        currentPath.RemoveAt(0);
        //Grab new first node and move to that position
        tileX = currentPath[0].x;
        tileY = currentPath[0].y;


        Vector3 target = map.TileCoordToWorldCoord(currentPath[0].x, currentPath[0].y);

        Debug.Log("X " + currentPath[0].x + "y " + currentPath[0].y);

        float step = speed * Time.deltaTime;

        while (transform.position != target)
        {
            step = speed * Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, target, step);
        }
            
        if(currentPath.Count == 1)
        {// target is at end of path(player is standing on it)
            currentPath = null;

        }
    }

}
