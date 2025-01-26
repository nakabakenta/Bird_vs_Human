using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour
{
    //このオブジェクトのコンポーネント
    public GameObject alpha;            //"GameObject(半透明)"
    public GameObject selectMark;       //"GameObject(選択マーク)"
    public AudioClip enter;             //"AudioClip(入場)"
    public AudioClip click;             //"AudioClip(クリック)"
    public Button button;              //"Button" 
    public RectTransform rectTransform;//"RectTransform"
    public AudioSource audioSource;    //"AudioSource"
    public SceneLoader sceneLoader;    //"Script(SceneLoader)"
    //処理
    public Vector2 buttonPosition;     //ボタンの位置
    public bool setActive;             //オブジェクト表示の可否

    public void GetComponent()
    {
        //このオブジェクトのコンポーネントを取得する
        button = this.GetComponent<Button>();
        rectTransform = this.GetComponent<RectTransform>();
        audioSource = this.GetComponent<AudioSource>();
        sceneLoader = this.GetComponent<SceneLoader>();
        //このオブジェクトのコンポーネントを初期化
        selectMark.SetActive(false);
        //処理を初期化
        buttonPosition = rectTransform.anchoredPosition;
    }
}
