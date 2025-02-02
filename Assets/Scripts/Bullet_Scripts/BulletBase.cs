using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    //ステータス
    public float moveSpeed;//弾の移動速度
    //座標
    public Vector3 viewPortPosition;

     public void BaseUpdate()
    {
        //このオブジェクトのワールド座標をビューポート座標に変換して取得する
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;
        viewPortPosition.y = Camera.main.WorldToViewportPoint(this.transform.position).y;

        if (viewPortPosition.x < 0 || viewPortPosition.x > 1 || 
            viewPortPosition.y < 0)
        {
            Destroy();
        }
    }

    //関数"Destroy"
    public void Destroy()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }

    //衝突判定(OnTriggerEnter)
    virtual public void OnTriggerEnter(Collider collision)
    {
        if (this.tag == "Bullet" && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy"))
        {
            Destroy();
        }
        else if (this.tag == "EnemyBullet" && collision.gameObject.tag == "Player")
        {
            Destroy();
        }
    }
}