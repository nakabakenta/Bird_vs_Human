using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasUIScript : MonoBehaviour
{
    //クラス
    public FlashUIClass flashUIClass;//点滅UIクラス
    public TitleUIClass titleUIClass;//タイトルUIクラス
    //このオブジェクトのコンポーネント
    private Image[] image;                //"Image"
    private RectTransform[] rectTransform;//"RectTransform"

    // Start is called before the first frame update
    void Start()
    {
        //このオブジェクトのコンポーネントを取得
        image = this.gameObject.GetComponentsInChildren<Image>();
        rectTransform = this.gameObject.GetComponentsInChildren<RectTransform>();

        //点滅UI使用の可否が"true"の場合
        if (flashUIClass.use == true)
        {
            //コルーチン"FlashUI"を実行する
            StartCoroutine("FlashUI");
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

    //コルーチン"FlashUI"
    IEnumerator FlashUI()
    {
        while (true)
        {
            for (int down = 0; down < 25; down++)
            {
                for (int count = 0; count < image.Length; count++)
                {
                    image[count].color = image[count].color - new Color32(0, 0, 0, 10);
                }

                yield return new WaitForSeconds(flashUIClass.interval);
            }

            for (int up = 0; up < 25; up++)
            {
                for (int count = 0; count < image.Length; count++)
                {
                    image[count].color = image[count].color + new Color32(0, 0, 0, 10);
                }

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
            for (int count = 0; count < image.Length; count++)
            {
                //
                if (rectTransform[count].anchoredPosition.x >= titleUIClass.limitPosition.x)
                {
                    rectTransform[count].anchoredPosition = new Vector2(-titleUIClass.limitPosition.x, rectTransform[count].anchoredPosition.y);
                }
                else
                {
                    rectTransform[count].anchoredPosition = new Vector2(rectTransform[count].anchoredPosition.x + titleUIClass.speed * Time.deltaTime, rectTransform[count].anchoredPosition.y);
                }
            }
        }
        //タイトルUI移動方向(右方向[1])の可否が"true"の場合
        else if (titleUIClass.direction[1] == true)
        {
            for (int count = 0; count < image.Length; count++)
            {
                //
                if (rectTransform[count].anchoredPosition.x <= -titleUIClass.limitPosition.x)
                {
                    rectTransform[count].anchoredPosition = new Vector2(titleUIClass.limitPosition.x, rectTransform[count].anchoredPosition.y);
                }
                else
                {
                    rectTransform[count].anchoredPosition = new Vector2(rectTransform[count].anchoredPosition.x - titleUIClass.speed * Time.deltaTime, rectTransform[count].anchoredPosition.y);
                }
            }
        }
    }

    //点滅UIクラス
    [System.Serializable]
    public class FlashUIClass
    {
        //処理
        public bool use;      //使用の可否
        public float interval;//間隔
    }
    
    //タイトルUIクラス
    [System.Serializable]
    public class TitleUIClass
    {
        //処理
        public bool use;                      //使用の可否
        public bool[] direction = new bool[2];//移動方向(左方向[0],右方向[1]の可否
        public float speed;                   //移動速度
        public Vector2 limitPosition;         //移動限界位置
    }
}
