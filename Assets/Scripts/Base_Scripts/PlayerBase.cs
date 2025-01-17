using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    //ステータス
    public static int hp;    //体力
    public int attack;       //攻撃力
    public float speed;      //移動速度
    public static int remain;//残機
    public string status;    //状態
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
    public bool isDamage;              //ダメージの可否
    //座標
    public Vector3 mousePosition, worldPosition, viewPortPosition;
    //このオブジェクトのコンポーネント
    public Transform thisTransform;                            //"Transform"
    public Rigidbody rigidBody;                                //"Rigidbody"
    public BoxCollider boxCollider;                            //"BoxCollider"
    public CapsuleCollider capsuleCollider;                    //"CapsuleCollider"
    public Renderer[] thisRenderer;                            //"Renderer"
    public AudioClip damage;                                   //"AudioClip(ダメージ)"
    public AudioClip scream;                                   //"AudioClip(叫び声)"
    public Animator animator = null;                           //"Animator"
    public RuntimeAnimatorController runtimeAnimatorController;//"RuntimeAnimatorController"
    public AudioSource audioSource;                            //"AudioSource"
    //他のオブジェクトのコンポーネント
    public Transform playerTransform;                         //"Transform(プレイヤー)"

    //関数"GetComponent"
    public virtual void GetComponent()
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
        thisRenderer = this.gameObject.GetComponentsInChildren<Renderer>();
        animator = this.GetComponent<Animator>();
        runtimeAnimatorController = animator.runtimeAnimatorController;
        audioSource = this.GetComponent<AudioSource>();
        //他のオブジェクトのコンポーネントを取得
        playerTransform = GameObject.Find("Player").transform;
    }

    //関数"Damage"
    public virtual void Damage()
    {
        hp -= 1;//体力を"-1"する
    }

    //関数"Death"
    public virtual void Death()
    {
        hp = 0;//体力を"0"にする
    }

    //関数"Destroy"
    public virtual void Destroy()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }
}
