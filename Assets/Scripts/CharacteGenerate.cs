using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteGenerate : MonoBehaviour
{
    public int maxPlayerAlly;
    public GameObject playerAlly;

    private Transform thisTransform;//"Transform"
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.gameObject.GetComponent<Transform>();

        for (int i = 0; i < maxPlayerAlly; i++)
        {
            position.x = (float)Random.Range(10.0f, 100.0f);
            position.y = (float)Random.Range(3.0f, 5.0f);

            Instantiate(playerAlly, new Vector3(position.x, position.y, this.transform.position.z), this.transform.rotation, thisTransform);
        }
    }
}
