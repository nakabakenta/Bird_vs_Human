using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighterJetBullet : MonoBehaviour
{
    //ステータス
    private float speed;//弾の移動速度
    //処理
    private float viewPointX; //ビューポイント座標.X

    // Start is called before the first frame update
    void Start()
    {
        //ステータスを設定
        speed = EnemyList.FighterJetEnemy.bulletSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する

        //ビューポイント座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面X座標

        //
        if (viewPointX < 0)
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
