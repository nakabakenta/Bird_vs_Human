using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    //処理
    public static string selectButton;  //選択しているボタン
    public static bool buttonClick;     //ボタンのクリック可否
    private Vector2 buttonPosition;     //ボタンの位置
    private bool setActive;             //オブジェクト表示の可否
    //このオブジェクトのコンポーネント
    public GameObject alpha;            //"GameObject(半透明)"
    public GameObject selectMark;       //"GameObject(選択マーク)"
    public AudioClip enter;             //"AudioClip(入場)"
    public AudioClip click;             //"AudioClip(クリック)"
    private Button button;              //"Button" 
    private RectTransform rectTransform;//"RectTransform"
    private AudioSource audioSource;    //"AudioSource"
    private SceneLoader sceneLoader;    //"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        //処理を初期化
        buttonPosition = rectTransform.anchoredPosition;
        buttonClick = false;
        //このオブジェクトのコンポーネントを取得する
        button = this.GetComponent<Button>();
        rectTransform = this.GetComponent<RectTransform>();
        audioSource = this.GetComponent<AudioSource>();
        sceneLoader = this.GetComponent<SceneLoader>();
        //このオブジェクトのコンポーネントを初期化
        selectMark.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ボタンを"クリックしていない"場合
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

            //スズメ
            if (selectButton == "Button_Sparrow")
            {
                GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"の"playerNumber"を"スズメ(0)"にする
            }
            //カラス
            else if (selectButton == "Button_Crow")
            {
                GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"の"playerNumber"を"カラス(1)"にする
            }
            //コガラ
            else if (selectButton == "Button_Chickadee")
            {
                GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"の"playerNumber"を"コガラ(2)"にする
            }
            //ペンギン
            else if (selectButton == "Button_Penguin")
            {
                GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"の"playerNumber"を"ペンギン(3)"にする
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
            buttonClick = true;            //ボタンを"クリックした"にする
            audioSource.PlayOneShot(click);//"クリック"を鳴らす
            Invoke("SceneLoad", 2.0f);     //関数"SceneLoad"を"2.0f"後に実行
        }

        InvokeRepeating("Flash", 0.0f, 0.25f);//関数"Flash"を"0.0f"後に実行、"0.25f"毎に繰り返す
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
        sceneLoader.StageSelect();//"SceneLoader"の関数"StageSelect"を実行
    }
}