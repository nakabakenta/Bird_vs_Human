using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIComponent : MonoBehaviour
{
    //処理
    public TitleUIClass titleUIClass;   //タイトルUIクラス
    //このオブジェクトのコンポーネント
    private RectTransform rectTransform;//"RectTransform"

    // Start is called before the first frame update
    void Start()
    {
        //このオブジェクトのコンポーネントを取得
        rectTransform = this.gameObject.GetComponent<RectTransform>();//"RectTransform"
    }

    // Update is called once per frame
    void Update()
    {
        //タイトルUI使用の可否が"true"の場合
        if(titleUIClass.use == true)
        {
            TitleUI();//関数"TitleUI"を実行
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
