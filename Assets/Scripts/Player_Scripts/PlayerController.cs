using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerBase
{
    //このオブジェクトのコンポーネント
    public GameObject forwardBullet, downBullet;  //"GameObject(弾)"
    public GameObject[] group = new GameObject[3];//"GameObject(群れ)"

    public int PlayerHp
    {
        get { return hp; }
    }

    // Start is called before the first frame update
    void Start()
    {
        BaseStart();
        //選択したプレイヤーをこのオブジェクトの子オブジェクトとして生成する
        playerObject = Instantiate(player[GameManager.selectPlayer], new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        //このオブジェクトのコンポーネントを取得
        animator = playerObject.GetComponent<Animator>();
        thisRenderer = this.gameObject.GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayer();//関数"UpdatePlayer"を実行する
    }

    public override void InputButton()
    {
        //前方攻撃
        //マウスが"左クリックされた"&&"攻撃タイマー[前方]"が"攻撃間隔[前方]" - "レベルアップ時の攻撃間隔短縮"以上の場合
        if (Input.GetMouseButton(0) && attackTimer[0] >= attackInterval)
        {
            //このオブジェクトの位置に前方弾を生成する
            Instantiate(forwardBullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
            //攻撃タイマー[前方]を初期化する
            attackTimer[0] = 0.0f;
        }
        //下方攻撃
        //マウスが"右クリックされた"&&"攻撃タイマー[下方]"が"攻撃間隔[下方]" - "レベルアップ時の攻撃間隔短縮"以上の場合
        if (Input.GetMouseButton(1) && attackTimer[1] >= attackInterval)
        {
            //このオブジェクトの位置に下方弾を生成する
            Instantiate(downBullet, this.transform.position, Quaternion.identity);
            //攻撃タイマー[下方]を初期化する
            attackTimer[1] = 0.0f;
        }
        //ゲージ解放
        //マウスホイールが"クリックされた"&&"ゲージタイマー"が"ゲージ蓄積時間"以上の場合
        if (Input.GetMouseButtonDown(2) && gageTimer >= gageInterval)
        {
            //プレイヤーの状態を"Invincible"にする
            status = "Invincible";
            //このオブジェクトの位置に群れを生成する
            groupObject = Instantiate(group[GameManager.selectPlayer], new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.identity);
            groupObject.transform.SetParent(playerTransform);
            //ゲージタイマーを初期化する
            gageTimer = 0.0f;
        }
    }

    public override void Death()
    {
        base.Death();
        boxCollider.enabled = false;//BoxColliderを"無効"にする
        hp = 0;                     //体力を"0"にする
        remain -= 1;                //残機を"-1"する
        status = "Death";
    }

    //衝突判定(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }
}

