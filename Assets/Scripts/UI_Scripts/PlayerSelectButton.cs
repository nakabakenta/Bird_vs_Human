using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelectButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
{
    public static string selectButton;//選択しているボタン
    public static bool buttonClick;   //ボタンのクリック可否

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        moveButton = true;
        selectButton = null;
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

            //スズメ
            if (selectButton == "Button_Sparrow")
            {
                GameManager.selectPlayer = (int)PlayerBase.Player.PlayerName.Sparrow;//
            }
            //カラス
            else if (selectButton == "Button_Crow")
            {
                GameManager.selectPlayer = (int)PlayerBase.Player.PlayerName.Crow;//
            }
            //コガラ
            else if (selectButton == "Button_Chickadee")
            {
                GameManager.selectPlayer = (int)PlayerBase.Player.PlayerName.Chickadee;//
            }
            //ペンギン
            else if (selectButton == "Button_Penguin")
            {
                GameManager.selectPlayer = (int)PlayerBase.Player.PlayerName.Penguin;//
            }

            EnterButton();//関数"EnterButton"を実行する
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //ボタンを"クリックしていない"場合
        if (buttonClick == false)
        {
            GameManager.nextScene = "StageSelect";
            buttonClick = true;                   //ボタンを"クリックした"にする
            ClickButton();                        //関数"ClickButton"を実行する
        }
    }
}