using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Barracks
{

    public static bool purchasePlayer(PlayerData player)
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
            Debug.Log("Buying " + player.playerName + " for " + player.cost + " Shards");
            return true;
        }
    }

    /* public static bool purchaseItem(Item goods)
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
