using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is in charge with the spawning of the platforms.

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> buildingsPrefabs;
    private List<GameObject> positionedBuilds;
    public Transform buildingSpawningPoint;
    public Player player;
    public float playerDistance;

    void Start()
    {
        
        StartCoroutine(SpawnBuidings());
        //StartCoroutine(EraseBuidings());
    }

    void Update()
    {
        SpawnerMove();
    }

    void SpawnerMove()
    {
        buildingSpawningPoint.transform.Translate(new Vector3(
                                                        1,
                                                        0,
                                                        0
                                                        ) * player.speed * Time.deltaTime);
        //Moves the building spawner at the speed of the player
    }

    void Spawn()
    {
        GameObject building = buildingsPrefabs[Random.Range(0, 3)];
        Instantiate(building, buildingSpawningPoint.position, buildingSpawningPoint.rotation, transform);
        positionedBuilds.Add(building);
        //randomly spawns the building prefab at the position of the spawning point
    }

    IEnumerator SpawnBuidings()
    {
        int i = 4;
        while (i>0)
        //it will spawn as many building prefabs as the number of the level.
        {
            Spawn();
            i--;
            yield return new WaitForSeconds(5);
        }
    }

    IEnumerator EraseBuidings()
    {
        while (true) 
        //it will spawn as many building prefabs as the number of the level.
        {
            Destroy(positionedBuilds[0]);
            yield return new WaitForSeconds(2 * playerDistance / player.speed);
        }
    }


}
