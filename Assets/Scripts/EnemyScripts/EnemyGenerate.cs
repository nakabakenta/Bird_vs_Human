using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : MonoBehaviour
{
    public GameObject[] enemyObject;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyObject[(int)Random.Range(0, 16)], this.transform.position, this.transform.rotation);
    }
}
