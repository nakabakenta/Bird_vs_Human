using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //自機ステータス
    public int hp;     //体力
    public int power;  //攻撃力
    public float speed;//移動速度

    public GameObject bullet;//

    private float coolTime = 0.2f;//クールタイム
    private float spanTime = 0.2f;//攻撃が出るまでの間隔
    //ビューポート座標変数
    private float viewX;//ビューポートX座標
    private float viewY;//ビューポートY座標
    //移動フラグ
    private bool forward; //前移動
    private bool backward;//後移動
    private bool up;      //上移動
    private bool down;    //下移動
    //ダメージ関係変数
    private float blinkingTime = 1.0f;//点滅・無敵の持続時間
    private bool isDamage;            //ダメージを受けているかのフラグ
    private bool isObjRenderer;       //objRendererが有効か無効かのフラグ

    private Rigidbody rigidBody;     //Rigidbody変数
    private BoxCollider boxCollider; //CapsuleCollider変数
    private Animator animator = null;//Animator変数
    private Renderer[] objRenderer;  //Renderer配列

    //リセットする時の為にコルーチンを保持しておく
    Coroutine blinking;

    //ダメージ点滅の合計経過時間
    private float blinkingTotalElapsedTime;

    //ダメージ点滅のRendererの有効・無効切り替え用の経過時間
    float blinkingElapsedTime;

    //ダメージ点滅のRendererの有効・無効切り替え用のインターバル
    float blinkingInterval = 0.05f;

    void Awake()
    {
        objRenderer = GetComponentsInChildren<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //クールタイムにTime.deltaTimeを足す
        coolTime += Time.deltaTime;

        PlayControl();

        //移動後のビューポート座標値を取得
        viewX = Camera.main.WorldToViewportPoint(this.transform.position).x;
        viewY = Camera.main.WorldToViewportPoint(this.transform.position).y;
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

        //後々使うのでおいておく
        //if (Input.GetKeyDown(KeyCode.E))
        //{

        //}        
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
            //攻撃発射
            if (Input.GetMouseButton(0) && coolTime > spanTime)
            {
                Instantiate(bullet, this.transform.position, Quaternion.identity);
                coolTime = 0.0f;
            }
        }
    }


    //衝突判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //
        if (collision.gameObject.tag == "Enemy")
        {
            Damage();//
        } 
    }

    void SetObjRenderer(bool b)
    {
        for (int i = 0; i < objRenderer.Length; i++)
        {
            objRenderer[i].enabled = b;
        }
    }

    void Damage()
    {
        //ダメージ点滅中は二重に実行しない。
        if (isDamage)
            return;

        hp -= 1;//

        //体力が0以下だったら
        if (hp <= 0)
        {
            boxCollider.enabled = false;    //BoxColliderを無効化する
            animator.SetBool("Death", true);//
            rigidBody.useGravity = true;    //RigidBodyの重力を有効化する
        }
        StartFlicker();
    }

    void StartFlicker()
    {
        //isDamagedで多重実行を防いでいるので、ここで多重実行を弾かなくてもOK    
        blinking = StartCoroutine(Blinking());
    }

    IEnumerator Blinking()
    {
        isDamage = true;

        blinkingTotalElapsedTime = 0;
        blinkingElapsedTime = 0;

        while (true)
        {

            blinkingTotalElapsedTime += Time.deltaTime;
            blinkingElapsedTime += Time.deltaTime;

            if (blinkingInterval <= blinkingElapsedTime)
            {
                //被ダメージ点滅処理
                blinkingElapsedTime = 0;
                //Rendererの有効、無効の反転
                isObjRenderer = !isObjRenderer;
                SetObjRenderer(isObjRenderer);
            }

            if (blinkingTime <= blinkingTotalElapsedTime)
            {
                //被ダメージ点滅の終了時の処理
                isDamage = false;
                //最後に必ずRendererを有効化する
                isObjRenderer = true;
                SetObjRenderer(true);

                blinking = null;
                yield break;
            }

            yield return null;
        }
    }
}