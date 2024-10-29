using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //ステータス
    public GameObject forwardBullet;//
    public GameObject downBullet;   //

    public static int hp;     //体力
    public static float speed;//移動速度
    
    private float coolTimeL = 0.25f;//クールタイム(前方攻撃)
    private float coolTimeR = 0.50f;//クールタイム(落下攻撃)
    private float attackL = 0.25f;  //攻撃が出るまでの間隔(前方攻撃)
    private float attackR = 0.50f;  //攻撃が出るまでの間隔(落下攻撃)
    //ビューポート座標変数
    private float viewX;//ビューポートX座標
    private float viewY;//ビューポートY座標
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
    }

    // Update is called once per frame
    void Update()
    {
        //クールタイムにTime.deltaTimeを足す
        coolTimeL += Time.deltaTime;
        coolTimeR += Time.deltaTime;

        //関数PlayControlを呼び出す
        PlayControl();             

        //移動後のビューポート座標値を取得
        viewX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面X座標
        viewY = Camera.main.WorldToViewportPoint(this.transform.position).y;//画面Y座標

        //移動可能な画面範囲指定
        //-X座標
        if (viewX >= 0)
        {
            backward = true;
        }
        else
        {
            backward = false;
        }
        //+X座標
        if (viewX <= 1)
        {
            forward = true;
        }
        else
        {
            forward = false;
        }
        //-Y座標
        if (viewY >= 0)
        {
            down = true;
        }
        else
        {
            down = false;
        }
        //+Y座標
        if (viewY <= 1)
        {
            up = true;
        }
        else
        {
            up = false;
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

    //操作入力
    void PlayControl()
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
            if (Input.GetMouseButton(0) && coolTimeL > attackL)
            {
                Instantiate(forwardBullet, this.transform.position, Quaternion.identity);
                coolTimeL = 0.0f;
            }
            //落下攻撃
            if (Input.GetMouseButton(1) && coolTimeR > attackR)
            {
                Instantiate(downBullet, this.transform.position, Quaternion.identity);
                coolTimeR = 0.0f;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                
            }
        }
    }

    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //タグEnemyの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Enemy")
        {
            Damage();//関数Damageを呼び出す
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
}