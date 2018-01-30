using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : AOE
{
    protected int AOESizeMin;
    protected int AOESizeMax;
    private Vector3 startTileCoords;
    

    public Thunder(GameObject obj)
    {
        Initialize(obj);
    }

    public override void Initialize(GameObject obj)
    {
        base.Initialize(obj);

        AOESizeMin = 0;
        AOESizeMax = 2;

        anim = gameObject.GetComponentInChildren<Animator>();
        range_max = 3;
        range_min = 0;
        dwell_time = 1.0f;
        abilityName = "Thunder";
        abilityImage = Resources.Load<Sprite>("UI/Ability/magician/magicianSkill1");
        if (abilityImage == null)
            Debug.Log("Unable to load image");
    }

    public override bool UseSkill(GameObject target)
    {
        if (base.UseSkill(target))
        {
            Skill(target);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Skill(GameObject target)
    {
        Vector3 targetCoords = target.transform.position;
        int rangeDelta = AOESizeMax;

        if (anim != null)
        {
            Debug.Log(string.Format("Using Skill {0}.  Attacker={1} Defender={2}", abilityName, gameObject.name, target.name));
            rotateAtObj(target);
            anim.SetTrigger("MagicCast");
            gameObject.GetComponent<Actor>().PlaySound("attack");
        }

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
                        actor.map.GetTileAt(tileCoordsToCheck2).gameObject.SetActive(false);
                    }
                }
                if (actor.map.IsValidCoord(tileCoordsToCheck1))
                {
                    actor.map.GetTileAt(tileCoordsToCheck1).gameObject.SetActive(false);
                }
            }
            rangeDelta--;
        }
        /*
        for (int x = 0; x <= AOESize; x++)
        {
            for (int z = 0; z <= AOESize; z++)
            {
                tileCoordsToCheck = (startTile.getCoords() + new Vector3(x, 0, z));
                if(actor.map.IsValidCoord(tileCoordsToCheck))
                    actor.map.GetTileAt(tileCoordsToCheck).gameObject.SetActive(false);

                tileCoordsToCheck = (startTile.getCoords() + new Vector3(-x, 0, -z));
                if(actor.map.IsValidCoord(tileCoordsToCheck))
                    actor.map.GetTileAt(tileCoordsToCheck).gameObject.SetActive(false);
            }
        }*/
    }
}