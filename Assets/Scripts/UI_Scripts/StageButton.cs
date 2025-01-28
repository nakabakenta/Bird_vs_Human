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

            if(selectButton == "Button_Restart")
            {
                GameManager.nextScene = GameManager.nowScene;
            }
            else if (selectButton == "Button_BackToMenu")
            {
                GameManager.nextScene = "PlayerSelect";
            }
            else if (selectButton == "Button_NextStage")
            {
                if(GameManager.nowScene == "Stage1")
                {
                    GameManager.nextScene = "Stage2";
                }
                else if (GameManager.nowScene == "Stage2")
                {
                    GameManager.nextScene = "Stage3";
                }
                else if (GameManager.nowScene == "Stage3")
                {
                    GameManager.nextScene = "Stage4";
                }
                else if (GameManager.nowScene == "Stage4")
                {
                    GameManager.nextScene = "Stage5";
                }
            }
            else if(selectButton == "Button_GameClear")
            {
                GameManager.nextScene = "GameClear";
            }

            EnterButton();//関数"EnterButton"を実行する
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //ボタンを"クリックしていない"場合
        if (buttonClick == false)
        {
            buttonClick = true;//ボタンを"クリックした"にする
            ClickButton();     //関数"ClickButton"を実行する
        }
    }
}
