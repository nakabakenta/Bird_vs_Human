using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateBase : MonoBehaviour
{
    public int generateCount;
    public Vector2[] generatePosition = new Vector2[2];
    public GameObject[] generate;
    private int generateNumber = 0;
    private Transform thisTransform;                   //"Transform"
    private Vector3 position;

    public void Generate()
    {
        thisTransform = this.gameObject.GetComponent<Transform>();

        for (int i = 0; i < generateCount; i++)
        {
            if (generate.Length > 1)
            {
                generateNumber = (int)Random.Range(0, generate.Length);
            }
            
            position.x = (float)Random.Range(generatePosition[0].x, generatePosition[1].x);
            position.y = (float)Random.Range(generatePosition[0].y, generatePosition[1].y);
            Instantiate(generate[generateNumber], new Vector3(position.x, position.y, this.transform.position.z), this.transform.rotation, thisTransform);
        }
    }
}
