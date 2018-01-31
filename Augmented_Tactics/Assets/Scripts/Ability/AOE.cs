using System.Collections;
using System.Threading;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AOE : Ability
{
    protected bool canAffectFriendly;
    protected bool canAffectEnemy;
    protected bool canAffectTiles;
    protected int AOESizeMin;
    protected int AOESizeMax;
    protected int rangeDelta;
    private Vector3 startTileCoords;    

    /// <summary>
    /// Increments for each Actor found found. Resets to 0 at the start of an AOE cast.
    /// </summary>
    protected int listIter;

    /// <summary>
    /// Increments for each tile found. Resets to 0 at the start of an AOE cast.
    /// </summary>
    protected int listIterTile;

    /// <summary>
    /// This array contains all of the actors hit by the AOE. Held as Actor.
    /// </summary>
    protected Actor[] listOfActorsAffected;

    /// <summary>
    /// This array contains all the tiles hit by the AOE. Held as gameObjects.
    /// </summary>
    protected ClickableTile[] listOfTilesAffected;

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        canTargetTile = true;
        canTargetFriendly = true;
        canTargetEnemy = true;

        canAffectEnemy = true;
        canAffectFriendly = false;
        canAffectTiles = false;

        listOfActorsAffected = new Actor[8];
        listOfTilesAffected = new ClickableTile[256];
    }

    /// <summary>
    /// Highlights the area that will be affected 
    /// </summary>
    public virtual void AOEBase(GameObject target)
    {
        Vector3 targetCoords = target.transform.position;
        rangeDelta = AOESizeMax;
        listIter = 0;

        //if clicked on gameobject is not a tile then get the coords under it, else use the clicked tile's coords
        if (target.tag != "Tile")
        {
            startTileCoords = new Vector3(targetCoords.x, 0, targetCoords.y);
        }
        else
        {
            startTileCoords = targetCoords;
        }
        AOERange();        
    }

    /// <summary>
    /// This looks around on the clicked tile or target in a '+'-ish pattern. Can ovveride this for a custom pattern.
    /// </summary>
    public virtual void AOERange()
    {
        Vector3 tileCoordsToCheck1;
        Vector3 tileCoordsToCheck2;


        //Looks around the specified area for things it can affect
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
                            //it found that something on is that tile, send the object occupying the tile to the addToList method
                            AddToList(actor.map.GetTileAt(tileCoordsToCheck2).isOccupiedBy());
                            if (canAffectTiles)
                            {
                                AddToList(actor.map.GetTileAt(tileCoordsToCheck2).gameObject);
                            }
                        }
                    }
                }
                if (actor.map.IsValidCoord(tileCoordsToCheck1))
                {
                    //actor.map.GetTileAt(tileCoordsToCheck1).gameObject.SetActive(false);
                    if (actor.map.GetTileAt(tileCoordsToCheck1).isOccupied())
                    {
                        AddToList(actor.map.GetTileAt(tileCoordsToCheck1).isOccupiedBy());
                        if (canAffectTiles)
                        {
                            AddToList(actor.map.GetTileAt(tileCoordsToCheck1).gameObject);
                        }
                    }
                }
            }
            rangeDelta--;
        }
    }

    void AddToList(GameObject gObj)
    {        
        if (gObj != null)
        {
            Debug.Log("Has tag: " + gObj.tag);
            if (canAffectEnemy && gObj.tag == "Enemy")
            {
                listOfActorsAffected[listIter] = gObj.GetComponent<Actor>();
                //Debug.Log("adding" + gObj);
                listIter++;
            }
            else if (canAffectFriendly && gObj.tag == "Player")
            {
                listOfActorsAffected[listIter] = gObj.GetComponent<Actor>();
                //Debug.Log("adding" + gObj + " and trying to access" + gObj.GetComponent<Actor>().name);
                listIter++;
            }
            else if (gObj.tag == "Tile")
            {
                listOfTilesAffected[listIterTile] = gObj.GetComponent<ClickableTile>();
            }
        }
        else
            Debug.Log("The AOE attack returned a null game object!");
    }
    
}