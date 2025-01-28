using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage : UIBase
{
    //処理
    public static int nowStage;        //現在のステージ
    public GameObject baseUI;
    public GameObject pauseUI;
    public GameObject clearUI;
    public GameObject continueUI;
    public GameObject buttonNextStage;
    public GameObject buttonGameClear;
    public GameObject buttonRestart;
    public GameObject BackToMenu;
    public Slider killSlider;
    public Slider gageSlider;
    public Image gageLight;

    public Image forwardBullet_UI;
    public Image downBullet_UI;
    public TMP_Text score;        //TMP_Text(スコア)
    public TMP_Text remain;       //TMP_Text(残り)
    public TMP_Text hp;           //TMP_Text(体力)

    private PlayerController playerController;
    private StageButton stageButton;
    private bool loadScene;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.status = "Play";//ゲームの状態を"Play"にする
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        baseUI.SetActive(false);
        pauseUI.SetActive(false);
        clearUI.SetActive(false);
        continueUI.SetActive(false);

        if(buttonNextStage != null)
        {
            buttonNextStage.SetActive(false);
        }
        else if(buttonGameClear != null)
        {
            buttonGameClear.SetActive(false);
        }

        buttonRestart.SetActive(false);
        BackToMenu.SetActive(false);
        loadScene = false;

        gageSlider.minValue = 0;
        gageSlider.maxValue = 1;

        killSlider.minValue = 0;
        killSlider.maxValue = PlayerBase.Player.maxExp;
    }

    // Update is called once per frame
    void Update()
    {
        killSlider.value = PlayerBase.exp;

        if (PlayerBase.remain > 0)
        {
            if (playerController.hp > 0)
            {
                if (EnemyBase.bossEnemy == false)
                {
                    GameManager.status = "Clear";
                    baseUI.SetActive(true);
                    clearUI.SetActive(true);

                    if (buttonNextStage != null)
                    {
                        buttonNextStage.SetActive(true);
                    }
                    else if (buttonGameClear != null)
                    {
                        buttonGameClear.SetActive(true);
                    }

                    buttonRestart.SetActive(true);
                    BackToMenu.SetActive(true);
                }

                //Escキーが"押された"&&ゲームの状態が"Play"の場合
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (GameManager.status == "Play")
                    {
                        GameManager.status = "Pause";//ゲームの状態を"Pause"にする
                        baseUI.SetActive(true);
                        pauseUI.SetActive(true);
                        buttonRestart.SetActive(true);
                        BackToMenu.SetActive(true);
                        Time.timeScale = 0;
                    }
                    //Escキーが"押された"&&ゲームの状態が"Pause"の場合
                    else if (GameManager.status == "Pause")
                    {
                        GameManager.status = "Play";//ゲームの状態を"Play"にする

                        if (StageButton.selectButton != null)
                        {
                            stageButton = GameObject.Find(StageButton.selectButton).GetComponent<StageButton>();
                            //選択しているボタンを探す
                            Transform selectButtonAlpha = GameObject.Find(StageButton.selectButton).transform.Find("Alpha_UI_Base_64_03");
                            Transform selectButtonMark = GameObject.Find(StageButton.selectButton).transform.Find("UI_Base_64_04");
                            //選択しているボタンを探してコンポーネントを取得
                            RectTransform selectButtonRectTransform = GameObject.Find(StageButton.selectButton).GetComponent<RectTransform>();
                            //選択しているボタンを初期化
                            selectButtonRectTransform.anchoredPosition = new Vector2(stageButton.buttonPosition.x, selectButtonRectTransform.anchoredPosition.y);
                            selectButtonAlpha.gameObject.SetActive(true);
                            selectButtonMark.gameObject.SetActive(false);
                            StageButton.selectButton = null;
                        }

                        baseUI.SetActive(false);
                        pauseUI.SetActive(false);
                        buttonRestart.SetActive(false);
                        BackToMenu.SetActive(false);
                        Time.timeScale = 1;
                    }

                }
            }
            else if (playerController.hp <= 0)
            {
                baseUI.SetActive(true);
                continueUI.SetActive(true);
                buttonRestart.SetActive(true);
                BackToMenu.SetActive(true);
            } 
        }
        else if (PlayerBase.remain <= 0 && loadScene == false)
        {
            loadScene = true;
            GameManager.status = "GameOver";
            GameManager.nextScene = "GameOver";
            LoadScene();
        }

        if (PlayerController.gageTimer < PlayerController.gageInterval)
        {
            gageSlider.value = Mathf.Clamp01(PlayerController.gageTimer / PlayerController.gageInterval);
            gageLight.color = new Color32(127, 127, 127, 255);
        }
        else if (PlayerController.gageTimer > PlayerController.gageInterval)
        {
            gageSlider.value = 1;
            gageLight.color = new Color32(255, 0, 0, 255);
        }

        if (PlayerController.status == "Invincible")
        {
            gageSlider.value = 0;
        }

        if (killSlider.value >= killSlider.maxValue)
        {
            killSlider.value = 0;
        }

        if (playerController.hp >= 10)
        {
            hp.text = "" + playerController.hp;//
        }
        //
        else if (playerController.hp >= 0)
        {
            hp.text = "0" + playerController.hp;//
        }

        //
        if (GameManager.score >= 10000)
        {
            score.text = "" + GameManager.score;
        }
        //
        else if (GameManager.score >= 1000)
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

        if (PlayerController.attackTimer[0] == 0.0f)
        {
            forwardBullet_UI.fillAmount = 0;
        }
        else if (PlayerController.attackTimer[0] < PlayerController.attackInterval)
        {
            forwardBullet_UI.fillAmount = Mathf.Clamp01(PlayerController.attackTimer[0] / PlayerController.attackInterval);
        }
        else if (PlayerController.attackTimer[0] > PlayerController.attackInterval)
        {
            forwardBullet_UI.fillAmount = 1;
        }

        if (PlayerController.attackTimer[1] == 0.0f)
        {
            downBullet_UI.fillAmount = 0;
        }
        else if (PlayerController.attackTimer[1] < PlayerController.attackInterval)
        {
            downBullet_UI.fillAmount = Mathf.Clamp01(PlayerController.attackTimer[1] / PlayerController.attackInterval);
        }
        else if (PlayerController.attackTimer[1] > PlayerController.attackInterval)
        {
            downBullet_UI.fillAmount = 1;
        }
    }
}
