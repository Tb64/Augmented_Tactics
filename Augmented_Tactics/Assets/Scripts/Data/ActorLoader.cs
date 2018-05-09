using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorLoader : MonoBehaviour {

    public int PlayerSlot;

    public void Start()
    {
        PlayerData playerData = LoadPlayerData();
        if(playerData == null || playerData.DisplayName == null || playerData.DisplayName.Length == 0)
        {
            DebugMobile.Log("Null Player Data");
            Destroy(gameObject);
            return;
        }
        GameObject actorObj = Resources.Load<GameObject>(playerData.Prefab);
        actorObj.GetComponent<PlayerControlled>().combatOn = true;
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
            DebugMobile.Log("PlayerSlot is out of range");
            return null;
        }
        return GameDataController.gameData.currentTeam[PlayerSlot];
    }
}
