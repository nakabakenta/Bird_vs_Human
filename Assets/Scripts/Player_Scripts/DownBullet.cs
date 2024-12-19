using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBullet : MonoBehaviour
{
    //処理
    public float speed;      //弾の移動速度
    private float viewPointY;//ビューポイント座標.Y

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= speed * transform.up * Time.deltaTime;//

        //ビューポイント座標を取得
        viewPointY = Camera.main.WorldToViewportPoint(this.transform.position).y;//画面Y座標

        //-Y座標
        if (viewPointY < 0)
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Enemy"だったら
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy")
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
