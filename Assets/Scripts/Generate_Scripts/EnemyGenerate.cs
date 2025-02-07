using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerate : GenerateBase
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent();

        if(infiniteGenerate == false)
        {
            Generate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (infiniteGenerate == true && EnemyBase.bossEnemy == true)
        {
            generateTimer += Time.deltaTime;

            if(generateTimer >= generateInterval)
            {
                if (Stage.nowStage == 5 && Stage.killCount < 8)
                {
                    generateTimer = 0.0f;
                    Generate();
                }
            }
        }
    }
}
