using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StageUI : MonoBehaviour
{
    private TMP_Text score;      //TMP_Text(スコア)
    private TMP_Text remain;     //TMP_Text(残り)
    private TMP_Text hp;         //TMP_Text(体力)
    private GameObject pauseUI;
    private GameObject stageClearUI;
    private GameObject continueUI;

    // Start is called before the first frame update
    void Start()
    {
        score = GameObject.Find("SCORE").GetComponent<TMP_Text>();  //
        remain = GameObject.Find("REMAIN").GetComponent<TMP_Text>();//
        hp = GameObject.Find("HP").GetComponent<TMP_Text>();

        pauseUI = GameObject.Find("Pause_UI");
        stageClearUI = GameObject.Find("StageClear_UI");
        continueUI = GameObject.Find("Continue_UI");

        pauseUI.SetActive(false);
        stageClearUI.SetActive(false);
        continueUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (PlayerController.hp >= 10)
        {
            hp.text = "HP : " + PlayerController.hp;//
        }
        //
        else if(PlayerController.hp >= 0)
        {
            hp.text = "HP : 0" + PlayerController.hp;//
        }

        //
        if(GameManager.score >= 10000)
        {
            score.text = "SCORE : " + GameManager.score;
        }
        //
        else if(GameManager.score >= 1000)
        {
            score.text = "SCORE : 0" + GameManager.score;
        }
        //
        else if (GameManager.score >= 100)
        {
            score.text = "SCORE : 00" + GameManager.score;
        }
        //
        else if (GameManager.score >= 10)
        {
            score.text = "SCORE : 000" + GameManager.score;
        }
        //
        else if (GameManager.score >= 0)
        {
            score.text = "SCORE : 0000" + GameManager.score;
        }

        //
        if (GameManager.remain >= 10)
        {
            remain.text = "REMAIN × " + GameManager.remain;//
        }
        //
        else if (GameManager.remain >= 0)
        {
            remain.text = "REMAIN × 0" + GameManager.remain;//
        }

        if(Stage.gameStatus == "Pause")
        {
            pauseUI.SetActive(true);
        }
        else if(Stage.gameStatus == "Play")
        {
            pauseUI.SetActive(false);
        }

        if (Stage.bossEnemy[Stage.nowStage - 1] == false)
        {
            stageClearUI.SetActive(true);
        }

        if(GameManager.remain > 0 && PlayerController.hp <= 0)
        {
            continueUI.SetActive(true);
        }
    }
}
