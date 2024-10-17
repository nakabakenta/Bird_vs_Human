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

    private Vector3 nowPosition; //現在の位置
    private Vector3 nextPosition;//移動後の位置
    private float viewX;//ビューポートX座標
    private float viewY;//ビューポートY座標

    public GameObject bullet;//

    // Start is called before the first frame update
    void Start()
    {
        nowPosition = new Vector3(0, 0, 0);
        nextPosition = nowPosition;
    }

    // Update is called once per frame
    void Update()
    {
        coolTime += Time.deltaTime;//クールタイムにTime.deltaTimeを足す

        //上移動
        if (Input.GetKey(KeyCode.W))
        {
            nextPosition.y = nowPosition.y + speed * Time.deltaTime;
        }
        //下移動
        if (Input.GetKey(KeyCode.S))
        {
            nextPosition.y = nowPosition.y - speed * Time.deltaTime;
        }
        //左移動
        if (Input.GetKey(KeyCode.D))
        {
            nextPosition.z = nowPosition.z + speed * Time.deltaTime;
        }
        //右移動
        if (Input.GetKey(KeyCode.A))
        {
            nextPosition.z = nowPosition.z - speed * Time.deltaTime;
        }
        //移動後のビューポート座標値を取得
        viewX = Camera.main.WorldToViewportPoint(nextPosition).x;
        viewY = Camera.main.WorldToViewportPoint(nextPosition).y;
        //もし移動後のビューポートX座標が0から1の範囲ならば
        if (0 <= viewX && viewX <= 1)
        {
            //移動する
            transform.position = nextPosition;
            //nowPositionにnextPositionを代入する(次のUpdateで使う)
            nowPosition = nextPosition;
        }
        //もし移動後のビューポートX座標が0から1の範囲ならば
        //if (0 <= viewY && viewY <= 1)
        //{
        //    //移動する
        //    transform.position = nextPosition;
        //    //nowPositionにnextPositionを代入する(次のUpdateで使う)
        //    nowPosition = nextPosition;
        //}

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
