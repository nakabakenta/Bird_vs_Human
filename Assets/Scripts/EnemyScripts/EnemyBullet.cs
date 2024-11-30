using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //処理
    public float speed;      //移動速度
    private float viewPointX;//ビューポイント座標.X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * transform.up * Time.deltaTime;//

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
        //衝突したオブジェクトのタグが"Enemy"だったら
        if (collision.gameObject.tag == "Player")
        {
            //Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
