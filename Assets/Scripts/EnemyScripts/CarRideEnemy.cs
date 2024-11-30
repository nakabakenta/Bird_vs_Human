using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRideEnemy : MonoBehaviour
{
    //ステータス
    private int hp = EnemyList.CarRideEnemy.hp;        //体力
    private float speed = EnemyList.CarRideEnemy.speed;//移動速度
    private float jump = EnemyList.CarRideEnemy.jump;  //ジャンプ力
    //処理
    private float interval = 0.0f;//間隔
    private float viewPointX;     //ビューポイント座標.X
    //アニメーション
    private int nowAnimation;        //現在のアニメーション
    private bool isAnimation = false;//アニメーションの可否
    //このオブジェクトのコンポーネント
    private Transform thisTransform;  //"Transform"(このオブジェクト)
    private Animator animator = null; //"Animator"(このオブジェクト)
    //他のオブジェクトのコンポーネント
    private Transform playerTransform;//"Transform"(プレイヤー)

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.gameObject.GetComponent<Transform>();//このオブジェクトの"Transform"を取得
        animator = this.GetComponent<Animator>();                 //このオブジェクトの"Animator"を取得
        playerTransform = GameObject.Find("Player").transform;    //ゲームオブジェクト"Player"を探して"Transform"を取得
        nowAnimation = EnemyList.HumanoidAnimation.carExit;
        Animation();
    }

    // Update is called once per frame
    void Update()
    {
        //このオブジェクトのビューポート座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//ビューポート座標.X

        //体力が0より上 && ビューポート座標.Xが1より上であれば
        if (hp > 0 && viewPointX < 1)
        {
            Behavior();//行動関数を呼び出す
        }
        //体力が0以下 && ビューポート座標.Xが0未満であれば
        else if (hp <= 0 && viewPointX < 0)
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }

    //行動関数
    void Behavior()
    {
        if(nowAnimation == EnemyList.HumanoidAnimation.carExit)
        {
            Wait();
        }
        else if(nowAnimation != EnemyList.HumanoidAnimation.carExit)
        {
            Vector3 localPosition = thisTransform.localPosition;//
            Vector3 localAngle = thisTransform.localEulerAngles;//

            if (this.transform.position.x + EnemyList.RunEnemy.rangeX > playerTransform.position.x &&
                this.transform.position.x - EnemyList.RunEnemy.rangeX < playerTransform.position.x &&
                this.transform.position.y + EnemyList.RunEnemy.rangeY < playerTransform.position.y &&
                this.transform.position.y == 0.0f && nowAnimation == EnemyList.HumanoidAnimation.run &&
                isAnimation == false)
            {
                isAnimation = true;
                nowAnimation = EnemyList.HumanoidAnimation.jump;
                Animation();
            }

            if (nowAnimation == EnemyList.HumanoidAnimation.run)
            {
                localPosition.y = 0.0f;//
            }

            localPosition.z = playerTransform.position.z;//

            //
            if (this.transform.position.x > playerTransform.position.x)
            {
                localAngle.y = -EnemyList.rotationY;//
            }
            //
            else if (this.transform.position.x < playerTransform.position.x)
            {
                localAngle.y = EnemyList.rotationY;//
            }

            thisTransform.localPosition = localPosition;//ローカル座標での座標を設定
            thisTransform.localEulerAngles = localAngle;//

            //
            if (nowAnimation == EnemyList.HumanoidAnimation.run)
            {
                this.transform.position += speed * transform.forward * Time.deltaTime;//前方向に移動する
            }
            //
            else if (nowAnimation != EnemyList.HumanoidAnimation.run && isAnimation == true)
            {
                Wait();
            }

            //
            if (PlayerController.hp <= 0 && isAnimation == false)
            {
                nowAnimation = EnemyList.HumanoidAnimation.dance;
                Animation();//アニメーション関数を実行
            }
        }
    }

    //アニメーション関数
    void Animation()
    {
        animator.SetInteger("Motion", nowAnimation);//"animator(Motion)"に"nowAnimation"を設定する
        Debug.Log(nowAnimation);//"Debug.Log(nowAnimation)"
    }

    //待機関数
    void Wait()
    {
        interval += Time.deltaTime;//クールタイムにTime.deltaTimeを足す

        if (nowAnimation == EnemyList.HumanoidAnimation.punch ||
            nowAnimation == EnemyList.HumanoidAnimation.kick)
        {
            //
            if (nowAnimation == EnemyList.HumanoidAnimation.punch)
            {
                //
                if (interval >= 2.0f)
                {
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.run;
                    Animation();
                }
            }
            //
            else if (nowAnimation == EnemyList.HumanoidAnimation.kick)
            {
                //
                if (interval >= 1.5f)
                {
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.run;
                    Animation();
                }
            }
        }
        //
        else if (nowAnimation == EnemyList.HumanoidAnimation.jump)
        {
            if (interval >= 0.75f)
            {
                this.transform.position += jump * transform.up * Time.deltaTime;

                if (interval >= 2.0f)
                {
                    animator.SetFloat("MoveSpeed", 1.0f);            //"animator(MoveSpeed)"を"1.0f(再生)"にする
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyList.HumanoidAnimation.run;
                    Animation();
                }
                else if (interval >= 1.0f)
                {
                    animator.SetFloat("MoveSpeed", 0.0f);            //"animator(MoveSpeed)"を"0.0f(停止)"にする
                }
            }
        }
        else if(nowAnimation == EnemyList.HumanoidAnimation.carExit)
        {
            //
            if (interval >= 3.0f)
            {
                interval = 0.0f;
                isAnimation = false;
                nowAnimation = EnemyList.HumanoidAnimation.run;
                Animation();
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
            Death();//関数"Death"死亡を呼び出す
        }
    }

    //死亡関数
    void Death()
    {
        hp = 0;                                            //体力を"0"にする
        this.thisTransform.position = new Vector3(this.thisTransform.position.x, 0.0f, this.thisTransform.position.z);//
        GameManager.score += EnemyList.WalkEnemy.score;  //
        this.tag = "Death";                                //タグを"Death"に変更する
        nowAnimation = EnemyList.HumanoidAnimation.death;
        Animation();
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトのタグが"Player" && "action"が"false"だったら
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;
            nowAnimation = (int)Random.Range(EnemyList.HumanoidAnimation.punch, EnemyList.HumanoidAnimation.kick + 1);//ランダム"10(パンチ)"～"12(キック)"
            Animation();
        }
        //タグBulletの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            Damage();//関数Damageを呼び出す
        }
    }
}
