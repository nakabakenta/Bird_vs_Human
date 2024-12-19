using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //ステータス(public)
    public static int hp;             //プレイヤーの体力
    public static string playerStatus;//プレイヤーの状態
    //プレイヤーの移動限界値
    private Vector2[,] limitPosition = new Vector2[5, 2]
    {
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.2f), new Vector2(1.0f, 0.8f),},
        { new Vector2(0.0f, 0.0f), new Vector2(1.0f, 0.8f),},
    };
    //処理
    public static float[] attackTimer = new float[2];   //攻撃間隔タイマー
    public static float[] attackInterval = new float[2];//攻撃間隔
    public static float gageTimer = 0.0f;               //ゲージタイマー
    public static float gageInterval = 20.0f;           //ゲージ蓄積時間
    public static int level;               //レベル
    public static int exp;                 //経験値
    public static int ally;                //味方数
    private float invincibleTimer = 0.0f;  //無敵タイマー
    private float invincible = 10.0f;      //無敵継続時間
    private float blinkingTime = 1.0f;     //点滅・無敵の持続時間
    private float rendererSwitch = 0.05f;  //Rendererの有効・無効を切り替える時間(点滅の切り替える時間)
    private float rendererTimer;           //Rendererの有効・無効の経過時間(点滅の経過時間)
    private float rendererTotalElapsedTime;//Rendererの有効・無効の合計経過時間
    private bool isDamage;                 //ダメージの可否
    private bool isObjRenderer;            //objRendererの可否
    //このオブジェクトのコンポーネント
    public GameObject[] player = new GameObject[3];  //"GameObject(プレイヤー)"
    public GameObject forwardBullet, downBullet;     //"GameObject(弾)"
    public GameObject[] group = new GameObject[3];   //"GameObject(群れ)"
    private GameObject nowPlayer;                    //"GameObject(現在のプレイヤー)"
    private GameObject[] nowally = new GameObject[2];//
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
        ally = 0;
        level = 1;
        exp = 0;

        thisTransform = this.gameObject.transform;//このオブジェクトの"Transform"を取得
        SetPlayer();                              //関数"SetPlayer"を実行
        //コンポーネントを取得
        objRenderer = this.gameObject.GetComponentsInChildren<Renderer>();//このオブジェクトの"Renderer(子オブジェクトを含む)を取得
        rigidBody = this.gameObject.GetComponent<Rigidbody>();            //このオブジェクトの"Rigidbody"を取得
        boxCollider = this.gameObject.GetComponent<BoxCollider>();        //このオブジェクトの"BoxCollider"を取得
        playerStatus = "Normal";
    }

    // Update is called once per frame
    void Update()
    {
        if (hp > 0)
        {
            Behavior();//関数PlayControlを呼び出す
        }
    }

    //関数"SetPlayer"
    void SetPlayer()
    {
        nowPlayer = Instantiate(player[GameManager.playerNumber], this.transform.position, Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        player[GameManager.playerNumber].SetActive(true);

        if(GameManager.gameStart == false)
        {
            GameManager.remain = 3;
            GameManager.gameStart = true;
        }

        hp = PlayerList.Player.hp[GameManager.playerNumber];                           //体力
        attackTimer[0] = PlayerList.Player.attackInterval[0, GameManager.playerNumber];
        attackTimer[1] = PlayerList.Player.attackInterval[1, GameManager.playerNumber];
    }

    //関数"Behavior"
    void Behavior()
    {
        //
        if (Stage.gameStatus == "Play")
        {
            //クールタイムにTime.deltaTimeを足す
            attackTimer[0] += Time.deltaTime;
            attackTimer[1] += Time.deltaTime;

            mousePosition = Input.mousePosition;
            viewPortPosition = Camera.main.ScreenToViewportPoint(new Vector3(mousePosition.x, mousePosition.y, 9.0f));

            viewPortPosition.x = Mathf.Clamp(viewPortPosition.x, limitPosition[Stage.nowStage - 1, 0].x, limitPosition[Stage.nowStage - 1, 1].x);
            viewPortPosition.y = Mathf.Clamp(viewPortPosition.y, limitPosition[Stage.nowStage - 1, 0].y, limitPosition[Stage.nowStage - 1, 1].y);

            this.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(viewPortPosition.x, viewPortPosition.y, 9.0f));

            if (playerStatus == "Normal")
            {
                attackInterval[0] = PlayerList.Player.attackInterval[0, GameManager.playerNumber];
                attackInterval[1] = PlayerList.Player.attackInterval[1, GameManager.playerNumber];
                gageTimer += Time.deltaTime;//ゲージタイマーにTime.deltaTimeを足す
            }
            else if(playerStatus == "Invincible")
            {
                attackInterval[0] = PlayerList.Invincible.attackInterval[0];
                attackInterval[1] = PlayerList.Invincible.attackInterval[1];
            }

            //前方攻撃
            if (Input.GetMouseButton(0) && attackTimer[0] >= attackInterval[0])
            {
                Instantiate(forwardBullet, this.transform.position, Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z));
                attackTimer[0] = 0.0f;
            }
            //落下攻撃
            if (Input.GetMouseButton(1) && attackTimer[1] >= attackInterval[1])
            {
                Instantiate(downBullet, this.transform.position, Quaternion.identity);
                attackTimer[1] = 0.0f;
            }
            //ゲージ解放
            if (Input.GetMouseButtonDown(2) && gageTimer >= gageInterval)
            {
                gageTimer = 0.0f;
                playerStatus = "Invincible";
                Instantiate(group[GameManager.playerNumber], this.transform.position, Quaternion.identity);
            }

            if(exp == PlayerList.Player.maxExp[GameManager.playerNumber])
            {
                LevelUp();
            }
        }
        //
        if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Play")
        {
            Stage.gameStatus = "Pause";
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Pause")
        {
            Stage.gameStatus = "Play";
        }

        //プレイヤーの状態が"Invincible(無敵)"であれば
        if (playerStatus == "Invincible")
        {
            Invincible();//関数"Invincible(無敵)"を呼び出す
        }
    }

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
        if(level == 1)
        {

        }
        else if(level == 2)
        {

        }
        else if (level == 3)
        {

        }
        else if (level == 4)
        {

        }
        else if (level == 5)
        {

        }

        exp = 0;
    }

    //関数"Damage"
    void Damage()
    {
        //点滅中は二重に実行しない
        if (isDamage == true)
        {
            return;
        }

        hp -= 1;//体力"hp"を"-1"する

        //体力が0より上だったら
        if (hp > 0)
        {
            StartCoroutine("Blinking");//コルーチン"Blinking"を実行する
        }
        //体力が0以下だったら
        else if (hp <= 0)
        {
            Death();//死亡関数"Death"を実行する
        }
    }

    IEnumerator Blinking()
    {
        isDamage = true;

        rendererTotalElapsedTime = 0;
        rendererTimer = 0;

        while (true)
        {
            rendererTotalElapsedTime += Time.deltaTime;
            rendererTimer += Time.deltaTime;
            if (rendererSwitch <= rendererTimer)
            {
                rendererTimer = 0;             //被ダメージ点滅処理
                isObjRenderer = !isObjRenderer;//Rendererの有効・無効を切り替える(点滅処理)
                SetObjRenderer(isObjRenderer); //
            }

            if (blinkingTime <= rendererTotalElapsedTime)
            {
                //被ダメージ点滅の終了時の処理
                isDamage = false;
                isObjRenderer = true;//Rendererを有効化する
                SetObjRenderer(true);//
                yield break;
            }
            yield return null;
        }
    }

    //無敵関数
    void Invincible()
    {
        invincibleTimer += Time.deltaTime;

        if(invincibleTimer > invincible)
        {
            invincibleTimer = 0.0f;
            playerStatus = "Normal";
        }  
    }

    //死亡関数
    void Death()
    {
        Animator animator = nowPlayer.GetComponent<Animator>();//
        hp = 0;                         //hpを"0"にする
        boxCollider.enabled = false;    //BoxColliderを無効にする
        animator.SetBool("Death", true);//Animatorの"Death"(死亡)を有効にする
        rigidBody.useGravity = true;    //RigidBodyの重力を有効にする
        GameManager.remain--;
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Enemy && playerStatus"が"Normal"の場合
        if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy" || collision.gameObject.tag == "EnemyBullet") && playerStatus == "Normal")
        {
            if (ally > 0)
            {
                Destroy(nowally[ally - 1]);
                ally -= 1;
            }
            else if (ally <= 0)
            {
                Damage();//関数"Damage"を実行
            }
        }

        //衝突したオブジェクトのタグが"PlayerAlly"の場合
        if (collision.gameObject.tag == "PlayerAlly" && ally < 2)
        {
            Invoke("Ally", 0.01f);//関数"Ally"を"0.01f"後に実行
        }
    }

    void Ally()
    {
        if (ally == 0)
        {
            nowally[ally] = Instantiate(player[GameManager.playerNumber], new Vector3(this.transform.position.x - 1.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }
        else if (ally == 1)
        {
            nowally[ally] = Instantiate(player[GameManager.playerNumber], new Vector3(this.transform.position.x - 2.0f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(this.transform.rotation.x, 90, this.transform.rotation.z), thisTransform);
        }

        ally += 1;
    }
}