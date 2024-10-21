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

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collider)
    {
        //
        if (collider.gameObject.tag == "Delete")
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
