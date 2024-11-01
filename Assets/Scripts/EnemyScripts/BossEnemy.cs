using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    //ステータス
    private int hp = EnemyStatus.BossEnemy.hp[0];        //体力
    private float speed = EnemyStatus.BossEnemy.speed[0];//移動速度
    //処理
    private int random = 0;       //ランダム
    private float waitTime = 0.0f;//待機時間
    private bool attack = false;  //攻撃フラグ
    private float viewPointX;     //ビューポイント座標.X
    //コンポーネント
    private Transform setTransform;   //Transform
    private Transform playerTransform;//Transform(プレイヤー)
    private Animator animator = null; //Animator

    // Start is called before the first frame update
    void Start()
    {
        setTransform = this.gameObject.GetComponent<Transform>();//このオブジェクトのTransformを取得
        animator = this.GetComponent<Animator>();                //このオブジェクトのAnimatorを取得
        animator.SetInteger("Motion", 0);                        //Animatorの"Motion 0"(走る)を有効にする
        playerTransform = GameObject.Find("Chickadee_Player").transform;//
    }

    // Update is called once per frame
    void Update()
    {
        //移動後のビューポート座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面座標.X

        //
        if (viewPointX < 1)
        {
            Behavior();
        }
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //タグPlayerの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Player" && attack == false)
        {
            Animation();
        }
        //タグBulletの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Bullet" && this.tag != "Death")
        {
            Damage();//関数Damageを呼び出す
        }
    }

    //動作関数
    void Behavior()
    {
        Vector3 localPosition = setTransform.localPosition;//オブジェクトの
        Vector3 localAngle = setTransform.localEulerAngles;//

        localPosition.y = 0.0f;//
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
        if(PlayerController.hp > 0)
        {
            //
            if (attack == false)
            {
                this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する
            }
        }
        //
        else if(PlayerController.hp <= 0)
        {
            Animation();//アニメーション関数を実行
        }
        //
        if (attack == true)
        {
            Wait();
        }
        //体力が0以下 && ビューポート座標.Xが0未満であれば
        if (hp <= 0 && viewPointX < 0)
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }

    //アニメーション関数
    void Animation()
    {
        if(PlayerController.hp > 0)
        {
            attack = true;
            random = (int)Random.Range(1, 3);     //ランダム処理(1～2)
            animator.SetInteger("Motion", random);//AnimatorのAttackMotion(1～2)を有効にする
            Debug.Log(random);                    //Debug.Log(random)
        }
        else if(PlayerController.hp <= 0)
        {
            animator.SetInteger("Motion", 3);//AnimatorのMotion 3(ダンスモーション)を有効にする
        }
    }

    //待機関数
    void Wait()
    {
        waitTime += Time.deltaTime;//クールタイムにTime.deltaTimeを足す
        //
        if (random == 1)
        {
            //
            if (waitTime >= 2.0f)
            {
                waitTime = 0.0f;                 //
                animator.SetInteger("Motion", 0);//
                attack = false;
            }
        }
        //
        else if (random == 2)
        {
            //
            if (waitTime >= 1.5f)
            {
                waitTime = 0.0f;
                animator.SetInteger("Motion", 0);//
                attack = false;
            }
        }
    }

    //ダメージ関数
    void Damage()
    {
        hp -= 1;//体力を-1する

        //体力が0以下だったら
        if (hp <= 0)
        {
            hp = 0;
            GameManager.score += EnemyStatus.BossEnemy.score[0];//
            this.tag = "Death";                                 //タグをDeathに変更する
            animator.SetInteger("Motion", 4);                   //
            Stage.BossEnemy[0] = false;
        }
    }
}
