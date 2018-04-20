using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorLoader : MonoBehaviour {

    public int PlayerSlot;

    public void Awake()
    {
        PlayerData playerData = LoadPlayerData();
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

        return GameDataController.gameData.currentTeam[PlayerSlot];
    }
}
