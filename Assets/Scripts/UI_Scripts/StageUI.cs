using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageUI : MonoBehaviour
{
    //コンポーネント(private)
    private GameObject pauseUI;
    private GameObject stageClearUI;
    private GameObject continueUI;
    private Image forwardBullet_UI;
    private Image downBullet_UI;
    private TMP_Text score;         //TMP_Text(スコア)
    private TMP_Text remain;        //TMP_Text(残り)
    private TMP_Text hp;            //TMP_Text(体力)
    //コンポーネント(public)
    public Image gageLight;
    //コンポーネント(private)
    public Slider gage;//Slider(ゲージ)

    // Start is called before the first frame update
    void Start()
    {
        forwardBullet_UI = GameObject.Find("ForwardBullet_Front_UI").GetComponent<Image>();
        downBullet_UI = GameObject.Find("DownBullet_Front_UI").GetComponent<Image>();

        pauseUI = GameObject.Find("Pause_UI");
        stageClearUI = GameObject.Find("StageClear_UI");
        continueUI = GameObject.Find("Continue_UI");

        remain = GameObject.Find("Remain_Number").GetComponent<TMP_Text>();//
        hp = GameObject.Find("Hp_Number").GetComponent<TMP_Text>();
        score = GameObject.Find("Score_Number").GetComponent<TMP_Text>();  //

        pauseUI.SetActive(false);
        stageClearUI.SetActive(false);
        continueUI.SetActive(false);

        gage.value = 0;   //
        gage.minValue = 0;//
        gage.maxValue = 1;//
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (PlayerController.hp >= 10)
        {
            hp.text = "" + PlayerController.hp;//
        }
        //
        else if(PlayerController.hp >= 0)
        {
            hp.text = "0" + PlayerController.hp;//
        }

        //
        if(GameManager.score >= 10000)
        {
            score.text = "" + GameManager.score;
        }
        //
        else if(GameManager.score >= 1000)
        {
            score.text = "0" + GameManager.score;
        }
        //
        else if (GameManager.score >= 100)
        {
            score.text = "00" + GameManager.score;
        }
        //
        else if (GameManager.score >= 10)
        {
            score.text = "000" + GameManager.score;
        }
        //
        else if (GameManager.score >= 0)
        {
            score.text = "0000" + GameManager.score;
        }

        //
        if (GameManager.remain >= 10)
        {
            remain.text = "" + GameManager.remain;//
        }
        //
        else if (GameManager.remain >= 0)
        {
            remain.text = "0" + GameManager.remain;//
        }

        if(Stage.gameStatus == "Pause")
        {
            pauseUI.SetActive(true);
        }
        else if(Stage.gameStatus == "Play")
        {
            pauseUI.SetActive(false);
        }


        if (Stage.gameStatus == "Clear")
        {
            stageClearUI.SetActive(true);
        }

        if(GameManager.remain > 0 && PlayerController.hp <= 0)
        {
            continueUI.SetActive(true);
        }

        if (PlayerController.attackTimer[0] == 0.0f)
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

        if (PlayerController.attackTimer[1] == 0.0f)
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

        
        if (PlayerController.gageTimer < PlayerController.gageInterval)
        {
            gage.value = Mathf.Clamp01(PlayerController.gageTimer / PlayerController.gageInterval);
            gageLight.color = new Color32(127, 127, 127, 255);
        }
        else if (PlayerController.gageTimer > PlayerController.gageInterval)
        {
            gage.value = 1;
            gageLight.color = new Color32(255, 255, 255, 255);
        }

        if (PlayerController.playerStatus == "Invincible")
        {
            gage.value = 0;
        }
    }
}
