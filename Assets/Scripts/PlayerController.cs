using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //自機ステータス
    public int hp;   //自機の体力
    public int power;//自機の攻撃力
    public int speed;//自機の移動速度

    private int coolTime;//自機の攻撃間隔

    public GameObject mainBullet;

    // Start is called before the first frame update
    void Start()
    {
        coolTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //上移動
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.position += speed * transform.up * Time.deltaTime;
        }
        //右移動
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position -= speed * transform.forward * Time.deltaTime;
        }
        //下移動
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= speed * transform.up * Time.deltaTime;
        }
        //左移動
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;
        }
        //後々使うのでおいておく
        //if (Input.GetKeyDown(KeyCode.E))
        //{

        //}
        //攻撃発射
        if (Input.GetMouseButton(0) && coolTime == 0)
        {
            Instantiate(mainBullet, this.transform.position, Quaternion.identity);
        }
        coolTime++;
    }

    void Shot()
    {
        
    }
}
