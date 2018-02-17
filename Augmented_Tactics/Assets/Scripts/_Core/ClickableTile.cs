using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileZ;
    public Vector3 coords;
    public int tileClass;
    private GameObject occupiedBy;
    public TileMap map;
    public TileType tileType;
    private Color32 originalColor;
    Actor unit;
    private bool occupied = false;
    StateMachine controller;
    Actor player;
    Actor enemy;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Actor>();
        enemy = GameObject.FindWithTag("Enemy").GetComponent<Actor>();
        //unit = GameObject.FindWithTag("Map").GetComponent<TileMap>().selectedUnit.GetComponent<Actor>();
        controller = GameObject.Find("GameController").GetComponent<StateMachine>();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with " + other.gameObject.name);
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
        {
            Debug.Log("Collison with actor.");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collison with actor.");
        }
    }

    private void Update()
    {
     
    }

    /// <summary>
    /// Will return GameObject occupuying the tiles. Only returns gameobject if tile
    /// is currently occupied
    /// </summary>
    /// <returns></returns>
    public GameObject isOccupiedBy()
    {
        return occupiedBy;
    }

    public void OnMouseExit()
    {
        //resets block back to original color after mouse pointer exits
        //gameObject.GetComponent<MeshRenderer>().material.color = originalColor;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    /// <summary>
    /// Will return whether the tile is currently occupied.
    /// </summary>
    /// <returns> returns a bool </returns>
    public bool isOccupied()
    {
        return occupied;
    }

    public void setOccupiedTrue(GameObject actor)
    {
        occupiedBy = actor;
        occupied = true;
    }

    public void setOccupiedFalse()
    {
        occupied = false;
        occupiedBy = null;
    }

    public Vector3 getCoords()
    {
        return coords;
    }

    [System.Obsolete("Use getCoords() instead.")]
    public Vector3 getMapPosition()
    {
        Vector3 output = new Vector3(tileX, 0f, tileZ);
        return output;
    }
}

