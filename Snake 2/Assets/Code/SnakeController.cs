using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float steerSpeed = 100;
    public GameObject BodyPrefab;
    public FoodSpawner food_spawner;
    private float powerupTimer = 5;

    private int gap = 20;
    private float bodySpeed = 5;
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> HeadPositionHistory = new List<Vector3>();


    void Start()
    {
        GrowSnake();

    }

    // Update is called once per frame
    void Update()
    {
        //move
        transform.position += transform.forward * moveSpeed * Time.deltaTime;

        //steer
        float steerDir = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up * steerDir * steerSpeed * Time.deltaTime);

        //To fly
        // float flyDir = Input.GetAxis("Vertical");
        // transform.Rotate(Vector3.left * flyDir * steerSpeed * Time.deltaTime);

        //store snake head positions
        HeadPositionHistory.Insert(0, transform.position);

        //move the each body parts to that headposition list
        int index = 0;
        foreach (var body in BodyParts)
        {
            Vector3 point = HeadPositionHistory[Mathf.Min(index * gap, HeadPositionHistory.Count - 1)];

            Vector3 headDirection = point - body.transform.position;

            body.transform.position += headDirection * bodySpeed * Time.deltaTime;

            body.transform.LookAt(point);

            index++;

        }

        Debug.Log(gap);
    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(BodyPrefab, transform.position, transform.rotation);
        BodyParts.Add(body);
        Debug.Log("eaten");
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Food"))
        {
            Destroy(other.gameObject);

            GrowSnake();

            food_spawner.triggerSpawn = true;
        }

        if (other.gameObject.CompareTag("Wall"))
        {
            // Rotate the snake slightly
            transform.Rotate(Vector3.up, 20);
        }

        if (other.gameObject.CompareTag("PowerUpFood"))
        {
            Destroy(other.gameObject);

            SpeedUp();

        }
    }

    private void SpeedUp()
    {
        
        float speedIncrease = 2;

        moveSpeed += speedIncrease;
        bodySpeed += speedIncrease;
        gap += (int)speedIncrease;

        foreach (var bodyPart in BodyParts)
        {
            GameObject speedUpChild = bodyPart.transform.Find("SpeedUp")?.gameObject;
            speedUpChild?.SetActive(true);
        }

        StartCoroutine(ResetSpeed(speedIncrease));

    }

    private IEnumerator ResetSpeed(float speedIncrease)
    {
        yield return new WaitForSeconds(5);

        moveSpeed -= speedIncrease;
        bodySpeed -= speedIncrease;

        // Reset the gap between body parts
        gap -= (int)speedIncrease;

        foreach (var bodyPart in BodyParts)
        {
            GameObject speedUpChild = bodyPart.transform.Find("SpeedUp")?.gameObject;
            speedUpChild?.SetActive(false);
        }
    }
}
