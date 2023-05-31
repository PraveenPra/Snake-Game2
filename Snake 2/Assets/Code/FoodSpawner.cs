using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject FoodPrefab;
    public GameObject ground;
    public float maxTime = 10;
    public float counter = 0;
    private float offset = 4f;
    public bool triggerSpawn = false;

    void Start() {
          SpawnFood();
    }
    
    void Update()
    {
        counter += Time.deltaTime;

        if(triggerSpawn == true){
            triggerSpawn = false;
            SpawnFood();
        }

        // if (counter > maxTime)
        // {
        //     SpawnFood();
        //     counter = 0;
        // }
    }

    private void SpawnFood(){
          GameObject egg = Instantiate(FoodPrefab, Vector3.zero, Quaternion.identity);

    // Get a random position within a defined area or range
    Vector3 randomPosition = GetRandomPosition();

    // Set the position of the spawned egg
    egg.transform.position = randomPosition;
    }

    private Vector3 GetRandomPosition()
    {
        // Define the range or area where the eggs can spawn on the ground
        float minX = ground.transform.position.x - ground.transform.localScale.x / 2f;
        float maxX = ground.transform.position.x + ground.transform.localScale.x / 2f;
        float minY = ground.transform.position.y;
        // float maxY = ground.transform.position.y + FoodPrefab.transform.localScale.y / 2f;
        float minZ = ground.transform.position.z - ground.transform.localScale.z / 2f;
        float maxZ = ground.transform.position.z + ground.transform.localScale.z / 2f;

        // Generate random coordinates within the defined range
        float randomX = Random.Range(minX + offset, maxX - offset);
        float Y = minY;
        float randomZ = Random.Range(minZ + offset, maxZ - offset);

        // Create and return the random position
        return new Vector3(randomX , Y + 1.5f, randomZ );
    }

}
