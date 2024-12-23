using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponent : MonoBehaviour
{
    //処理
    public FlashUIClass flashUIClass;//点滅UIクラス
    public TitleUIClass titleUIClass;//タイトルUIクラス
    //このオブジェクトのコンポーネント
    public Image image;                //"Image"
    public RectTransform rectTransform;//"RectTransform"

    // Start is called before the first frame update
    void Start()
    {
        //このオブジェクトのコンポーネントを取得

        //点滅UI使用の可否が"true"の場合
        if (flashUIClass.use == true)
        {
            StartCoroutine("Flash");
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        //タイトルUI使用の可否が"true"の場合
        if (titleUIClass.use == true)
        {
            TitleUI();//関数"TitleUI"を実行
        }
    }

    IEnumerator Flash()
    {
        while (true)
        {
            for (int i = 0; i < 25; i++)
            {
                image.color = image.color - new Color32(0, 0, 0, 10);
                yield return new WaitForSeconds(flashUIClass.interval);
            }

            for (int k = 0; k < 25; k++)
            {
                image.color = image.color + new Color32(0, 0, 0, 10);
                yield return new WaitForSeconds(flashUIClass.interval);
            }
        }
    }

    //関数"TitleUI"
    void TitleUI()
    {
        //タイトルUI移動方向(左方向[0])の可否が"true"の場合
        if (titleUIClass.direction[0] == true)
        {
            //
            if (rectTransform.anchoredPosition.x >= titleUIClass.limitPosition.x)
            {
                rectTransform.anchoredPosition = new Vector2(-titleUIClass.limitPosition.x, rectTransform.anchoredPosition.y);
            }
            else
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + titleUIClass.speed * Time.deltaTime, rectTransform.anchoredPosition.y);
            }
        }
        //タイトルUI移動方向(右方向[1])の可否が"true"の場合
        else if (titleUIClass.direction[1] == true)
        {
            //
            if (rectTransform.anchoredPosition.x <= -titleUIClass.limitPosition.x)
            {
                rectTransform.anchoredPosition = new Vector2(titleUIClass.limitPosition.x, rectTransform.anchoredPosition.y);
            }
            else
            {
                rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x - titleUIClass.speed * Time.deltaTime, rectTransform.anchoredPosition.y);
            }
        }
    }

    //点滅UIクラス
    [System.Serializable]
    public class FlashUIClass
    {
        //処理
        public bool use;              //使用の可否
        public float interval = 0.05f;//間隔
    }
    
    //タイトルUIクラス
    [System.Serializable]
    public class TitleUIClass
    {
        //処理
        public bool use;                                          //使用の可否
        public bool[] direction = new bool[2];                    //移動方向(左方向[0],右方向[1]の可否
        public float speed = 100.0f;                              //移動速度
        public Vector2 limitPosition = new Vector2(1176.0f, 0.0f);//移動の限界値
    }
}
