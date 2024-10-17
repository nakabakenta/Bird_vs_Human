using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //自機ステータス
    public int hp;   //自機の体力
    public int power;//自機の攻撃力
    public int speed;//自機の移動速度

    private float coolTime = 0.25f;//攻撃の間隔をあけるための変数
    private float spanTime = 0.25f;//攻撃が出るまでの間隔
    

    public GameObject mainBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coolTime += Time.deltaTime;//

        //上移動
        if (Input.GetKey(KeyCode.W))
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
        if (Input.GetMouseButton(0) && coolTime > spanTime)
        {
            Instantiate(mainBullet, this.transform.position, Quaternion.identity);
            coolTime = 0.0f;
        }
    }

    void Shot()
    {
        
    }
}
