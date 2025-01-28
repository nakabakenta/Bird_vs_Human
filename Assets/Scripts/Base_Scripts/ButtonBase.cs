using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBase : UIBase
{
    //このオブジェクトのコンポーネント
    public GameObject alpha;           //"GameObject(半透明)"
    public GameObject selectMark;      //"GameObject(選択マーク)"
    public AudioClip enter;            //"AudioClip(入場)"
    public AudioClip click;            //"AudioClip(クリック)"
    public Button button;              //"Button" 
    public RectTransform rectTransform;//"RectTransform"
    public AudioSource audioSource;    //"AudioSource"
    //処理
    public Vector2 buttonPosition;     //ボタンの位置
    public bool setActive;             //オブジェクト表示の可否

    public void GetComponent()
    {
        //このオブジェクトのコンポーネントを取得する
        button = this.GetComponent<Button>();
        rectTransform = this.GetComponent<RectTransform>();
        audioSource = this.GetComponent<AudioSource>();
        //このオブジェクトのコンポーネントを初期化
        selectMark.SetActive(false);
        //処理を初期化
        buttonPosition = rectTransform.anchoredPosition;
    }

    public void ResetButton()
    {

    }

    public void EnterButton()
    {
        rectTransform.anchoredPosition = new Vector2(buttonPosition.x + 150, rectTransform.anchoredPosition.y);
        alpha.SetActive(false);
        selectMark.SetActive(true);
        audioSource.PlayOneShot(enter);//"入場"を鳴らす
    }

    public void ClickButton()
    {
        audioSource.PlayOneShot(click);       //"クリック"を鳴らす

        if(Stage.status == "Pause")
        {
            Stage.status = null;
            LoadScene();
        }

        InvokeRepeating("Flash", 0.0f, 0.25f);//関数"Flash"を"0.0f"後に実行、"0.25f"毎に繰り返す
        Invoke("LoadScene", 2.0f);            //関数"LoadScene"を"2.0f"後に実行
    }

    //関数"Flash"
    public void Flash()
    {
        //"setActive"を"true"の場合は"false"、"false"の場合は"true"にする
        setActive = !setActive;

        //子オブジェクトを取得
        foreach (Transform child in transform)
        {
            //子オブジェクトの名前が"Alpha"の場合
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
