using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float moveSpeed = 5;
    public float steerSpeed = 100;
    public GameObject BodyPrefab;
    private int gap = 30;
    private float bodySpeed = 3;
    private List<GameObject> BodyParts = new List<GameObject>();
    private List<Vector3> HeadPositionHistory = new List<Vector3>();
    // Start is called before the first frame update
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
    }

    private void GrowSnake()
    {
        GameObject body = Instantiate(BodyPrefab);
        BodyParts.Add(body);
    }
}
