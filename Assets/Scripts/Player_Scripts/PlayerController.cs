using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PlayerBase
{
    //移動の限界位置
    private Vector2[,] limitPosition = new Vector2[5, 2]
    {
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.8f),},
    };
    //このオブジェクトのコンポーネント
    public GameObject[] player = new GameObject[3];  //"GameObject(プレイヤー)"
    public GameObject forwardBullet, downBullet;     //"GameObject(弾)"
    public GameObject[] group = new GameObject[3];   //"GameObject(群れ)"
    //コルーチン
    private Coroutine blinking;//

    public int PlayerHp
    {
        get { return hp; }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        StartPlayer();
        //選択したプレイヤーをこのオブジェクトの子オブジェクトとして生成する
        nowPlayer = Instantiate(player[GameManager.selectPlayer], this.transform.position, Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        //このオブジェクトのコンポーネントを取得
        animator = nowPlayer.GetComponent<Animator>();
        thisRenderer = this.gameObject.GetComponentsInChildren<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //体力が"0より上"の場合
        if (hp > 0)
        {
            UpdatePlayer();//関数"UpdatePlayer"を実行する
        }
    }

    //関数"UpdatePlayer"
    public override void UpdatePlayer()
    {
        //ゲームの状態が"Play"の場合
        if (Stage.status == "Play")
        {
            //攻撃タイマーに経過時間を足す
            attackTimer[0] += Time.deltaTime;//攻撃タイマー[前方]
            attackTimer[1] += Time.deltaTime;//攻撃タイマー[下方]
            //マウスの位置を取得する
            mousePosition = Input.mousePosition;
            //マウスの位置(スクリーン座標)をビューポイント座標に変換する
            viewPortPosition = Camera.main.ScreenToViewportPoint(new Vector3(mousePosition.x, mousePosition.y, 9.0f));
            //移動の限界位置を設定する
            viewPortPosition.x = Mathf.Clamp(viewPortPosition.x, limitPosition[Stage.nowStage - 1, 0].x, limitPosition[Stage.nowStage - 1, 1].x);
            viewPortPosition.y = Mathf.Clamp(viewPortPosition.y, limitPosition[Stage.nowStage - 1, 0].y, limitPosition[Stage.nowStage - 1, 1].y);
            //ビューポイント座標をワールド座標に変換する
            this.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewPortPosition.x, viewPortPosition.y, 9.0f));

            //プレイヤーの状態が"Normal"の場合
            if (status == "Normal")
            {
                if (GameManager.selectPlayer == 0)
                {
                    attackTimeInterval[0] = 2.0f;
                    attackTimeInterval[1] = 2.0f;
                }
                else if (GameManager.selectPlayer == 1)
                {
                    attackTimeInterval[0] = 3.0f;
                    attackTimeInterval[1] = 3.0f;
                }
                else if (GameManager.selectPlayer == 2)
                {
                    attackTimeInterval[0] = 1.0f;
                    attackTimeInterval[1] = 1.0f;
                }

                gageTimer += Time.deltaTime;//ゲージタイマーに経過時間を足す
            }
            //プレイヤーの状態が"Invincible"の場合
            else if (status == "Invincible")
            {
                //無敵時の攻撃間隔を設定する
                attackTimeInterval[0] = PlayerBase.InvincibleStatus.attackSpeed;
                attackTimeInterval[1] = PlayerBase.InvincibleStatus.attackSpeed;
            }

            //前方攻撃
            //マウスが"左クリックされた"&&"攻撃タイマー[前方]"が"攻撃間隔[前方]" - "レベルアップ時の攻撃間隔短縮"以上の場合
            if (Input.GetMouseButton(0) && attackTimer[0] >= attackTimeInterval[0] - levelAttackInterval)
            {
                //このオブジェクトの位置に前方弾を生成する
                Instantiate(forwardBullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
                //攻撃タイマー[前方]を初期化する
                attackTimer[0] = 0.0f;
            }
            //下方攻撃
            //マウスが"右クリックされた"&&"攻撃タイマー[下方]"が"攻撃間隔[下方]" - "レベルアップ時の攻撃間隔短縮"以上の場合
            if (Input.GetMouseButton(1) && attackTimer[1] >= attackTimeInterval[1] - levelAttackInterval)
            {
                //このオブジェクトの位置に下方弾を生成する
                Instantiate(downBullet, this.transform.position, Quaternion.identity);
                //攻撃タイマー[下方]を初期化する
                attackTimer[1] = 0.0f;
            }
            //ゲージ解放
            //マウスホイールが"クリックされた"&&"ゲージタイマー"が"ゲージ蓄積時間"以上の場合
            if (Input.GetMouseButtonDown(2) && gageTimer >= gageTimeInterval)
            {
                //プレイヤーの状態を"Invincible"にする
                status = "Invincible";
                //このオブジェクトの位置に群れを生成する
                Instantiate(group[GameManager.selectPlayer], this.transform.position, Quaternion.identity);
                //ゲージタイマーを初期化する
                gageTimer = 0.0f;
            }
            //経験値が最大経験値と等しい場合
            if (exp == maxExp)
            {
                LevelUp();//関数"LevelUp"を実行する
            }
        }
        //Escキーが"押された"&&ゲームの状態が"Play"の場合
        if (Input.GetKeyDown(KeyCode.Escape) && Stage.status == "Play")
        {
            Stage.status = "Pause";//ゲームの状態を"Pause"にする
        }
        //Escキーが"押された"&&ゲームの状態が"Pause"の場合
        else if (Input.GetKeyDown(KeyCode.Escape) && Stage.status == "Pause")
        {
            Stage.status = "Play";//ゲームの状態を"Play"にする
        }
        //プレイヤーの状態が"Invincible"の場合
        if (status == "Invincible")
        {
            Invincible();//関数"Invincible"を実行する
        }
    }

    //関数"SetObjRenderer"
    void SetObjRenderer(bool set)
    {
        for (int i = 0; i < thisRenderer.Length; i++)
        {
            thisRenderer[i].enabled = set;//RendererをthisRendererにセットする
        }
    }

    //関数"LevelUp"
    void LevelUp()
    {
        hp += 1;
        exp = 0;//経験値を初期化する
    }

    //関数"Damage"
    public override void DamagePlayer()
    {
        base.DamagePlayer();

        //体力が"0より上"の場合
        if (hp > 0)
        {
            StartCoroutine("Blinking");//コルーチン"Blinking"を実行する
        }
        //体力が"0以下"だったら
        else if (hp <= 0)
        {
           Death();
        }
    }

    //コルーチン"Blinking"
    IEnumerator Blinking()
    {
        isDamage = true;         //ダメージを"受けている"にする
        //タイマーを初期化
        rendererTimer = 0.0f;
        rendererTotalTime = 0.0f;

        while (true)
        {
            //タイマーに経過時間を足す
            rendererTimer += Time.deltaTime;
            rendererTotalTime += Time.deltaTime;
            //Renderer切り替えの経過時間がRenderer切り替え時間以上の場合
            if (rendererSwitch <= rendererTimer)
            {
                rendererTimer = 0.0f;          //Renderer切り替えの経過時間を初期化する
                isRenderer = !isRenderer;//"objRenderer"を"true"の場合は"false"、"false"の場合は"true"にする
                SetObjRenderer(isRenderer); //関数"SetObjRenderer"を実行する
            }
            //Renderer切り替えの合計経過時間が点滅持続時間以上の場合
            if (blinkingTime <= rendererTotalTime)
            {
                isDamage = false;    //ダメージを"受けていない"にする
                isRenderer = true;//Rendererを有効化する
                SetObjRenderer(true);//関数"SetObjRenderer"を実行する
                yield break;         //コルーチンを停止する
            }
            yield return null;
        }
    }

    //関数"Invincible"
    void Invincible()
    {
        invincibleTimer += Time.deltaTime;       //無敵タイマーに経過時間を足す
        //無敵タイマーが無敵持続時間以上の場合
        if (invincibleTimer >= invincibleInterval)
        {
            invincibleTimer = 0.0f; //無敵タイマーを初期化する
            status = "Normal";//プレイヤーの状態を"Normal"にする
        }  
    }

    //衝突判定(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);
    }

    //関数"Ally"
    void Ally()
    {
        //味方数が"0と等しい"場合
        if (ally == 0)
        {
            //味方を生成する
            playerAlly[ally] = Instantiate(player[GameManager.selectPlayer], new Vector3(this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }
        //味方数が"1と等しい"場合
        else if (ally == 1)
        {
            //味方を生成する
            playerAlly[ally] = Instantiate(player[GameManager.selectPlayer], new Vector3(this.transform.position.x - 2.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }

        ally += 1;//味方数を"+1"する
    }
}

