using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //ステータス
    public static int hp;             //体力
    public static int remain;         //残機
    public static string playerStatus;//プレイヤーの状態
    //移動の限界位置
    private Vector2[,] limitPosition = new Vector2[5, 2]
    {
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.8f),},
    };
    //処理
    public static float[] attackTimer = new float[2];   //攻撃タイマー([前方],[下方])
    public static float[] attackInterval = new float[2];//攻撃間隔([前方],[下方])
    public static float gageTimer;                      //ゲージタイマー
    public static float gageInterval;                   //ゲージ蓄積時間
    public static int level;                            //レベル
    public static int exp;                              //経験値
    public static int ally;                             //味方数
    private float invincibleTimer = 0.0f;               //無敵タイマー
    private float invincibleInterval = 10.0f;           //無敵持続時間
    private float blinkingTime = 1.0f;                  //点滅持続時間
    private float rendererSwitch = 0.05f;               //Renderer切り替え時間
    private float rendererTimer;                        //Renderer切り替えの経過時間
    private float rendererTotalTime;                    //Renderer切り替えの合計経過時間
    private bool isDamage;                              //ダメージの可否
    private bool isObjRenderer;                         //objRendererの可否
    private float levelAttackInterval = 0.0f;           //レベルアップ時の攻撃間隔短縮
    //このオブジェクトのコンポーネント
    public GameObject[] player = new GameObject[3];  //"GameObject(プレイヤー)"
    public GameObject forwardBullet, downBullet;     //"GameObject(弾)"
    public GameObject[] group = new GameObject[3];   //"GameObject(群れ)"
    private GameObject nowPlayer;                    //"GameObject(現在のプレイヤー)"
    private GameObject[] nowAlly = new GameObject[2];//"GameObject(現在の味方)"
    private Transform thisTransform ;                //"Transform"
    private Rigidbody rigidBody;                     //"Rigidbody"
    private BoxCollider boxCollider;                 //"BoxCollider"
    private Renderer[] objRenderer;                  //"Renderer"
    //コルーチン
    private Coroutine blinking;//
    //座標
    private Vector3 mousePosition, worldPosition, viewPortPosition;

    // Start is called before the first frame update
    void Start()
    {
        //処理を初期化する
        gageTimer = 0.0f;    
        gageInterval = 20.0f;
        ally = 0;
        level = 1;
        exp = 0;
        thisTransform = this.gameObject.transform;//このオブジェクトの"Transform"を取得
        //このオブジェクトのコンポーネントを取得する
        objRenderer = this.gameObject.GetComponentsInChildren<Renderer>();
        rigidBody = this.gameObject.GetComponent<Rigidbody>();            
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
        
        SetPlayer();//関数"SetPlayer"を実行する
    }

    // Update is called once per frame
    void Update()
    {
        //体力が"0より上"の場合
        if (hp > 0)
        {
            Action();//関数"Action"を実行する
        }
    }

    //関数"SetPlayer"
    void SetPlayer()
    {
        //選択したプレイヤーをこのオブジェクトの子オブジェクトとして生成する
        nowPlayer = Instantiate(player[GameManager.playerNumber], this.transform.position, Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        //選択したプレイヤーのステータスを設定する
        hp = PlayerList.Player.hp[GameManager.playerNumber];                           //体力
        attackTimer[0] = PlayerList.Player.attackInterval[0, GameManager.playerNumber];//攻撃タイマー[前方]
        attackTimer[1] = PlayerList.Player.attackInterval[1, GameManager.playerNumber];//攻撃タイマー[下方]
        playerStatus = "Normal";                                                       //プレイヤーの状態を"Normal"にする
        //ゲームを"開始していない"場合
        if (GameManager.gameStart == false)
        {
            remain = 3;                  //残機
            GameManager.gameStart = true;//ゲームを"開始した"にする
        }
    }

    //関数"Action"
    void Action()
    {
        //ゲームの状態が"Play"の場合
        if (Stage.gameStatus == "Play")
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
            if (playerStatus == "Normal")
            {
                //選択したプレイヤーの攻撃間隔を設定する
                attackInterval[0] = PlayerList.Player.attackInterval[0, GameManager.playerNumber];
                attackInterval[1] = PlayerList.Player.attackInterval[1, GameManager.playerNumber];

                gageTimer += Time.deltaTime;//ゲージタイマーに経過時間を足す
            }
            //プレイヤーの状態が"Invincible"の場合
            else if (playerStatus == "Invincible")
            {
                //無敵時の攻撃間隔を設定する
                attackInterval[0] = PlayerList.Invincible.attackInterval[0];
                attackInterval[1] = PlayerList.Invincible.attackInterval[1];
            }

            //前方攻撃
            //マウスが"左クリックされた"&&"攻撃タイマー[前方]"が"攻撃間隔[前方]" - "レベルアップ時の攻撃間隔短縮"以上の場合
            if (Input.GetMouseButton(0) && attackTimer[0] >= attackInterval[0] - levelAttackInterval)
            {
                //このオブジェクトの位置に前方弾を生成する
                Instantiate(forwardBullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
                //攻撃タイマー[前方]を初期化する
                attackTimer[0] = 0.0f;
            }
            //下方攻撃
            //マウスが"右クリックされた"&&"攻撃タイマー[下方]"が"攻撃間隔[下方]" - "レベルアップ時の攻撃間隔短縮"以上の場合
            if (Input.GetMouseButton(1) && attackTimer[1] >= attackInterval[1] - levelAttackInterval)
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
                playerStatus = "Invincible";
                //このオブジェクトの位置に群れを生成する
                Instantiate(group[GameManager.playerNumber], this.transform.position, Quaternion.identity);
                //ゲージタイマーを初期化する
                gageTimer = 0.0f;
            }
            //経験値が最大経験値と等しい場合
            if (exp == PlayerList.Player.maxExp[GameManager.playerNumber])
            {
                LevelUp();//関数"LevelUp"を実行する
            }
        }
        //Escキーが"押された"&&ゲームの状態が"Play"の場合
        if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Play")
        {
            Stage.gameStatus = "Pause";//ゲームの状態を"Pause"にする
        }
        //Escキーが"押された"&&ゲームの状態が"Pause"の場合
        else if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Pause")
        {
            Stage.gameStatus = "Play";//ゲームの状態を"Play"にする
        }
        //プレイヤーの状態が"Invincible"の場合
        if (playerStatus == "Invincible")
        {
            Invincible();//関数"Invincible"を実行する
        }
    }

    //関数"SetObjRenderer"
    void SetObjRenderer(bool set)
    {
        for (int i = 0; i < objRenderer.Length; i++)
        {
            objRenderer[i].enabled = set;//RendererをobjRendererにセットする
        }
    }

    ///関数"LevelUp"
    void LevelUp()
    {
        //レベルが"5以下"の場合
        if(level >= 5)
        {
            levelAttackInterval += 0.1f;//レベルアップ時の攻撃間隔短縮に0.1f足す
        }

        exp = 0;//経験値を初期化する
    }

    //関数"Damage"
    void Damage()
    {
        //ダメージが"受けている"場合
        if (isDamage == true)
        {
            return;//返す
        }

        hp -= 1;//体力を"-1"する

        //体力が"0より上"の場合
        if (hp > 0)
        {
            StartCoroutine("Blinking");//コルーチン"Blinking"を実行する
        }
        //体力が"0以下"だったら
        else if (hp <= 0)
        {
            Death();//関数"Death"を実行する
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
            rendererTotalTime += Time.deltaTime;
            rendererTimer += Time.deltaTime;
            //Renderer切り替えの経過時間がRenderer切り替え時間以上の場合
            if (rendererTimer >= rendererSwitch)
            {
                rendererTimer = 0.0f;          //Renderer切り替えの経過時間を初期化する
                isObjRenderer = !isObjRenderer;//"objRenderer"を"true"の場合は"false"、"false"の場合は"true"にする
                SetObjRenderer(isObjRenderer); //関数"SetObjRenderer"を実行する
            }
            //Renderer切り替えの合計経過時間が点滅持続時間以上の場合
            if (rendererTotalTime >= blinkingTime)
            {
                isDamage = false;    //ダメージを"受けていない"にする
                isObjRenderer = true;//Rendererを有効化する
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
            playerStatus = "Normal";//プレイヤーの状態を"Normal"にする
        }  
    }

    //関数"Death"
    void Death()
    {
        //アニメーターコンポーネントを取得する
        Animator animator = nowPlayer.GetComponent<Animator>();
        hp = 0;                         //体力を"0"にする
        boxCollider.enabled = false;    //BoxColliderを"無効"にする
        animator.SetBool("Death", true);//Animatorを"Death"にする
        rigidBody.useGravity = true;    //RigidBodyの重力を有効にする
        remain -= 1;                    //残機を"-1"する
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //(衝突したオブジェクトのタグが"Enemy" || "BossEnemy" || "EnemyBullet" ) && プレイヤーの状態が"Normal"の場合
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy" || collision.gameObject.tag == "EnemyBullet") && playerStatus == "Normal")
        {
            //味方数が"0より上"の場合
            if (ally > 0)
            {
                Destroy(nowAlly[ally - 1]);//味方を消す
                ally -= 1;                 //味方数を"-1"する
            }
            //味方数が"0以下"の場合
            else if (ally <= 0)
            {
                Damage();//関数"Damage"を実行する
            }
        }

        //衝突したオブジェクトのタグが"PlayerAlly"の場合
        if (collision.gameObject.tag == "PlayerAlly" && ally < 2)
        {
            Invoke("Ally", 0.01f);//関数"Ally"を"0.01f"後に実行する
        }
    }

    //関数"Ally"
    void Ally()
    {
        //味方数が"0と等しい"場合
        if (ally == 0)
        {
            //味方を生成する
            nowAlly[ally] = Instantiate(player[GameManager.playerNumber], new Vector3(this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }
        //味方数が"1と等しい"場合
        else if (ally == 1)
        {
            //味方を生成する
            nowAlly[ally] = Instantiate(player[GameManager.playerNumber], new Vector3(this.transform.position.x - 2.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }

        ally += 1;//味方数を"+1"する
    }
}

public class PlayerBase : CharacterBase
{
    //public override void Damage()
    //{

    //}
}