using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
{
    public static string selectButton; //選択しているボタン
    public static bool buttonClick;    //ボタンのクリック可否

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
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
                Transform findAlpha = GameObject.Find(selectButton).transform.Find("Alpha_UI_Base_02");
                Transform findSelectMark = GameObject.Find(selectButton).transform.Find("UI_Base_03");
                //選択しているボタンを探してコンポーネントを取得
                RectTransform findRectTransform = GameObject.Find(selectButton).GetComponent<RectTransform>();
                //選択しているボタンを初期化
                findRectTransform.anchoredPosition = new Vector2(buttonPosition.x, findRectTransform.anchoredPosition.y);
                findAlpha.gameObject.SetActive(true);
                findSelectMark.gameObject.SetActive(false);
            }

            selectButton = button.gameObject.name;//選択しているボタンの名前を入れる

            //ステージ1
            if (button.gameObject.name == "Button_Stage1")
            {
                Stage.nowStage = 1;//"Stage"の"nowStage"を"1"にする
            }
            //ステージ2
            else if (button.gameObject.name == "Button_Stage2")
            {
                Stage.nowStage = 2;//"Stage"の"nowStage"を"2"にする
            }
            //ステージ3
            else if (button.gameObject.name == "Button_Stage3")
            {
                Stage.nowStage = 3;//"Stage"の"nowStage"を"3"にする
            }
            //ステージ4
            else if (button.gameObject.name == "Button_Stage4")
            {
                Stage.nowStage = 4;//"Stage"の"nowStage"を"4"にする
            }
            //ステージ5
            else if (button.gameObject.name == "Button_Stage5")
            {
                Stage.nowStage = 5;//"Stage"の"nowStage"を"5"にする
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
            buttonClick = true;                   //ボタンを"クリックした"にする
            audioSource.PlayOneShot(click);       //"クリック"を鳴らす
            InvokeRepeating("Flash", 0.0f, 0.25f);//関数"Flash"を"0.0f"後に実行、"0.25f"毎に繰り返す
            Invoke("SceneLoad", 2.0f);            //関数"SceneLoad"を"2.0f"後に実行する
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
            //子オブジェクトの名前が"Alpha"の場合
            if (child.name == "Alpha_UI_Base_02")
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

    //関数"SceneLoad"
    void SceneLoad()
    {
        sceneLoader.StageScene();//"SceneLoader"の関数"StageScene"を実行する
    }
}