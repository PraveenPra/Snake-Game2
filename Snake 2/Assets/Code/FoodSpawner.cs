using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public GameObject FoodPrefab;
    public GameObject PowerUpFoodPrefab;
    public GameObject ground;
    private float powerupDelay = 25;
    private float powerupCounter = 0;
    private float offset = 4f;
    public bool triggerSpawn = false;
    private GameObject item;

    void Start()
    {
        SpawnFood("NormalFood");
    }

    void Update()
    {
        powerupCounter += Time.deltaTime;

        if (triggerSpawn == true)
        {
            triggerSpawn = false;
            SpawnFood("NormalFood");
        }

        if (powerupCounter > powerupDelay)
        {
            SpawnFood("PowerUpFood");
            powerupCounter = 0;
        }
    }

    private void SpawnFood(string foodtype)
    {
        if(foodtype == "NormalFood"){
             item = FoodPrefab;
        }else{
              item = PowerUpFoodPrefab;
        }


        GameObject egg = Instantiate(item, Vector3.zero, Quaternion.identity);

        // Get a random position within a defined area or range
        Vector3 randomPosition = GetRandomPosition();

        // Set the position of the spawned egg
        egg.transform.position = randomPosition;
    }

    private Vector3 GetRandomPosition()
    {
        Bounds groundBounds = ground.GetComponent<Renderer>().bounds;

        // Generate random coordinates within the defined range
        float randomX = Random.Range(groundBounds.min.x + offset, groundBounds.max.x - offset);
        float randomZ = Random.Range(groundBounds.min.z + offset, groundBounds.max.z - offset);

        // Create and return the random position
        return new Vector3(randomX, groundBounds.min.y + 1.5f, randomZ);
    }

}
