using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdPlayer : MonoBehaviour
{
    private GameObject sparrow;
    private GameObject crow;
    private GameObject chickadee;
    private Transform playerTransform;//Transform(ÉvÉåÉCÉÑÅ[)

    // Start is called before the first frame update
    void Start()
    {
        sparrow = GameObject.Find("Sparrow_Player");
        //crow = GameObject.Find("Crow_Player");
        //chickadee = GameObject.Find("Chickadee_Player");
        playerTransform = GameObject.Find("Player").transform;

        sparrow.SetActive(false);
        //crow.SetActive(false);
        //chickadee.SetActive(false);

        if (GameManager.playerSelect == "Sparrow")
        {
            sparrow.SetActive(true);
        }
        else if (GameManager.playerSelect == "Crow")
        {
            //crow.SetActive(true);
        }
        else if (GameManager.playerSelect == "Chickadee")
        {
            //chickadee.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerTransform.position;
    }
}
