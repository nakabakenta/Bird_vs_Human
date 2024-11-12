using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunEnemy : MonoBehaviour
{
    //ステータス
    private int hp = EnemyStatus.RunEnemy.hp;        //体力
    private float speed = EnemyStatus.RunEnemy.speed;//移動速度
    private float jump = EnemyStatus.RunEnemy.jump;  //ジャンプ力
    //処理
    private int random = 0;       //ランダム
    private float interval = 0.0f;//間隔
    private float abc = 0.0f;//間隔

    private string nowAction;     //現在の動作
    private bool action = false;  //動作フラグ



    private float viewPointX;     //ビューポイント座標.X
    //コンポーネント
    private Transform setTransform;   //Transform
    private Transform playerTransform;//Transform(プレイヤー)
    private Rigidbody rigidBody;
    private Animator animator = null; //Animator

    // Start is called before the first frame update
    void Start()
    {
        setTransform = this.gameObject.GetComponent<Transform>();//このオブジェクトのTransformを取得
        animator = this.GetComponent<Animator>();                //このオブジェクトのAnimatorを取得
        animator.SetInteger("Motion", 0);                        //Animatorの"Motion 0"(走る)を有効にする
        playerTransform = GameObject.Find("Player").transform;
        nowAction = "Run";
        rigidBody = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //移動後のビューポート座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面座標.X

        //体力が0より上 && ビューポート座標.Xが1より上であれば
        if (hp > 0 && viewPointX < 1)
        {
            Behavior();//
        }
        //体力が0以下 && ビューポート座標.Xが0未満であれば
        else if (hp <= 0 && viewPointX < 0)
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }

    void Behavior()
    {
        Vector3 localPosition = setTransform.localPosition;//オブジェクトの
        Vector3 localAngle = setTransform.localEulerAngles;//

        if (this.transform.position.y + 2.0f < playerTransform.position.y &&
            this.transform.position.x + 0.5f > playerTransform.position.x &&
            this.transform.position.x - 0.5f < playerTransform.position.x &&
            nowAction == "Run" && action == false && this.transform.position.y == 0.0f)
        {
            action = true;
            nowAction = "jump";
            Animation();
        }

        if(nowAction == "Run")
        {
            localPosition.y = 0.0f;//

            //this.transform.position -= jump * transform.up * Time.deltaTime;
        }

        localPosition.z = 0.0f;//

        //
        if (this.transform.position.x > playerTransform.position.x)
        {
            localAngle.y = -EnemyStatus.rotationY;//
        }
        //
        else if (this.transform.position.x < playerTransform.position.x)
        {
            localAngle.y = EnemyStatus.rotationY;//
        }

        setTransform.localPosition = localPosition;//ローカル座標での座標を設定
        setTransform.localEulerAngles = localAngle;//

        //
        if (nowAction == "Run")
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する
        }
        //
        else if (nowAction != "Run" && action == true)
        {
            Wait();
        }

        //
        if (PlayerController.hp <= 0 && action == false)
        {
            Animation();//アニメーション関数を実行
        }
    }

    //アニメーション関数
    void Animation()
    {
        if (PlayerController.hp > 0 && nowAction == "Attack")
        {
            random = (int)Random.Range(1, 3);     //ランダム処理(1～2)
            animator.SetInteger("Motion", random);//AnimatorのAttackMotion(1～2)を有効にする
            Debug.Log(random);                    //Debug.Log(random)
        }
        else if(PlayerController.hp > 0 && nowAction == "jump")
        {
            animator.SetInteger("Motion", 10);
        }
        else if (PlayerController.hp <= 0)
        {
            nowAction = "Dance";
            animator.SetInteger("Motion", 3);//AnimatorのMotion 3(ダンスモーション)を有効にする
        }
    }

    //待機関数
    void Wait()
    {
        interval += Time.deltaTime;//クールタイムにTime.deltaTimeを足す
        abc += Time.deltaTime;

        if (nowAction == "Attack")
        {
            //
            if (random == 1)
            {
                //
                if (interval >= 2.0f)
                {
                    interval = 0.0f;                 //
                    animator.SetInteger("Motion", 0);//
                    action = false;
                    nowAction = "Run";
                }
            }
            //
            else if (random == 2)
            {
                //
                if (interval >= 1.5f)
                {
                    interval = 0.0f;
                    animator.SetInteger("Motion", 0);//
                    action = false;
                    nowAction = "Run";
                }
            }
        }
        //
        else if(nowAction == "jump")
        {
            if(abc >= 0.75f)
            {
                this.transform.position += jump * transform.up * Time.deltaTime;

                //
                if (interval >= 2.0f)
                {
                    abc = 0.0f;
                    interval = 0.0f;
                    animator.SetInteger("Motion", 0);//
                    action = false;
                    nowAction = "Run";
                }
            }
            
            
        }
    }

    //ダメージ関数
    void Damage()
    {
        hp -= 1;//体力を"-1"する

        //体力が0以下だったら
        if (hp <= 0)
        {
            Death();
        }
    }

    //死亡関数
    void Death()
    {
        hp = 0;                                         //体力を"0"にする
        GameManager.score += EnemyStatus.RunEnemy.score;//
        this.tag = "Death";                             //タグを"Death"に変更する
        animator.SetInteger("Motion", 4);               //
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //タグPlayerの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Player" && action == false)
        {
            action = true;
            nowAction = "Attack";
            Animation();
        }
        //タグBulletの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//関数Damageを呼び出す
        }
    }
}
