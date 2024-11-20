using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAlly : MonoBehaviour
{
    //処理
    public static int allyCount;//味方カウント
    private int allyNumber;     //味方番号
    private bool playerFollow;  //追尾の可否
    //ビューポイント座標.X
    private float viewPointX;
    //オブジェクト
    public GameObject[] ally = new GameObject[3];//仲間オブジェクト
    //コンポーネント
    private Transform thisTransform;  //Transform(このオブジェクト)
    private Transform playerTransform;//Transform(プレイヤー)
    private BoxCollider boxCollider;  //BoxCollider


    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.gameObject.transform;
        playerTransform = GameObject.Find("Player").transform;    //ゲームオブジェクト"Player"を探して位置を取得
        boxCollider = this.gameObject.GetComponent<BoxCollider>();//このオブジェクトの"BoxCollider"を取得
        playerFollow = false;

        Instantiate(ally[GameManager.playerNumber], this.transform.position, this.transform.rotation, thisTransform);
    }

    // Update is called once per frame
    void Update()
    {
        //ビューポイント座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面X座標

        if (playerFollow == true && allyNumber == 1)
        {
            this.transform.position = new Vector3(playerTransform.position.x - 1.0f, playerTransform.position.y, playerTransform.position.z);
        }
        else if(playerFollow == true && allyNumber == 2)
        {
            this.transform.position = new Vector3(playerTransform.position.x - 2.0f, playerTransform.position.y, playerTransform.position.z);
        }

        if(PlayerController.allySacrifice == true && playerFollow == true)
        {
            Destroy(this.gameObject);
            PlayerController.allySacrifice = false;
        }

        if(playerFollow == false && viewPointX < 0)
        {
            Destroy(this.gameObject);
        }
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Player"だったら
        if (collision.gameObject.tag == "Player" && allyCount < 3)
        {
            playerFollow = true;
            allyCount++;

            if(allyCount == 1)
            {
                allyNumber = 1;
            }
            else if (allyCount == 2)
            {
                allyNumber = 2;
            }
        }
    }
}
