using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public static int stage;
    public static bool[] BossEnemy = new bool[5];//ƒ{ƒX

    private SceneLoader sceneLoader;//

    // Start is called before the first frame update
    void Start()
    {
        if(stage == 1)
        {
            BossEnemy[0] = true;
        }
        else if(stage == 2)
        {
            BossEnemy[1] = true;
        }
        else if (stage == 3)
        {
            BossEnemy[2] = true;
        }
        else if (stage == 4)
        {
            BossEnemy[3] = true;
        }
        else if (stage == 5)
        {
            BossEnemy[4] = true;
        }

        sceneLoader = GetComponent<SceneLoader>();//Script"SceneLoader"‚ðŽæ“¾‚·‚é
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.hp <= 0)
        {
            sceneLoader.GameOver();
        }
    }
}
