using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barracks
{

    public bool purchasePlayer(PlayerData player)
    {
        if(player.cost > ShardController.getShards())
        {
            Debug.Log("Not Enough Shards");
            return false;
        }
        else
        {
            ShardController.spendShards(player.cost);
            player.unlockPlayer();
            return true;
        }
    }

    /* public bool purchaseItem(Item goods)
      {
         if(goods.cost > ShardController.getShards())
         {
             Debug.Log("Not Enough Shards");
             return false;
         }
         else
         {
             ShardController.spendShards(goods.cost);

             //here add to whichever item the person bought or add to inventory

             return true;
         }
      }*/
}
