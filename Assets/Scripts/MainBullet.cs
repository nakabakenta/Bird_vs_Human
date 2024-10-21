using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBullet : MonoBehaviour
{
    public int speed;//弾の速度

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * transform.forward * Time.deltaTime;
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //下記のタグが付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Enemy"||//敵
            collision.gameObject.tag == "Delete")//削除
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
