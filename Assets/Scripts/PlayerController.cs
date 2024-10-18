using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //自機ステータス
    public int hp;     //体力
    public int power;  //攻撃力
    public float speed;//移動速度

    private float coolTime = 0.2f;//クールタイム
    private float spanTime = 0.2f;//攻撃が出るまでの間隔
    //ビューポート座標変数
    private float viewX;//ビューポートX座標
    private float viewY;//ビューポートY座標
    //移動変数
    private bool forward; //前移動
    private bool backward;//後移動
    private bool up;      //上移動
    private bool down;    //下移動

    public GameObject bullet;//

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coolTime += Time.deltaTime;//クールタイムにTime.deltaTimeを足す

        //移動処理
        //前移動
        if (Input.GetKey(KeyCode.D) && forward == true)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;
        }
        //後移動
        if (Input.GetKey(KeyCode.A) && backward == true)
        {
            this.transform.position -= speed * transform.forward * Time.deltaTime;
        }
        //上移動
        if (Input.GetKey(KeyCode.W) && up == true)
        {
            this.transform.position += speed * transform.up * Time.deltaTime;
        }
        //下移動
        if (Input.GetKey(KeyCode.S) && down == true)
        {
            this.transform.position -= speed * transform.up * Time.deltaTime;
        }
        
        //移動後のビューポート座標値を取得
        viewX = Camera.main.WorldToViewportPoint(this.transform.position).x;
        viewY = Camera.main.WorldToViewportPoint(this.transform.position).y;

        //移動可能な画面範囲指定
        //-X座標
        if (viewX >= 0)
        {
            backward = true;
        }
        else
        {
            backward = false;
        }
        //+X座標
        if (viewX <= 1)
        {
            forward = true;
        }
        else
        {
            forward = false;
        }
        //-Y座標
        if (viewY >= 0)
        {
            down = true;
        }
        else
        {
            down = false;
        }
        //+Y座標
        if (viewY <= 1)
        {
            up = true;
        }
        else
        {
            up = false;
        }

        //後々使うのでおいておく
        //if (Input.GetKeyDown(KeyCode.E))
        //{

        //}

        //攻撃発射
        if (Input.GetMouseButton(0) && coolTime > spanTime)
        {
            Instantiate(bullet, this.transform.position, Quaternion.identity);
            coolTime = 0.0f;
        }
    }
}
