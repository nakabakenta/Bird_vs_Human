using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Stage : AddBase
{
    public float uISetActiveInterval;

    //処理
    public static string gameStatus;
    public static int nowStage;                      //現在のステージ

    public GameObject[] uIMenu = new GameObject[5];
    public GameObject[] uIButton = new GameObject[3];

    private bool[] uIMenuSetActive = new bool[5]
        {false, false, false, false, false};

    private bool[] uIButtonSetActive = new bool[3]
        {false, false, false};

    private bool menu = false;
    private bool pause = false;

    public Slider killSlider;
    public Slider gageSlider;
    public Image gageLight;

    public Image forwardBullet_UI;
    public Image downBullet_UI;

    private PlayerController playerController;
    private StageButton stageButton;
    public static int killCount;

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();

        gameStatus = Status.Play.ToString();//ゲームの状態を"Play"にする
        PlayBGM(bgm[0]);

        killCount = 0;
        fade[0] = false;

        if(nowStage == 5)
        {
            fade[1] = true;
            gameObjectFade[1].SetActive(false);
        }

        {
            for (int i = 0; i < uIMenu.Length; i++)
            {
                if(uIMenu[i] != null)
                {
                    uIMenu[i].SetActive(uIMenuSetActive[i]);
                }
            }
        }

        {
            for (int i = 0; i < uIButton.Length; i++)
            {
                if (uIButton[i] != null)
                {
                    uIButton[i].SetActive(uIMenuSetActive[i]);
                }
            }
        }

        loadScene = false;

        gageSlider.minValue = 0;
        gageSlider.maxValue = 1;

        killSlider.minValue = 0;
        killSlider.maxValue = PlayerBase.Player.maxExp;
    }

    // Update is called once per frame
    void Update()
    {
        if (imageFade[0].color.a > 0)
        {
            Fade(0);
        }
        else if(imageFade[0].color.a <= 0)
        {
            gameObjectFade[0].SetActive(false);

            if (playerController.hp > 0)
            {
                //Escキーが"押された"&&ゲームの状態が"Play"の場合
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (gameStatus == Status.Play.ToString())
                    {
                        gameStatus = Status.Pause.ToString();//ゲームの状態を"Pause"にする
                        pause = true;
                    }
                    //Escキーが"押された"&&ゲームの状態が"Pause"の場合
                    else if (gameStatus == "Pause")
                    {
                        pause = false;

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
                    }
                    menu = true;
                }
            }
        }

        if (playerController.hp > 0)
        {
            killSlider.value = PlayerBase.exp;
        }

        if (gameStatus == "Play")
        {
            if (EnemyBase.bossEnemy == false)
            {
                gameStatus = "GameClear";

                if(nowStage == 5)
                {
                    loadScene = true;
                    gameObjectFade[1].SetActive(true);
                }
                else
                {
                    menu = true;
                }  
            }
            else if (playerController.hp <= 0)
            {
                gameStatus = "GameOver";
                menu = true;
            }
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
        if (PlayerController.remain >= 10)
        {
            remain.text = "" + PlayerController.remain;//
            resultRemain.text = "" + PlayerController.remain;//
        }
        //
        else if (PlayerController.remain >= 0)
        {
            remain.text = "0" + PlayerController.remain;//
            resultRemain.text = "0" + PlayerController.remain;//
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

        //
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

        if(menu == true)
        {
            menu = false;

            ResultScore();

            if (gameStatus == "Pause")
            {
                UISetActive();
            }
            else
            {
                Invoke("UISetActive", uISetActiveInterval);
            }
        }

        if (loadScene == true)
        {
            if (imageFade[1].color.a < 1)
            {
                Fade(1);
            }
            else if (imageFade[1].color.a >= 1)
            {
                gameStatus = "GameClear";
                GameManager.nextScene = "GameClear";
                LoadScene();
            }
        }
    }

    void UISetActive()
    {
        if (PlayerController.remain > 0)
        {
            uIMenu[0].SetActive(uIMenuSetActive[0] = !uIMenuSetActive[0]);
            uIMenu[4].SetActive(uIMenuSetActive[4] = !uIMenuSetActive[4]);
            uIButton[1].SetActive(uIButtonSetActive[1] = !uIButtonSetActive[1]);
            uIButton[2].SetActive(uIButtonSetActive[2] = !uIButtonSetActive[2]);
        }

        if (gameStatus == "Pause")
        {
            Pause();
        }

        if (gameStatus == "GameClear")
        {
            GameClear();
        }

        if (gameStatus == "GameOver")
        {
            GameOver();
        }
    }

    void Pause()
    {
        uIMenu[1].SetActive(true);

        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
        }

        if(pause == false)
        {
            gameStatus = "Play";//ゲームの状態を"Pause"にする
        }
    }

    void GameClear()
    {
        PlayBGM(bgm[1]);
        audioSource.loop = false;

        uIMenu[2].SetActive(true);
        uIButton[0].SetActive(true);
    }

    void GameOver()
    {
        if (PlayerBase.remain <= 0 && loadScene == false)
        {
            loadScene = true;
            GameManager.nextScene = "GameOver";
            LoadScene();
        }
        else if(playerController.hp <= 0)
        {
            PlayBGM(bgm[2]);
            uIMenu[3].SetActive(true);
        }
    }

    public enum Status
    {
        Play,
        Pause,
        GameClear,
        GameOver,
    }
}