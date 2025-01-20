using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteBase : MonoBehaviour
{
    //ステータス
    public float speed;//移動速度
    //処理
    public bool isDamage;//ダメージの可否
    //座標
    public Vector3 mousePosition, worldPosition, viewPortPosition;
    //このオブジェクトのコンポーネント
    public Transform thisTransform;                            //"Transform"
    public Rigidbody rigidBody;                                //"Rigidbody"
    public BoxCollider boxCollider;                            //"BoxCollider"
    public CapsuleCollider capsuleCollider;                    //"CapsuleCollider"
    public Renderer[] thisRenderer;                            //"Renderer"
    public AudioClip damage;                                   //"AudioClip(ダメージ)"
    public Animator animator = null;                           //"Animator"
    public RuntimeAnimatorController runtimeAnimatorController;//"RuntimeAnimatorController"
    public AudioSource audioSource;                            //"AudioSource"
    //他のオブジェクトのコンポーネント
    public Transform playerTransform;                         //"Transform(プレイヤー)"

    //関数"GetComponent"
    public void GetComponent()
    {
        //このオブジェクトのコンポーネントを取得
        thisTransform = this.GetComponent<Transform>();
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        //"BoxCollider"が存在している場合
        if (TryGetComponent<BoxCollider>(out boxCollider))
        {
            boxCollider = this.gameObject.GetComponent<BoxCollider>();
        }
        //"CapsuleCollider"が存在している場合
        if (TryGetComponent<CapsuleCollider>(out capsuleCollider))
        {
            capsuleCollider = this.gameObject.GetComponent<CapsuleCollider>();
        }
        audioSource = this.GetComponent<AudioSource>();
        //他のオブジェクトのコンポーネントを取得
        playerTransform = GameObject.Find("Player").transform;
    }

    //関数"Destroy"
    public void Destroy()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }
}

public class PlayerBase : CharacteBase
{
    //ステータス
    public static int hp;       //体力
    public static int attack;   //攻撃力
    public static int remain;   //残機
    public static string status;//状態
    //処理
    public static float[] attackTimer = new float[2];       //攻撃タイマー([前方],[下方])
    public static float[] attackTimeInterval = new float[2];//攻撃時間間隔([前方],[下方])
    public static float gageTimer;                          //ゲージタイマー
    public static float gageTimeInterval;                   //ゲージ時間間隔
    public static int level;                                //レベル
    public static int exp;                                  //経験値
    public static int ally;                                 //味方数
    public float invincibleTimer = 0.0f;                    //無敵タイマー
    public float invincibleInterval = 10.0f;                //無敵持続時間
    public float blinkingTime = 1.0f;                       //点滅持続時間
    public float rendererSwitch = 0.05f;                    //Renderer切り替え時間
    public float rendererTimer;                             //Renderer切り替えの経過時間
    public float rendererTotalTime;                         //Renderer切り替えの合計経過時間
    public bool isObjRenderer;                              //objRendererの可否
    public float levelAttackInterval = 0.0f;                //レベルアップ時の攻撃間隔短縮
    public bool isAction = false;      //行動の可否
    public bool isAnimation = false;   //アニメーションの可否
    //このオブジェクトのコンポーネント
    public GameObject nowPlayer;                       //"GameObject(現在のプレイヤー)"
    public GameObject[] playerAlly = new GameObject[2];//"GameObject(プレイヤーの味方)"

    //関数"StartPlayer"
    public void StartPlayer()
    {
        //処理を初期化する
        gageTimer = 0.0f;
        gageTimeInterval = 20.0f;
        ally = 0;
        level = 1;
        exp = 0;
        //選択したプレイヤーのステータスを設定する
        hp = PlayerList.Player.hp[GameManager.playerNumber];                           //体力
        attack = PlayerList.Player.power[GameManager.playerNumber];                    //攻撃力
        attackTimer[0] = PlayerList.Player.attackInterval[0, GameManager.playerNumber];//攻撃タイマー[前方]
        attackTimer[1] = PlayerList.Player.attackInterval[1, GameManager.playerNumber];//攻撃タイマー[下方]
        status = "Normal";                                                             //プレイヤーの状態を"Normal"にする
        //ゲームの状態が"Menu"の場合
        if (GameManager.status == "Menu")
        {
            remain = 3;                 //残機
            GameManager.status = "Play";//ゲームの状態を"Play"にする
        }
    }

    public virtual void UpdatePlayer()
    {

    }

    //関数"DamagePlayer"
    public virtual void DamagePlayer()
    {
        //ダメージを"受けている"場合
        if (isDamage == true)
        {
            return;//返す
        }

        hp -= 1;//体力を"-1"する
    }

    //関数"DeathPlayer"
    public void DeathPlayer()
    {
        boxCollider.enabled = false;    //BoxColliderを"無効"にする
        rigidBody.useGravity = true;    //RigidBodyの重力を"有効"にする
        animator.SetBool("Death", true);//Animatorを"Death"にする
        hp = 0;                         //体力を"0"にする
        remain -= 1;                    //残機を"-1"する
    }
}

public class EnemyBase : CharacteBase
{
    //このオブジェクトのコンポーネント
    public AudioClip scream;                 //"AudioClip(叫び声)"
    //ステータス
    public int hp;       //体力
    public float jump;   //ジャンプ力
    public string status;//状態
    //処理
    public int nowAnimationNumber;           //現在のアニメーションの番号
    public string nowAnimationName;          //現在のアニメーションの名前
    public float nowAnimationLength;         //現在のアニメーションの長さ
    public bool isAction = false;            //行動の可否
    public float animationTimer = 0.0f;      //アニメーションタイマー
    public bool isAnimation = false;         //アニメーションの可否
    public HumanoidAnimation humanoidAnimation;
    public delegate void DirectionDelegate();//""
    public DirectionDelegate nowDirection;

    //関数"StartEnmey"
    public virtual void StartEnemy()
    {
        animator = this.GetComponent<Animator>();
        runtimeAnimatorController = animator.runtimeAnimatorController;
    }

    //関数"UpdateEnmey"
    public virtual void UpdateEnemy()
    {
        //このオブジェクトのビューポート座標を取得
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        //ビューポート座標が"0未満"の場合
        if (viewPortPosition.x < 0)
        {
            Destroy();//関数"Destroy"を実行
        }
    }

    //関数"Animation"
    public void AnimationPlay()
    {
        animator.SetInteger("Motion", nowAnimationNumber);//"animator(Motion)"に"nowAnimation"を設定して再生
    }

    //関数"Wait"
    public void Wait()
    {
        animationTimer += Time.deltaTime;//"animationTimer"に"Time.deltaTime(経過時間)"を足す

        foreach (AnimationClip clip in runtimeAnimatorController.animationClips)
        {
            if (clip.name == nowAnimationName)
            {
                nowAnimationLength = clip.length;
            }
        }

        if (animationTimer >= nowAnimationLength)
        {
            animationTimer = 0.0f;
            isAnimation = false;
            nowAnimationNumber = (int)HumanoidAnimation.Walk;
            nowAnimationName = HumanoidAnimation.Walk.ToString();
            AnimationPlay();//関数"Animation"を実行
        }
    }

    //関数"Enmey"
    public virtual void DamageEnemy()
    {
        hp -= PlayerBase.attack;

        //"hp > 0"の場合
        if (hp > 0)
        {
            isAnimation = true;                                    //"isAnimation = true"にする
            nowAnimationNumber = (int)HumanoidAnimation.Damage;
            nowAnimationName = HumanoidAnimation.Damage.ToString();
            audioSource.PlayOneShot(damage);                       //"damage"を鳴らす
            AnimationPlay();                                       //関数"Animation"を実行
        }
        //"hp <= 0"の場合
        else if (hp <= 0)
        {
            Invoke("DeathEnemy", 0.01f);//関数"DeathEnemy"を"0.01f"後に実行する
        }
    }

    //関数"Enmey"
    public virtual void DeathEnemy()
    {
        this.tag = "Untagged";                                //このタグを"Untagged"にする
        hp = 0;                                               //体力を"0"にする
        audioSource.PlayOneShot(scream);//"scream"を鳴らす
        GameManager.score += EnemyList.WalkEnemy.score;       //スコアを足す
        PlayerController.exp += EnemyList.WalkEnemy.exp;      //経験値を足す
        nowAnimationNumber = (int)HumanoidAnimation.Death;
        nowAnimationName = HumanoidAnimation.Death.ToString();    
        AnimationPlay();                                      //関数"Animation"を実行
    }

    //当たり判定(OnTriggerEnter)
    public virtual void OnTriggerEnter(Collider collision)
    {
        //衝突したオブジェクトの"tag == Player" && "isAnimation == false"の場合
        if (collision.gameObject.tag == "Player" && isAnimation == false)
        {
            isAnimation = true;//"isAnimation = true"にする
            //ランダム"10(パンチ)"～"12(キック)"
            nowAnimationNumber = (int)Random.Range((int)HumanoidAnimation.Punch,
                                                   (int)HumanoidAnimation.Kick + 1);
            humanoidAnimation = (HumanoidAnimation)nowAnimationNumber;
            nowAnimationName = humanoidAnimation.ToString();

            AnimationPlay();//関数"AnimationPlay"を実行する
        }
        //衝突したオブジェクトのタグが"Bullet" && "hp > 0"の場合
        if (collision.gameObject.tag == "Bullet" && hp > 0)
        {
            DamageEnemy();//関数"DamageEnemy"を実行する
        }
    }

    public enum HumanoidAnimation
    {
        Walk = 0,
        Run = 1,
        CrazyRun = 2,
        HaveGunIdle = 3,
        Punch = 10,
        Kick = 11,
        Dance  = 30,
        Damage = 31,
        Death  = 32
    }
}