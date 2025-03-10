using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
{
    public static string selectButton;//選択しているボタン
    public static bool buttonClick;   //ボタンのクリック可否

    public Vector2 ButtonPosition
    {
        get { return buttonPosition; }
    }

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
                Transform selectButtonAlpha = GameObject.Find(selectButton).transform.Find("Alpha_UI_Base_64_03");
                Transform selectButtonMark = GameObject.Find(selectButton).transform.Find("UI_Base_64_04");
                //選択しているボタンを探してコンポーネントを取得
                RectTransform selectButtonRectTransform = GameObject.Find(selectButton).GetComponent<RectTransform>();
                //選択しているボタンを初期化
                selectButtonRectTransform.anchoredPosition = new Vector2(buttonPosition.x, selectButtonRectTransform.anchoredPosition.y);
                selectButtonAlpha.gameObject.SetActive(true);
                selectButtonMark.gameObject.SetActive(false);
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
                for (int i = 0; i < 4; i++)
                {
                    if (GameManager.nowScene == stageName[i])
                    {
                        GameManager.nextScene = stageName[i + 1];
                    }
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
