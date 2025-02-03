using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EnemyBase.bossEnemy = true;
        GameManager.selectPlayer = 0;
        Stage.nowStage = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
