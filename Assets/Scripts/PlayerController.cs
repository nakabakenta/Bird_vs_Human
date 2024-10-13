using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //自機ステータス
    public int hp;   //自機の体力
    public int power;//自機の攻撃力
    public int speed;//自機の攻撃速度
    public int cost; //自機の追加武器入手時の初期残数

    private int moveSpeed;        //自機の初期移動速度
    private int acceleration = 10;//自機の加減速値

    public GameObject mainBullet;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //上移動
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.position += moveSpeed * transform.up * Time.deltaTime;
        }
        //右移動
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position -= moveSpeed * transform.forward * Time.deltaTime;
        }
        //下移動
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= moveSpeed * transform.up * Time.deltaTime;
        }
        //左移動
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += moveSpeed * transform.forward * Time.deltaTime;
        }
        //加速
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(moveSpeed < 60)
            {
                moveSpeed += acceleration;
            }
        }
        //減速
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (moveSpeed > 20)
            {
                moveSpeed -= acceleration;
            }
        }
        //攻撃発射
        if(Input.GetMouseButton(0))
        {
            Instantiate(mainBullet, this.transform.position, Quaternion.identity);
        }
    }

    void Shot()
    {
        
    }
}
