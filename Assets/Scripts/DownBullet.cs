using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBullet : MonoBehaviour
{
    public int speed;//弾の速度
    //ビューポート座標変数
    private float viewY;//ビューポートY座標

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= speed * transform.up * Time.deltaTime;//

        //移動後のビューポート座標値を取得
        viewY = Camera.main.WorldToViewportPoint(this.transform.position).y;//画面Y座標

        //-Y座標
        if (viewY < 0)
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //下記のタグが付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Enemy" )//敵
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
