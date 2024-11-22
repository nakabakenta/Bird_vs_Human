using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //ステータス
    public static int hp;             //体力
    public static float speed;        //移動速度
    public static string playerStatus;//プレイヤーの状態
    public static bool useGage;       //ゲージの使用可否
    public static bool allySacrifice; //味方の犠牲可否
    //処理
    private float attackTimerF = 0.25f;   //前攻撃間隔タイマー
    private float attackTimerD = 0.50f;   //下攻撃間隔タイマー
    private float attackIntervalF = 0.25f;//攻撃間隔(前方攻撃)
    private float attackIntervalD = 0.50f;//攻撃間隔(落下攻撃)
    private float invincibleTimer = 0.0f; //無敵タイマー
    private float invincible = 10.0f;     //無敵継続時間
    //ビューポイント座標.X, Y
    private float viewPointX, viewPointY;
    //ダメージ関係変数
    private float blinkingTime = 1.0f;     //点滅・無敵の持続時間
    private float rendererSwitch = 0.05f;  //Rendererの有効・無効を切り替える時間(点滅の切り替える時間)
    private float rendererTimer;           //Rendererの有効・無効の経過時間(点滅の経過時間)
    private float rendererTotalElapsedTime;//Rendererの有効・無効の合計経過時間
    private bool isDamage;                 //ダメージの可否
    private bool isObjRenderer;            //objRendererの有効・無効フラグ
    //オブジェクト
    private GameObject[] player = new GameObject[3];//プレイヤーオブジェクト
    public GameObject forwardBullet, downBullet;    //弾オブジェクト
    public GameObject[] group = new GameObject[3];  //群れオブジェクト
    //このオブジェクトのコンポーネント
    private Rigidbody rigidBody;     //"Rigidbody"
    private BoxCollider boxCollider; //"BoxCollider"
    private Animator animator = null;//"Animator"
    private Renderer[] objRenderer;  //"Renderer"
    //コルーチン
    private Coroutine blinking;//
    //マウス座標
    private Vector3 mousePosition, worldPosition;

    // Start is called before the first frame update
    void Start()
    {
        allySacrifice = false;

        //プレイヤーオブジェクトを探して取得する
        player[0] = GameObject.Find("Sparrow_Player");
        player[1] = GameObject.Find("Crow_Player");
        player[2] = GameObject.Find("Chickadee_Player");

        objRenderer = this.gameObject.GetComponentsInChildren<Renderer>();//このオブジェクトのRenderer(子オブジェクトを含む)を取得
        rigidBody = this.gameObject.GetComponent<Rigidbody>();            //このオブジェクトのRigidbodyを取得
        boxCollider = this.gameObject.GetComponent<BoxCollider>();        //このオブジェクトのBoxColliderを取得
        useGage = false;
        playerStatus = "Normal";

        player[0].SetActive(false);
        player[1].SetActive(false);
        player[2].SetActive(false);

        SetPlayer();//関数"SetPlayer"を呼び出す
    }

    // Update is called once per frame
    void Update()
    {
        //クールタイムにTime.deltaTimeを足す
        attackTimerF += Time.deltaTime;
        attackTimerD += Time.deltaTime;

        if(hp > 0)
        {
            Behavior();//関数PlayControlを呼び出す
        }

        //ビューポイント座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面X座標
        viewPointY = Camera.main.WorldToViewportPoint(this.transform.position).y;//画面Y座標
    }

    //ステータスを設定
    void SetPlayer()
    {
        player[GameManager.playerNumber].SetActive(true);
        animator = player[GameManager.playerNumber].GetComponent<Animator>();//"Animator"を取得

        if(GameManager.gameStart == false)
        {
            GameManager.remain = 3;
            GameManager.gameStart = true;
        }

        hp = PlayerList.Player.hp[GameManager.playerNumber];      //体力
        speed = PlayerList.Player.speed[GameManager.playerNumber];//移動速度
    }

    //動作関数
    void Behavior()
    {
        //マウス座標を取得して、スクリーン座標をワールド座標に変換する
        worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 9.0f));

        //
        if(Stage.gameStatus == "Play")
        {
            this.transform.position = worldPosition;

            //前方攻撃
            if (Input.GetMouseButton(0) && attackTimerF > attackIntervalF)
            {
                Instantiate(forwardBullet, this.transform.position, Quaternion.identity);
                attackTimerF = 0.0f;
            }
            //落下攻撃
            if (Input.GetMouseButton(1) && attackTimerD > attackIntervalD)
            {
                Instantiate(downBullet, this.transform.position, Quaternion.identity);
                attackTimerD = 0.0f;
            }
            //ゲージ解放
            if (Input.GetKeyDown(KeyCode.E) && useGage == true)
            {
                useGage = false;
                playerStatus = "Invincible";
                Instantiate(group[GameManager.playerNumber], this.transform.position, Quaternion.identity);
            }
        }

        //Vector3 position = this.transform.position;

        //if (viewPointX >= 0 && viewPointX <= 1)
        //{
        //    position.x = worldPosition.x;
        //    this.transform.position = position;
        //}
        //else
        //{

        //}

        //if (viewPointY >= 0 && viewPointY <= 1)
        //{
        //    position.y = worldPosition.y;
        //    this.transform.position = position;
        //}
        //else
        //{

        //}

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

    //ダメージ関数
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
        hp = 0;                         //hpを"0"にする
        boxCollider.enabled = false;    //BoxColliderを無効にする
        animator.SetBool("Death", true);//Animatorの"Death"(死亡)を有効にする
        rigidBody.useGravity = true;    //RigidBodyの重力を有効にする
        GameManager.remain--;
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Enemy" && "playerStatus"が"Normal"だったら 
        if (collision.gameObject.tag == "Enemy" && playerStatus == "Normal")
        {
            if(BirdAlly.allyCount > 0)
            {
                BirdAlly.allyCount--;
                allySacrifice = true;
            }
            else if(BirdAlly.allyCount <= 0)
            {
                Damage();//ダメージ関数"Damage"を実行する
            }
        }
    }
}