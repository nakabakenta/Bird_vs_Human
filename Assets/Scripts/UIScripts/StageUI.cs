using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageUI : MonoBehaviour
{
    private Image forwardBullet_UI;
    private Image downBullet_UI;

    private GameObject pauseUI;
    private GameObject stageClearUI;
    private GameObject continueUI;
    private TMP_Text score;      //TMP_Text(ƒXƒRƒA)
    private TMP_Text remain;     //TMP_Text(Žc‚è)
    private TMP_Text hp;         //TMP_Text(‘Ì—Í)

    // Start is called before the first frame update
    void Start()
    {
        forwardBullet_UI = GameObject.Find("ForwardBullet_Front_UI").GetComponent<Image>();
        downBullet_UI = GameObject.Find("DownBullet_Front_UI").GetComponent<Image>();

        pauseUI = GameObject.Find("Pause_UI");
        stageClearUI = GameObject.Find("StageClear_UI");
        continueUI = GameObject.Find("Continue_UI");

        score = GameObject.Find("SCORE").GetComponent<TMP_Text>();  //
        remain = GameObject.Find("REMAIN").GetComponent<TMP_Text>();//
        hp = GameObject.Find("HP").GetComponent<TMP_Text>();

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
            remain.text = "REMAIN ~ " + GameManager.remain;//
        }
        //
        else if (GameManager.remain >= 0)
        {
            remain.text = "REMAIN ~ 0" + GameManager.remain;//
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

        if (PlayerController.attackTimer[0] == 0)
        {
            forwardBullet_UI.fillAmount = 0;
        }
        else if (PlayerController.attackTimer[0] < PlayerController.attackInterval[0])
        {
            forwardBullet_UI.fillAmount = Mathf.Clamp01(PlayerController.attackTimer[0] / PlayerController.attackInterval[0]);
        }
        else if (PlayerController.attackTimer[0] > PlayerController.attackInterval[0])
        {
            forwardBullet_UI.fillAmount = 1;
        }

        if (PlayerController.attackTimer[1] == 0)
        {
            downBullet_UI.fillAmount = 0;
        }
        else if (PlayerController.attackTimer[1] < PlayerController.attackInterval[1])
        {
            downBullet_UI.fillAmount = Mathf.Clamp01(PlayerController.attackTimer[1] / PlayerController.attackInterval[1]);
        }
        else if (PlayerController.attackTimer[1] > PlayerController.attackInterval[1])
        {
            downBullet_UI.fillAmount = 1;
        }
    }
}
