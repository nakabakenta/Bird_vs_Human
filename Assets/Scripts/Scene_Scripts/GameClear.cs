using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClear : AddBase
{
    // Start is called before the first frame update
    void Start()
    {
        ResultScore();

        GameManager.nowScene = "GameClear";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
