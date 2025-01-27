using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
{
    public static string selectButton;//選択しているボタン
    public static bool buttonClick;   //ボタンのクリック可否

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        buttonClick = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ボタンを"クリックしていない"場合
        if (buttonClick == false)
        {
            if (selectButton != null)
            {
                //選択しているボタンを探す
                Transform findAlpha = GameObject.Find(selectButton).transform.Find("Alpha_UI_Base_64_03");
                Transform findSelectMark = GameObject.Find(selectButton).transform.Find("UI_Base_64_04");
                //選択しているボタンを探してコンポーネントを取得
                RectTransform findRectTransform = GameObject.Find(selectButton).GetComponent<RectTransform>();
                //選択しているボタンを初期化
                findRectTransform.anchoredPosition = new Vector2(buttonPosition.x, findRectTransform.anchoredPosition.y);
                findAlpha.gameObject.SetActive(true);
                findSelectMark.gameObject.SetActive(false);
            }

            selectButton = button.gameObject.name;//選択しているボタンの名前を入れる

            //
            if (selectButton == "Button_Restart")
            {
                if(Stage.nowStage == 1)
                {
                    GameManager.nowScene = "Stage1";
                }
                else if(Stage.nowStage == 2)
                {
                    GameManager.nowScene = "Stage2";
                }
                else if(Stage.nowStage == 3)
                {
                    GameManager.nowScene = "Stage3";
                }
                else if(Stage.nowStage == 4)
                {
                    GameManager.nowScene = "Stage4";
                }
                else if(Stage.nowStage == 5)
                {
                    GameManager.nowScene = "Stage5";
                } 
            }
            //
            else if (selectButton == "Button_BackToMenu")
            {
                GameManager.nowScene = "PlayerSelect";
            }
            //
            else if (selectButton == "Button_NextStage")
            {
                
            }
            //
            else if (selectButton == "Button_GameClear")
            {
                GameManager.nowScene = "GameClear";
            }

            rectTransform.anchoredPosition = new Vector2(buttonPosition.x + 200, rectTransform.anchoredPosition.y);
            alpha.SetActive(false);
            selectMark.SetActive(true);
            audioSource.PlayOneShot(enter);//"入場"を鳴らす
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //ボタンを"クリックしていない"場合
        if (buttonClick == false)
        {
            if (selectButton == "Button_NextStage")
            {
                if (Stage.nowStage == 1)
                {
                    GameManager.nowScene = "Stage2";
                    Stage.nowStage = 2;
                }
                else if (Stage.nowStage == 2)
                {
                    GameManager.nowScene = "Stage3";
                    Stage.nowStage = 3;
                }
                else if (Stage.nowStage == 3)
                {
                    GameManager.nowScene = "Stage4";
                    Stage.nowStage = 4;
                }
                else if (Stage.nowStage == 4)
                {
                    GameManager.nowScene = "Stage5";
                    Stage.nowStage = 5;
                }
            }

            buttonClick = true;                   //ボタンを"クリックした"にする
            audioSource.PlayOneShot(click);       //"クリック"を鳴らす
            LoadScene();

            InvokeRepeating("Flash", 0.0f, 0.25f);//関数"Flash"を"0.0f"後に実行、"0.25f"毎に繰り返す
            //Invoke("LoadScene", 2.0f);            //関数"LoadScene"を"2.0f"後に実行
        }
    }

    //関数"Flash"
    void Flash()
    {
        //"setActive"を"true"の場合は"false"、"false"の場合は"true"にする
        setActive = !setActive;

        //子オブジェクトを取得
        foreach (Transform child in transform)
        {
            //子オブジェクトの名前が"Alpha_UI_Base_02"の場合
            if (child.name == "Alpha_UI_Base_64_03")
            {
                //子オブジェクトを非表示にする
                child.gameObject.SetActive(false);
            }
            //それ以外の場合
            else
            {
                //子オブジェクトを"setActive"にする
                child.gameObject.SetActive(setActive);
            }
        }
    }
}
