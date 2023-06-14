using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject[] tilePrefabs;
    public float zSpawn = 80;
    public float tileLegth = 200;
    public int numberOfTiles = 2;
    public Transform playerTransform;
    private List<GameObject> activeTiles = new List<GameObject>(); 
    void Start()
    {
        // Spawn Maps
        for (int i = 0; i < numberOfTiles; i++)
        {
            if(i==0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(1, tilePrefabs.Length));
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(playerTransform.position.z -150 > zSpawn - (numberOfTiles*tileLegth))
        {
            SpawnTile(Random.Range(1,tilePrefabs.Length));
            DeleteTile();
        }
        
    }
    // Spawn Ground for Z Lane
    public void SpawnTile(int tileIndex)
    {
       GameObject go = Instantiate(tilePrefabs[tileIndex],transform.forward * zSpawn , transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLegth;    
    }
    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
