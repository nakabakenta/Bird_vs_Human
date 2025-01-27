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
    private TMP_Text score; //TMP_Text(スコア)
    private TMP_Text remain;//TMP_Text(残り)
    private TMP_Text hp;    //TMP_Text(体力)
    //コンポーネント(public)
    public Image gageLight;
    //コンポーネント(private)
    public Slider gage;//Slider(ゲージ)
    public Slider level;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        forwardBullet_UI = GameObject.Find("ForwardBullet_Front_UI").GetComponent<Image>();
        downBullet_UI = GameObject.Find("DownBullet_Front_UI").GetComponent<Image>();

        pauseUI = GameObject.Find("Pause_UI");
        stageClearUI = GameObject.Find("Clear_UI");
        continueUI = GameObject.Find("Continue_UI");

        remain = GameObject.Find("Text_RemainNumber").GetComponent<TMP_Text>();//
        hp = GameObject.Find("Text_HpNumber").GetComponent<TMP_Text>();
        score = GameObject.Find("Text_ScoreNumber").GetComponent<TMP_Text>();  //

        pauseUI.SetActive(false);
        stageClearUI.SetActive(false);
        continueUI.SetActive(false);

        gage.value = 0;   //
        gage.minValue = 0;//
        gage.maxValue = 1;//

        level.value = 0;
        level.minValue = 0;
        level.maxValue = PlayerBase.maxExp;
    }

    // Update is called once per frame
    void Update()
    {
        level.value = PlayerController.exp;

        //
        if (playerController.hp >= 10)
        {
            hp.text = "" + playerController.hp;//
        }
        //
        else if(playerController.hp >= 0)
        {
            hp.text = "0" + playerController.hp;//
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
        if (PlayerController.remain >= 10)
        {
            remain.text = "" + PlayerController.remain;//
        }
        //
        else if (PlayerController.remain >= 0)
        {
            remain.text = "0" + PlayerController.remain;//
        }

        if(Stage.status == "Pause")
        {
            pauseUI.SetActive(true);
        }
        else if(Stage.status == "Play")
        {
            pauseUI.SetActive(false);
        }


        if (Stage.status == "Clear")
        {
            stageClearUI.SetActive(true);
        }

        if(playerController.hp <= 0 && PlayerController.remain > 0)
        {
            continueUI.SetActive(true);
        }

        if (PlayerController.attackTimer[0] == 0.0f)
        {
            forwardBullet_UI.fillAmount = 0;
        }
        else if (PlayerController.attackTimer[0] < PlayerController.attackTimeInterval[0])
        {
            forwardBullet_UI.fillAmount = Mathf.Clamp01(PlayerController.attackTimer[0] / PlayerController.attackTimeInterval[0]);
        }
        else if (PlayerController.attackTimer[0] > PlayerController.attackTimeInterval[0])
        {
            forwardBullet_UI.fillAmount = 1;
        }

        if (PlayerController.attackTimer[1] == 0.0f)
        {
            downBullet_UI.fillAmount = 0;
        }
        else if (PlayerController.attackTimer[1] < PlayerController.attackTimeInterval[1])
        {
            downBullet_UI.fillAmount = Mathf.Clamp01(PlayerController.attackTimer[1] / PlayerController.attackTimeInterval[1]);
        }
        else if (PlayerController.attackTimer[1] > PlayerController.attackTimeInterval[1])
        {
            downBullet_UI.fillAmount = 1;
        }

        
        if (PlayerController.gageTimer < PlayerController.gageTimeInterval)
        {
            gage.value = Mathf.Clamp01(PlayerController.gageTimer / PlayerController.gageTimeInterval);
            gageLight.color = new Color32(127, 127, 127, 255);
        }
        else if (PlayerController.gageTimer > PlayerController.gageTimeInterval)
        {
            gage.value = 1;
            gageLight.color = new Color32(255, 0, 0, 255);
        }

        if (PlayerController.status == "Invincible")
        {
            gage.value = 0;
        }

        if(level.value >= level.maxValue)
        {
            level.value = 0;
        }
    }
}
