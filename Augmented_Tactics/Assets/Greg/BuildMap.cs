using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class BuildMap : MonoBehaviour{
    //reference tile prefab
    public GameObject tile;
    //scale for tile size
    float tileSize = 1.0f;
    //map start position information
    Quaternion rotation;
    Vector3 seed;
    //simple rectangle map dimensions
    int xSize = 50;
    int zSize = 50;
    public List<GameObject> tiles;
    void Start(){
        Debug.Log("Start");
        rotation = Camera.main.transform.localRotation;
        seed = Camera.main.transform.forward * 5;
        buildArea(xSize, zSize);
    }

    void buildArea(int xSize, int zSize){
        //offset from seed location
        int xIndex = 0;
        int zIndex = 0;
        Vector3 offset;
        //count to store tile objects
        int count = 0;
        //instantiates objects, offsets them, and gives then a position
        for (; xIndex < xSize; xIndex++){
            for (; zIndex < zSize; zIndex++){
                Debug.Log("Object being created.");
                offset = new Vector3(xIndex, 0, zIndex);
                tiles[count] = (GameObject)Instantiate(tile, seed + offset, rotation);
                //GameObject tileInstance = (GameObject)Instantiate(tile, seed + offset, rotation);
                tiles[count].GetComponent<TileData>().position = offset;
                count++;
                Debug.Log("Object Created");
            }
        }
    }
    /*
    //tile size
    float tileSize = 1.0f;
    //size refers to size of map, pos refers to coords
    static int xSize;
    int xPos = 0;
    //int ySize reserved for height
    static int zSize;
    int zPos = 0;
    //vectors that handle orienation of the map in 3D space
    static Vector3 unitVector = new Vector3(0, 0, 1);
    //Quaternion angle = new Quaternion(0, 0, 0, 0);
    Vector3 mapOrientation = Quaternion.Euler(0, 0, 0) * unitVector;
    Vector3 source = new Vector3(0, 0, 0);
    //total vertex count
    static int vertCount = 2 * (xSize + 1) * (zSize + 1);
    Vector3[] vertices = new Vector3[vertCount];
    

    void Start () {
        buildArea();
	}
	
    void buildTile(int xPos, int zPos, bool check){
        //tile position
        int x = xPos;
        int z = zPos;
        //8 vertices per cube
        int localVert = 8;
        //1 normal per vertex = smooth edges, 2 = sharp edges
        Vector3[] normals = new Vector3[localVert];
        //uv is used to properly map 2D texture to 3D object
        Vector2[] uv = new Vector2[localVert];
        //12 triangles per cube; triangle = 3 vertices
        int[] triangles = new int[12 * 3];


    }

    void buildArea(){
        //initial attempt at basing the map off of variable posistion
        int indexX = (int)source.x;
        int indexZ = (int)source.z;
        //+1 as there is one more layer of vertices than the size
        int xLimit = indexX + xSize + 1;
        int zLimit = indexZ + zSize + 1;
        //count to show break point of upper and lower layer of vertices
        int count = 0;
        bool check = false;

        for (; indexX < xLimit; indexX++){
            for (; indexZ < zLimit; indexZ++){
                //
                vertices[indexX * (zSize + 1) + indexZ] = mapOrientation + new Vector3(indexX * tileSize, 0, indexZ * tileSize);
                if (!check && count > vertCount / 2)
                    check = true;
                buildTile(xPos, zPos, check);
                count++;
                zPos++;
            }
            xPos++;
        }
    }*/
}
