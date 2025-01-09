using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button[] button = new Button[3];
    public AudioClip cursor;               //"AudioClip(カーソル)"
    private AudioSource audioSource;       //"AudioSource"
    private SceneLoader sceneLoader;       //"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        sceneLoader = this.GetComponent<SceneLoader>();//この"Script(SceneLoader)"を取得する

        button[0].onClick.AddListener(Sparrow);
        button[1].onClick.AddListener(Crow);
        button[2].onClick.AddListener(Chickadee);

        GameManager.gameStart = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerPress == button[0])
        {
            audioSource.PlayOneShot(cursor);
            Debug.Log("aaa");
        }

        if (eventData.pointerPress == button[1])
        {
            audioSource.PlayOneShot(cursor);
            Debug.Log("bbb");
        }

        if (eventData.pointerPress == button[2])
        {
            audioSource.PlayOneShot(cursor);
            Debug.Log("ccc");
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //Debug.Log("カーソルがボタンから離れました！");
    }

    //プレイヤーセレクト一覧
    //スズメ
    public void Sparrow()
    {
        GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"の"playerNumber"を"スズメ(0)"にする
        sceneLoader.StageSelect();                           //"Script(SceneLoader)"の"関数(StageSelect)"を実行する

         Debug.Log("1");
    }
    //カラス
    public void Crow()
    {
        GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"の"playerNumber"を"カラス(1)"にする
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"の"関数(StageSelect)"を実行する

        Debug.Log("2");
    }
    //コガラ
    public void Chickadee()
    {
        GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"の"playerNumber"を"コガラ(2)"にする
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"の"関数(StageSelect)"を実行する
        Debug.Log("3");
    }
    //ペンギン
    public void Penguin()
    {
        GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"の"playerNumber"を"ペンギン(3)"にする
        sceneLoader.StageSelect();                             //"Script(SceneLoader)"の"関数(StageSelect)"を実行する
    }
}
