using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int tileX;
    public int tileZ;
    public Vector3 coords;
    public int tileClass;
    public TileMap map;
    public TileType tileType;
    private Color32 originalColor;
    Actor unit;
    public bool occupied = false;
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


    public void isOccupiedBy()
    {
        //should return what unit is standing on the tile
    }

    public void OnMouseExit()
    {
        //resets block back to original color after mouse pointer exits
        gameObject.GetComponent<MeshRenderer>().material.color = originalColor;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public bool isOccupied()
    {
        return occupied;
    }

    public void setOccupiedTrue()
    {
        occupied = true;
    }
    public void setOccupiedFalse()
    {
        occupied = false;
    }

    public Vector3 getCoords()
    {
        return coords;
    }

    public Vector3 getMapPosition()
    {
        Vector3 output = new Vector3(tileX, 0f, tileZ);
        return output;
    }
}

