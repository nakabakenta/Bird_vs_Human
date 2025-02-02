using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : PlayerBase
{
    //処理
    private int allyNumber;        //味方番号
    private bool sacrifice = false;
    //このオブジェクトのコンポーネント
    public GameObject text;       //"GameObject(テキスト)"
    private GameObject allyObject;//"GameObject(味方)"

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        rotation = 90.0f;
        allyObject = Instantiate(player[GameManager.selectPlayer], this.transform.position, this.transform.rotation, thisTransform);
        animator = allyObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //このオブジェクトのワールド座標をビューポート座標に変換して取得する
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        if (viewPortPosition.x < 0 && (boxCollider.enabled == true || allyNumber > nowAlly))
        {
            Destroy();//関数"Destroy"を実行する
        }

        if (allyNumber > nowAlly)
        {
            if(sacrifice == false)
            {
                Death();
                sacrifice = true;
            }

            if (this.transform.position.y <= 0.0f)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
            }
        }
    }

    public override void Death()
    {
        this.gameObject.transform.SetParent(null);//親から外す
        base.Death();
    }

    //衝突判定(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Player"の場合
        if (collision.gameObject.tag == "Player" && nowAlly < Player.maxAlly)
        {
            boxCollider.enabled = false;//BoxColliderを"無効"にする
            text.SetActive(false);      //テキストを非表示にする
            nowAlly += 1;               //味方数を"+1"する
            allyNumber = nowAlly;
            
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, rotation, this.transform.rotation.z);
            this.gameObject.transform.SetParent(playerTransform);
            this.transform.position = new Vector3(playerTransform.position.x - (1.0f * allyNumber), playerTransform.position.y, playerTransform.position.z);
        }
    }
}