using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorLoader : MonoBehaviour {

    public int PlayerSlot;

    public void Awake()
    {
        PlayerData playerData = LoadPlayerData();
        if(playerData == null)
        {
            Debug.Log("Player Data null");
            Destroy(gameObject);
            return;
        }
        GameObject actorObj = Resources.Load<GameObject>(playerData.Prefab);
        GameObject spawned = Instantiate(actorObj);
        spawned.transform.position = transform.position;
        spawned.transform.rotation = transform.rotation;
        spawned.GetComponent<Actor>().LoadStatsFromData(playerData);
        Destroy(gameObject);
    }

    private PlayerData LoadPlayerData()
    {
        
        GameDataController.loadPlayerData();
        if(PlayerSlot < 0 || PlayerSlot >= GameDataController.gameData.currentTeam.Length)
        {
            Debug.Log("PlayerSlot is out of range");
            return null;
        }
        return GameDataController.gameData.currentTeam[PlayerSlot];
    }
}
