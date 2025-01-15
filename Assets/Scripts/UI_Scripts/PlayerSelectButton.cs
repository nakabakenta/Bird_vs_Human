using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //処理
    public static bool buttonSelect;//ボタンを押した可否
    private bool setActive;         //オブジェクト表示の可否
    //このオブジェクトのコンポーネント
    public GameObject buttonAlpha;  //"GameObject(非選択)"
    public AudioClip cursor;        //"AudioClip(カーソル)"
    public AudioClip select;        //"AudioClip(選択)"
    private Button button;          //"Button" 
    private AudioSource audioSource;//"AudioSource"
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        //このオブジェクトのコンポーネントを取得
        button = this.GetComponent<Button>();
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        sceneLoader = this.GetComponent<SceneLoader>();//"Script(SceneLoader)"
        //処理を初期化
        buttonSelect = false;                          //ボタンを"押していない"にする
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ボタンを"押していない"場合
        if (buttonSelect == false)
        {
            notSelect.SetActive(false);
            audioSource.PlayOneShot(cursor);//"select(カーソル)"を鳴らす

            //スズメ
            if (button.gameObject.name == "Button_Sparrow")
            {
                GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"の"playerNumber"を"スズメ(0)"にする
            }
            //カラス
            else if (button.gameObject.name == "Button_Crow")
            {
                GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"の"playerNumber"を"カラス(1)"にする
            }
            //コガラ
            else if (button.gameObject.name == "Button_Chickadee")
            {
                GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"の"playerNumber"を"コガラ(2)"にする
            }
            //ペンギン
            else if (button.gameObject.name == "Button_Penguin")
            {
                GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"の"playerNumber"を"ペンギン(3)"にする
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonSelect == false)
        {
            notSelect.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //ボタンを"押していない"場合
        if (buttonSelect == false)
        {
            buttonSelect = true;            //ボタンを"押した"にする
            audioSource.PlayOneShot(select);//"select(選択)"を鳴らす
            Invoke("SceneLoad", 2.0f);      //関数"SceneLoad"を"2.0f"後に実行する
        }

        InvokeRepeating("Flash", 0.0f, 0.25f);//関数"Flash"を"0.0f"後に実行して"0.25f"毎に繰り返す
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
        sceneLoader.StageSelect();//"SceneLoader"の関数"StageSelect"を実行する
    }
}