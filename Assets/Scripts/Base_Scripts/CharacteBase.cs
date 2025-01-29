using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteBase : MonoBehaviour
{
    //ステータス
    public int hp;       //体力
    public float speed;  //移動速度
    //処理
    public float rotation;
    public bool isDamage;//ダメージの可否
    //座標
    public Vector3 worldPosition, viewPortPosition;
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
    public Transform playerTransform;                          //"Transform(プレイヤー)"

    //関数"GetComponent"
    public void GetComponent()
    {
        //このオブジェクトのコンポーネントを取得
        thisTransform = this.gameObject.GetComponent<Transform>();
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
        audioSource = this.gameObject.GetComponent<AudioSource>();
        //他のオブジェクトのコンポーネントを取得
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }

    //関数"Destroy"
    public void Destroy()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }
}