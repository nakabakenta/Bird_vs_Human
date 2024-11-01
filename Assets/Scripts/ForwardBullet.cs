using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardBullet : MonoBehaviour
{
    public int speed;        //弾移動速度
    private float viewPointX;//ビューポイント座標.X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * transform.right * Time.deltaTime;//

        //移動後のビューポート座標値を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面X座標

        //
        if (viewPointX > 1)
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //下記のタグが付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Enemy")//敵
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
