using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //処理
    public static bool buttonSelect;//ボタンを押した可否
    private bool setActive;         //オブジェクト表示の可否
    //このオブジェクトのコンポーネント
    public GameObject buttonAlpha;      //"GameObject(非選択)"
    public AudioClip cursor;            //"AudioClip(カーソル)"
    public AudioClip select;            //"AudioClip(選択)"
    private Button button;              //"Button" 
    private RectTransform rectTransform;//"RectTransform"
    private AudioSource audioSource;    //"AudioSource"
    private SceneLoader sceneLoader;    //"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        //このオブジェクトのコンポーネントを取得
        button = this.GetComponent<Button>();
        rectTransform = this.GetComponent<RectTransform>();
        audioSource = this.GetComponent<AudioSource>();
        sceneLoader = this.GetComponent<SceneLoader>();
        //処理を初期化
        buttonSelect = false;

        rectTransform.anchoredPosition = new Vector2(-400, rectTransform.anchoredPosition.y);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - titleUIClass.speed * Time.deltaTime, rectTransform.anchoredPosition.y);
        rectTransform.anchoredPosition = new Vector2(-400, rectTransform.anchoredPosition.y);

        //ボタンを"押していない"場合
        if (buttonSelect == false)
        {
            buttonAlpha.SetActive(false);
            audioSource.PlayOneShot(cursor);//"select(カーソル)"を鳴らす

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
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonSelect == false)
        {
            buttonAlpha.SetActive(true);
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
        sceneLoader.StageScene();//"SceneLoader"の関数"StageScene"を実行する
    }
}
