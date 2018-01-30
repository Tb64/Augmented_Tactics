using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AOE : Ability
{
    protected bool canAffectFriendly;
    protected bool canAffectEnemy;
    protected int AOESizeMin;
    protected int AOESizeMax;
    private Vector3 startTileCoords;
    protected Actor[] listOfAffected;
    private int listIter = 0;

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        canTargetTile = true;
        canTargetFriendly = true;
        canTargetEnemy = true;

        canAffectEnemy = true;
        canAffectFriendly = true;

        listOfAffected = new Actor[8];
    }

    /// <summary>
    /// Highlights the area that will be affected 
    /// </summary>
    public virtual void AOEBase(GameObject target)
    {
        Vector3 targetCoords = target.transform.position;
        int rangeDelta = AOESizeMax;

        //if not tile get the coords under it, else use the tile's coords
        if (target.tag != "Tile")
        {
            startTileCoords = new Vector3(targetCoords.x, 0, targetCoords.y);
        }
        else
        {
            startTileCoords = targetCoords;
        }

        Vector3 tileCoordsToCheck1;
        Vector3 tileCoordsToCheck2;
        
        for (int x = AOESizeMin; x <= AOESizeMax; x++)
        {
            for (int z = -rangeDelta; z <= rangeDelta; z++)
            {
                tileCoordsToCheck1 = new Vector3(startTileCoords.x + x, startTileCoords.y, startTileCoords.z + z);
                tileCoordsToCheck2 = new Vector3(startTileCoords.x - x, startTileCoords.y, startTileCoords.z + z);
                if (tileCoordsToCheck1 != tileCoordsToCheck2)
                {
                    if (actor.map.IsValidCoord(tileCoordsToCheck2))
                    {
                        //actor.map.GetTileAt(tileCoordsToCheck2).gameObject.SetActive(false);
                        if (actor.map.GetTileAt(tileCoordsToCheck2).isOccupied())
                        {
                            addToList(actor.map.GetTileAt(tileCoordsToCheck2).isOccupiedBy());
                        }
                    }
                }
                if (actor.map.IsValidCoord(tileCoordsToCheck1))
                {
                    //actor.map.GetTileAt(tileCoordsToCheck1).gameObject.SetActive(false);
                    if (actor.map.GetTileAt(tileCoordsToCheck1).isOccupied())
                    {
                        addToList(actor.map.GetTileAt(tileCoordsToCheck1).isOccupiedBy());
                    }
                }
            }
            rangeDelta--;
        }
        listIter = 0;
    }

    void addToList(GameObject gObj)
    {
        Debug.Log("Has tag: " + gObj.tag);
        if (canAffectEnemy && gObj.tag == "Enemy")
        {
            listOfAffected[listIter] = gObj.GetComponent<Actor>();
            Debug.Log("adding" + gObj);
            listIter++;
        }
        if (canAffectFriendly && gObj.tag == "Player")
        {
            listOfAffected[listIter] = gObj.GetComponent<Actor>();
            Debug.Log("adding" + gObj + " but trying to get" + gObj.GetComponent<Actor>().name);
            listIter++;
        }
    }
    
}