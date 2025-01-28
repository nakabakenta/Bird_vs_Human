using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : UIBase
{
    //処理
    public static int nowStage;                  //現在のステージ
    public static bool[] bossEnemy = new bool[5];//ボスの生存可否
    private GameObject pauseUI;
    private bool setPauseUI;

    // Start is called before the first frame update
    void Start()
    {
        bossEnemy[nowStage - 1] = true;//現在のステージのボスを"true(生存)"にする
        GameManager.status = "Play";   //ゲームの状態を"Play"にする

        pauseUI = GameObject.Find("Pause_UI");
        setPauseUI = false;
        pauseUI.SetActive(setPauseUI);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBase.status == "GameOver")
        {
            GameManager.nextScene = "GameOver";
            LoadScene();
        }

        if(bossEnemy[nowStage - 1] == false)
        {
            GameManager.status = "Clear";
        }

        if (GameManager.status == "Pause")
        {
            pauseUI.SetActive(true);
            Time.timeScale = 0;
        }
        else if (GameManager.status == "Play")
        {
            pauseUI.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void SetUI()
    {
        setPauseUI = !setPauseUI;
        pauseUI.SetActive(setPauseUI);
    }
}
