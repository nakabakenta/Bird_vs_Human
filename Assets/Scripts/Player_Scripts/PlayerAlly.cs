using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlly : MonoBehaviour
{
    //処理
    private float viewPointX;//ビューポイント座標.X
    //オブジェクト
    public GameObject[] ally = new GameObject[3];//仲間オブジェクト
    //コンポーネント(このオブジェクト)
    private Transform thisTransform;//"Transform"
    private BoxCollider boxCollider;//"BoxCollider"
    //コンポーネント(他のオブジェクト)
    private Transform playerTransform;//"Transform(プレイヤー)"

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.gameObject.transform;                //このオブジェクトの"Transform"を取得
        playerTransform = GameObject.Find("Player").transform;    //ゲームオブジェクト"Player"を探して"Transform"を取得
        boxCollider = this.gameObject.GetComponent<BoxCollider>();//このオブジェクトの"BoxCollider"を取得

        Instantiate(ally[GameManager.playerNumber], this.transform.position, this.transform.rotation, thisTransform);
    }

    // Update is called once per frame
    void Update()
    {
        //ビューポイント座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面X座標

        if(viewPointX < 0)
        {
            Destroy();//関数"Destroy"を実行
        }
    }

    //関数"Destroy"
    void Destroy()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Player"の場合
        if (collision.gameObject.tag == "Player" && PlayerController.ally < 2)
        {
            Destroy();//関数"Destroy"を実行
        }
    }
}