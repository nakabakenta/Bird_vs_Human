using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public static int stage;
    public static bool[] BossEnemy = new bool[5] { false, false, false, false, false };//ƒ{ƒX

    private SceneLoader sceneLoader;//

    // Start is called before the first frame update
    void Start()
    {
        BossEnemy[stage - 1] = true;
        sceneLoader = GetComponent<SceneLoader>();//Script"SceneLoader"‚ðŽæ“¾‚·‚é
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.hp <= 0)
        {
            //sceneLoader.GameOver();
        }
    }
}
