using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public static int stage;
    public static bool[] bossEnemy = new bool[5];//ƒ{ƒX

    public static string gameStatus;  //ƒQ[ƒ€‚Ìó‘Ô

    private SceneLoader sceneLoader;//

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        sceneLoader = GetComponent<SceneLoader>();//Script"SceneLoader"‚ğæ“¾‚·‚é

        bossEnemy = new bool[5] { false, false, false, false, false };
        bossEnemy[stage - 1] = true;

        gameStatus = "Play";
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStatus == "Play")
        {
            Time.timeScale = 1;
        }
        else if(gameStatus == "Pause")
        {
            Time.timeScale = 0;
        }

        if(PlayerController.hp <= 0 && GameManager.remain <= 0)
        {
            sceneLoader.GameOver();
        }
    }
}
