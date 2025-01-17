using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteBase : MonoBehaviour
{
    //ステータス
    public int hp;       //体力
    public int attack;   //攻撃力
    public float speed;  //移動速度
    public string status;//状態
    //処理
    public bool isAction = false;      //行動の可否
    public float animationTimer = 0.0f;//アニメーションタイマー
    public bool isAnimation = false;   //アニメーションの可否
    public bool isDamage;              //ダメージの可否
    //座標
    public Vector3 mousePosition, worldPosition, viewPortPosition;
    //このオブジェクトのコンポーネント
    public Transform thisTransform;                            //"Transform"
    public Rigidbody rigidBody;                                //"Rigidbody"
    public BoxCollider boxCollider;                            //"BoxCollider"
    public CapsuleCollider capsuleCollider;                    //"CapsuleCollider"
    public AudioClip damage;                                   //"AudioClip(ダメージ)"
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
        boxCollider = this.gameObject.GetComponent<BoxCollider>();
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
