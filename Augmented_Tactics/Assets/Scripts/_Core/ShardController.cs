using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShardController
{
    public static int shardCount;
    public static void setShards(int amount)
    {
        shardCount = amount;
    }

    public static int getShards()
    {
        return shardCount;
    }

    public static void spendShards(int amount)
    {
        shardCount -= amount;
    }

    public static void earnShards(int amount)
    {
        shardCount += amount;
    }
}
