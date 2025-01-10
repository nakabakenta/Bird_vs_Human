using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //処理
    public static bool buttonSelect;//ボタンを押した可否
    //このオブジェクトのコンポーネント
    public AudioClip cursor;        //"AudioClip(カーソル)"
    public AudioClip select;        //"AudioClip(選択)"
    private Button button;          //"Button" 
    private AudioSource audioSource;//"AudioSource"
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        //処理を初期化
        buttonSelect = false;                          //ボタンを"押していない"にする
        //このオブジェクトのコンポーネントを取得
        button = this.GetComponent<Button>();
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        sceneLoader = this.GetComponent<SceneLoader>();//"Script(SceneLoader)"
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //ボタンを"押していない"場合
        if (buttonSelect == false)
        {
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
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //ボタンを"押していない"場合
        if (buttonSelect == false)
        {
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

            buttonSelect = true;            //ボタンを"押した"にする
            audioSource.PlayOneShot(select);//"select(選択)"を鳴らす
            Invoke("SceneLoad", 1.515f);    //関数"SceneLoad"を"1.515f"後に実行する
        }
    }

    //関数"SceneLoad"
    void SceneLoad()
    {
        sceneLoader.StageSelect();//"SceneLoader"の関数"StageSelect"を実行する
    }
}