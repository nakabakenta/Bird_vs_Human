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

    private float intervalF = 0.25f;//間隔(前方攻撃)
    private float intervalD = 0.50f;//間隔(落下攻撃)
    private float attackL = 0.25f;  //攻撃が出るまでの間隔(前方攻撃)
    private float attackR = 0.50f;  //攻撃が出るまでの間隔(落下攻撃)

    private float elapsedTime = 0.0f;
    private float invincibleTime = 10.0f;

    //ビューポート座標変数
    private float viewPointX;//ビューポイント座標.X
    private float viewPointY;//ビューポイント座標.Y
    //移動フラグ変数
    private bool forward; //前移動
    private bool backward;//後移動
    private bool up;      //上移動
    private bool down;    //下移動
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
        intervalF += Time.deltaTime;
        intervalD += Time.deltaTime;

        if(hp > 0)
        {
            Behavior();//関数PlayControlを呼び出す
        }

        //移動後のビューポート座標値を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面X座標
        viewPointY = Camera.main.WorldToViewportPoint(this.transform.position).y;//画面Y座標

        //移動可能な画面範囲指定
        //-X座標
        if (viewPointX >= 0)
        {
            backward = true;
        }
        else
        {
            backward = false;
        }
        //+X座標
        if (viewPointX <= 1)
        {
            forward = true;
        }
        else
        {
            forward = false;
        }
        //-Y座標
        if (viewPointY >= 0)
        {
            down = true;
        }
        else
        {
            down = false;
        }
        //+Y座標
        if (viewPointY <= 1)
        {
            up = true;
        }
        else
        {
            up = false;
        }

        if(playerStatus == "Invincible")
        {
            Invincible();
        }
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
        //体力が0より上だったら
        if (hp > 0)
        {
            //移動処理
            //前移動
            if (Input.GetKey(KeyCode.D) && forward == true)
            {
                this.transform.position += speed * transform.forward * Time.deltaTime;
            }
            //後移動
            if (Input.GetKey(KeyCode.A) && backward == true)
            {
                this.transform.position -= speed * transform.forward * Time.deltaTime;
            }
            //上移動
            if (Input.GetKey(KeyCode.W) && up == true)
            {
                this.transform.position += speed * transform.up * Time.deltaTime;
            }
            //下移動
            if (Input.GetKey(KeyCode.S) && down == true)
            {
                this.transform.position -= speed * transform.up * Time.deltaTime;
            }
            //前方攻撃
            if (Input.GetMouseButton(0) && intervalF > attackL)
            {
                Instantiate(forwardBullet, this.transform.position, Quaternion.identity);
                intervalF = 0.0f;
            }
            //落下攻撃
            if (Input.GetMouseButton(1) && intervalD > attackR)
            {
                Instantiate(downBullet, this.transform.position, Quaternion.identity);
                intervalD = 0.0f;
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
        }
    }

    void SetObjRenderer(bool set)
    {
        for (int i = 0; i < objRenderer.Length; i++)
        {
            objRenderer[i].enabled = set;//RendererをobjRendererにセットする
        }
    }

    //ダメージ判定
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

    void Invincible()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > invincibleTime)
        {
            elapsedTime = 0.0f;
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