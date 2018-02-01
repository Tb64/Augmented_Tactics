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
    protected int rangeDelta;
    private Vector3 startTileCoords;    

    /// <summary>
    /// Increments for each Actor found found. Resets to 0 at the start of an AOE cast.
    /// </summary>
    protected int listIterActor;

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
        canAffectFriendly = true;

        listOfActorsAffected = new Actor[8];
        listOfTilesAffected = new ClickableTile[256];

        TurnBehaviour.OnPlayerConfirmingAttack += HighlightArea;
        /*
        **THERE IS NO ON DESTROY METHOD. IS THIS A GOOD IDEA???
        */
    }

    //This will get all the tiles and actors to be hit, they will be stored in variables so no need to be wasteful and calculate again later
    private void HighlightArea()
    {
        Debug.Log("*************************");
        AOERange();
    }

    /// <summary>
    /// Highlights the area that will be affected 
    /// </summary>
    public virtual void AOEBase(GameObject target)
    {
        Vector3 targetCoords = target.transform.position;
        rangeDelta = AOESizeMax;
        listIterActor = 0;
        listIterTile = 0;

        //if clicked on gameobject is not a tile then get the coords under it, else use the clicked tile's coords
        if (target.tag != "Tile")
        {
            startTileCoords = new Vector3(targetCoords.x, 0, targetCoords.y);
        }
        else
        {
            startTileCoords = targetCoords;
        }
        CastAOE();        
    }

    private void CastAOE()
    {
    }

    /// <summary>
    /// This looks around on the clicked tile or target in a '+'-ish pattern. Can ovveride this for a custom pattern. 
    /// Pass a ClickableTile to the AddToList function to have that tile and anything standing on it included in the 'listOfTilesAffected', and 'listOfActorsAffected' per 'canAffectXX' booleans
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
                        Debug.Log("1");
                        AddToList(actor.map.GetTileAt(tileCoordsToCheck2));
                    }
                }
                if (actor.map.IsValidCoord(tileCoordsToCheck1))
                {
                    Debug.Log("2");
                    AddToList(actor.map.GetTileAt(tileCoordsToCheck1));
                }
            }
            rangeDelta--;
        }
    }

    void AddToList(ClickableTile cTile)
    {
        GameObject occupiedBy;

        listOfTilesAffected[listIterTile] = cTile;
        listIterTile++;

        if (cTile.isOccupied() == true)
        {
            Debug.Log("3");
            occupiedBy = cTile.isOccupiedBy();
            if (occupiedBy != null)
            {
                if (canAffectEnemy && occupiedBy.tag == "Enemy")
                {
                    listOfActorsAffected[listIterActor] = occupiedBy.GetComponent<Actor>();
                    Debug.Log("Found enemy: " + occupiedBy.name);
                    listIterActor++;
                }
                else if (canAffectFriendly && occupiedBy.tag == "Player")
                {
                    listOfActorsAffected[listIterActor] = occupiedBy.GetComponent<Actor>();
                    Debug.Log("Found player: " + occupiedBy.name);
                    listIterActor++;
                }
            }
            else
                Debug.Log("The occupiedBy function returned a null game object!");
        }
    }
    
}