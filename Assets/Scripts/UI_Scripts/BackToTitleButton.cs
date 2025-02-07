using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackToTitleButton : ButtonBase, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public static string selectButton = null;//選択しているボタン
    public static bool buttonClick;          //ボタンのクリック可否

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        moveButton = false;
        buttonClick = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ボタンを"クリックしていない"場合
        if (buttonClick == false)
        {
            selectButton = button.gameObject.name;//選択しているボタンの名前を入れる

            if (selectButton == "Button_BackToTitle")
            {
                GameManager.nextScene = "Title";
            }

            EnterButton();//関数"EnterButton"を実行する
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonClick == false)
        {
            ExitButton();
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

    public override void ResetButton()
    {
        selectButton = null;
    }
}
