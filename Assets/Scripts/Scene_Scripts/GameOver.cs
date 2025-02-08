using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : AddBase
{
    // Start is called before the first frame update
    void Start()
    {
        ResultScore();

        GameManager.nowScene = "GameOver";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
