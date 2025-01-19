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
    public void DestroyCharacte()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }
}

public class PlayerBase : CharacteBase
{
    //ステータス
    public static int hp;       //体力
    public int attack;          //攻撃力
    public static int remain;   //残機
    public static string status;//状態
    //処理
    public static float[] attackTimer = new float[2];   //攻撃タイマー([前方],[下方])
    public static float[] attackInterval = new float[2];//攻撃間隔([前方],[下方])
    public static float gageTimer;                      //ゲージタイマー
    public static float gageInterval;                   //ゲージ蓄積時間
    public static int level;                            //レベル
    public static int exp;                              //経験値
    public static int ally;                             //味方数
    public float invincibleTimer = 0.0f;                //無敵タイマー
    public float invincibleInterval = 10.0f;            //無敵持続時間
    public float blinkingTime = 1.0f;                   //点滅持続時間
    public float rendererSwitch = 0.05f;                //Renderer切り替え時間
    public float rendererTimer;                         //Renderer切り替えの経過時間
    public float rendererTotalTime;                     //Renderer切り替えの合計経過時間
    public bool isObjRenderer;                          //objRendererの可否
    public float levelAttackInterval = 0.0f;            //レベルアップ時の攻撃間隔短縮
    public bool isAction = false;      //行動の可否
    public bool isAnimation = false;   //アニメーションの可否
    //このオブジェクトのコンポーネント
    public GameObject nowPlayer;                       //"GameObject(現在のプレイヤー)"
    public GameObject[] playerAlly = new GameObject[2];//"GameObject(プレイヤーの味方)"

    //関数"PlayerGetComponent"
    public virtual void StartPlayer()
    {
        base.GetComponent();
        //処理を初期化する
        gageTimer = 0.0f;
        gageInterval = 20.0f;
        ally = 0;
        level = 1;
        exp = 0;
        //選択したプレイヤーのステータスを設定する
        hp = PlayerList.Player.hp[GameManager.playerNumber];                           //体力
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
    //ステータス
    public int hp;       //体力
    public float jump;   //ジャンプ力
    public string status;//状態
    //処理
    public bool isAction = false;      //行動の可否
    public float animationTimer = 0.0f;//アニメーションタイマー
    public bool isAnimation = false;   //アニメーションの可否
    //このオブジェクトのコンポーネント
    public AudioClip scream;//"AudioClip(叫び声)"

    //関数"StartEnmey"
    public virtual void StartEnemy()
    {
        base.GetComponent();
        animator = this.GetComponent<Animator>();
        runtimeAnimatorController = animator.runtimeAnimatorController;
    }

    //関数"UpdateEnmey"
    public virtual void UpdateEnemy()
    {

    }

    //関数"Enmey"
    public virtual void DamageEnemy()
    {
        hp -= 1;//体力を"-1"する
    }

    //関数"Enmey"
    public virtual void DeathEnemy()
    {
        hp = 0;//体力を"0"にする
        audioSource.PlayOneShot(scream);//"scream"を鳴らす
    }
}