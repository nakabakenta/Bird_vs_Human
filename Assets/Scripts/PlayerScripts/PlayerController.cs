using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //ステータス
    public GameObject forwardBullet;//
    public GameObject downBullet;   //

    public GameObject aaa;

    public static int hp;             //体力
    public static float speed;        //移動速度
    public static bool gage;          //ゲージフラグ
    public static string playerStatus;//プレイヤーの状態

    private float intervalTimerF = 0.25f; //間隔タイマー(前方攻撃)
    private float intervalTimerD = 0.50f; //間隔タイマー(落下攻撃)
    private float attackIntervalF = 0.25f;//攻撃間隔(前方攻撃)
    private float attackIntervalD = 0.50f;//攻撃間隔(落下攻撃)

    private float invincibleTimer = 0.0f;//無敵タイマー
    private float invincible = 10.0f;    //無敵継続時間

    //ビューポート座標変数
    private float viewPointX, viewPointY;//ビューポイント座標.X, Y
    //ダメージ関係変数
    private float blinkingTime = 1.0f;     //点滅・無敵の持続時間
    private float rendererSwitch = 0.05f;  //Rendererの有効・無効を切り替える時間(点滅の切り替える時間)
    private float rendererElapsedTime;     //Rendererの有効・無効の経過時間(点滅の経過時間)
    private float rendererTotalElapsedTime;//Rendererの有効・無効の合計経過時間
    private bool isDamage;                 //ダメージを受けているかのフラグ
    private bool isObjRenderer;            //objRendererの有効・無効フラグ
    //コンポーネント取得変数
    private DamageManager damageManager;
    private Rigidbody rigidBody;     //Rigidbody変数
    private BoxCollider boxCollider; //CapsuleCollider変数
    private Animator animator = null;//Animator変数
    private Renderer[] objRenderer;  //Renderer配列変数
    //コルーチン変数
    private Coroutine blinking;//

    //マウス座標
    private Vector3 mousePosition, worldPosition;

    void Awake()
    {
        if (GameManager.gameStart == false)
        {
            SetPlayerStatus();//関数"SetPlayerStatus"を呼び出す
        }

        objRenderer = this.gameObject.GetComponentsInChildren<Renderer>();//このオブジェクトのRenderer(子オブジェクトを含む)を取得
    }

    // Start is called before the first frame update
    void Start()
    {
        damageManager = GetComponent<DamageManager>();//
        rigidBody = this.gameObject.GetComponent<Rigidbody>();      //このオブジェクトのRigidbodyを取得
        boxCollider = this.gameObject.GetComponent<BoxCollider>();  //このオブジェクトのBoxColliderを取得
        animator = this.GetComponent<Animator>();                   //このオブジェクトのAnimatorを取得
        gage = false;
        playerStatus = "Normal";
    }

    // Update is called once per frame
    void Update()
    {
        //クールタイムにTime.deltaTimeを足す
        intervalTimerF += Time.deltaTime;
        intervalTimerD += Time.deltaTime;

        if(hp > 0)
        {
            Behavior();//関数PlayControlを呼び出す
        }

        //移動後のビューポート座標値を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面X座標
        viewPointY = Camera.main.WorldToViewportPoint(this.transform.position).y;//画面Y座標
    }

    //ステータスを設定
    void SetPlayerStatus()
    {
        //スズメ
        if (GameManager.playerSelect == "Sparrow")
        {
            hp = PlayerStatus.Sparrow.hp;      //体力
            speed = PlayerStatus.Sparrow.speed;//移動速度
        }
        //カラス
        else if(GameManager.playerSelect == "Crow")
        {

        }
        //
        else if (GameManager.playerSelect == "Chickadee")
        {

        }
        //
        else if (GameManager.playerSelect == "Penguin")
        {

        }
        //それ以外"デバック用"(自動敵にスズメのステータスを参照する)
        else
        {
            GameManager.playerSelect = PlayerStatus.Sparrow.name;//
            hp = PlayerStatus.Sparrow.hp;                        //体力
            speed = PlayerStatus.Sparrow.speed;                  //移動速度
        }
        GameManager.remain = 3;
    }

    //動作関数
    void Behavior()
    {
        //マウス座標を取得して、スクリーン座標をワールド座標に変換する
        worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 7.0f));

        //
        if(Stage.gameStatus == "Play")
        {
            this.transform.position = worldPosition;
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

        //前方攻撃
        if (Input.GetMouseButton(0) && intervalTimerF > attackIntervalF)
        {
            Instantiate(forwardBullet, this.transform.position, Quaternion.identity);
            intervalTimerF = 0.0f;
        }
        //落下攻撃
        if (Input.GetMouseButton(1) && intervalTimerD > attackIntervalD)
        {
            Instantiate(downBullet, this.transform.position, Quaternion.identity);
            intervalTimerD = 0.0f;
        }
        //
        if (Input.GetKeyDown(KeyCode.E) && gage == true)
        {
            gage = false;
            playerStatus = "Invincible";
            Instantiate(aaa, this.transform.position, Quaternion.identity);
        }
        //
        if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Play")
        {
            Stage.gameStatus = "Menu";
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && Stage.gameStatus == "Menu")
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
        if (isDamage)
        {
            return;
        }

        //damageManager.PlayerDamage();

        hp -= 1;//体力を-1する

        //体力が0以下だったら
        if (hp <= 0)
        {
            hp = 0;                         //
            boxCollider.enabled = false;    //BoxColliderを無効化する
            animator.SetBool("Death", true);//AnimatorのDeath(死亡モーション)を有効化する
            rigidBody.useGravity = true;    //RigidBodyの重力を有効化する
        }
        StartCoroutine("Blinking");//コルーチン(Blinking)を呼び出す
    }

    IEnumerator Blinking()
    {
        isDamage = true;

        rendererTotalElapsedTime = 0;
        rendererElapsedTime = 0;

        while (true)
        {
            rendererTotalElapsedTime += Time.deltaTime;
            rendererElapsedTime += Time.deltaTime;
            if (rendererSwitch <= rendererElapsedTime)
            {
                rendererElapsedTime = 0;       //被ダメージ点滅処理
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

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //タグEnemyの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Enemy" && playerStatus == "Normal")
        {
            Damage();//関数Damageを呼び出す
        }
    }
}