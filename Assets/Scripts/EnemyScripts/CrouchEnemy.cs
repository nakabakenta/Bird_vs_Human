using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchEnemy : MonoBehaviour
{
    //ステータス
    private int hp = EnemyStatus.CrouchEnemy.hp;//体力
    //処理
    private float interval = 0.0f;//間隔
    private float viewPointX;     //ビューポイント座標.X
    //アニメーション
    private int nowAnimation;        //現在のアニメーション
    private bool isAnimation = false;//アニメーションの可否
    //このオブジェクトのコンポーネント
    private Transform thisTransform; //"Transform"
    private Animator animator = null;//"Animator"

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.gameObject.GetComponent<Transform>();//このオブジェクトの"Transform"を取得
        animator = this.GetComponent<Animator>();                 //このオブジェクトの"Animatorを取得
        nowAnimation = EnemyStatus.HumanoidAnimation.crouch;
        Animation();
    }

    // Update is called once per frame
    void Update()
    {
        //ビューポート座標を取得
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//画面座標.X

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
    void Behavior()
    {
        Vector3 localPosition = thisTransform.localPosition;//オブジェクトの
        Vector3 localAngle = thisTransform.localEulerAngles;//
        localPosition.y = 0.0f;//
        localPosition.z = 1.0f;//
        localAngle.y = -EnemyStatus.rotationY;//
        thisTransform.localPosition = localPosition;       //ローカル座標での座標を設定
        thisTransform.localEulerAngles = localAngle;       //

        //
        if (nowAnimation != EnemyStatus.HumanoidAnimation.crouch && isAnimation == true)
        {
            Wait();
        }

        //
        if (PlayerController.hp <= 0 && isAnimation == false)
        {
            nowAnimation = EnemyStatus.HumanoidAnimation.dance;
            Animation();//アニメーション関数を実行
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

        if (nowAnimation == EnemyStatus.HumanoidAnimation.punch ||
            nowAnimation == EnemyStatus.HumanoidAnimation.kick)
        {
            //
            if (nowAnimation == EnemyStatus.HumanoidAnimation.punch)
            {
                //
                if (interval >= 2.0f)
                {
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyStatus.HumanoidAnimation.crouch;
                    Animation();
                }
            }
            //
            else if (nowAnimation == EnemyStatus.HumanoidAnimation.kick)
            {
                //
                if (interval >= 1.5f)
                {
                    interval = 0.0f;
                    isAnimation = false;
                    nowAnimation = EnemyStatus.HumanoidAnimation.crouch;
                    Animation();
                }
            }
        }
    }

    //ダメージ判定関数
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
        GameManager.score += EnemyStatus.WalkEnemy.score;  //
        this.tag = "Death";                                //タグを"Death"に変更する
        nowAnimation = EnemyStatus.HumanoidAnimation.death;
        Animation();
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //タグPlayerの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;
            nowAnimation = (int)Random.Range(EnemyStatus.HumanoidAnimation.punch, EnemyStatus.HumanoidAnimation.kick + 1);//ランダム"10(パンチ)"～"12(キック)"
            Animation();
        }
        //タグBulletの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Bullet" && this.tag != "Death")
        {
            Damage();//関数Damageを呼び出す
        }
    }
}
