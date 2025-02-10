using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
{
    public static string selectButton;//選択しているボタン
    public static bool buttonClick;   //ボタンのクリック可否
    private string[] buttonName = new string[5]
        {"Button_Stage1", "Button_Stage2", "Button_Stage3", "Button_Stage4", "Button_Stage5" };

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
        //ボタンを"押していない"場合
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

            for (int i = 0; i < 5; i++)
            {
                if (button.gameObject.name == buttonName[i])
                {
                    GameManager.nextScene = stageName[i];
                }
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