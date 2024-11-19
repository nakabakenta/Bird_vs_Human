using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAllyGeneration : MonoBehaviour
{
    //仲間オブジェクト
    public GameObject[] ally = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ally[GameManager.playerNumber], this.transform.position, this.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
