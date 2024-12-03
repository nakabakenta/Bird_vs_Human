using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAlly : MonoBehaviour
{
    //処理
    public static int allyCount;//味方カウント
    public static bool damege;  //
    private int allyNumber;     //味方番号
    private bool playerFollow;  //プレイヤーへの追尾の可否
    //ビューポイント座標.X
    private float viewPointX;
    //オブジェクト
    public GameObject[] ally = new GameObject[3];//仲間オブジェクト
    //コンポーネント(このオブジェクト)
    private Transform thisTransform;//"Transform"
    private BoxCollider boxCollider;//"BoxCollider"
    //private Animator animator = null;//"Animator"
    //コンポーネント(他のオブジェクト)
    private Transform playerTransform;//"Transform(プレイヤー)"

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.gameObject.transform;                //このオブジェクトの"Transform"を取得
        playerTransform = GameObject.Find("Player").transform;    //ゲームオブジェクト"Player"を探して"Transform"を取得
        boxCollider = this.gameObject.GetComponent<BoxCollider>();//このオブジェクトの"BoxCollider"を取得
        //animator = this.gameObject.GetComponent<Animator>();    //このオブジェクトの"Animator"を取得
        playerFollow = false;                                     //プレイヤーへの追尾を"false"にする

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

        if(damege == true && playerFollow == true && allyNumber == 1)
        {
            Destroy(this.gameObject);
            allyCount -= 1;
            damege = false;
            //animator.SetBool("Death", true);//"Animator"の"Death"(死亡)を有効にする
        }
        else if(allyNumber == 2)
        {
            allyNumber = 1;
        }

        if(playerFollow == false && viewPointX < 0)
        {
            Destroy(this.gameObject);
        }

        Debug.Log(allyCount);
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Player"だったら
        if (collision.gameObject.tag == "Player" && allyCount < 3)
        {
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, 90.0f, this.transform.rotation.z);

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
