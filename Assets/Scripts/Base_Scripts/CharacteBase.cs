using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacteBase : MonoBehaviour
{
    //ステータス
    public int hp;             //体力
    public float moveSpeed;    //移動速度
    public Vector3 rotateSpeed;//回転速度
    //処理
    protected Vector3 direction;//オブジェクトの方向
    protected bool isDamage;    //ダメージの可否
    //座標
    protected Vector3 worldPosition, viewPortPosition;
    //このオブジェクトのコンポーネント
    public AudioClip damage;                                      //"AudioClip(ダメージ)"
    protected Transform thisTransform;                            //"Transform"
    protected Rigidbody rigidBody;                                //"Rigidbody"
    protected BoxCollider boxCollider;                            //"BoxCollider"
    protected CapsuleCollider capsuleCollider;                    //"CapsuleCollider"
    protected Renderer[] thisRenderer;                            //"Renderer"
    protected Animator animator = null;                           //"Animator"
    protected RuntimeAnimatorController runtimeAnimatorController;//"RuntimeAnimatorController"
    protected AudioSource audioSource;                            //"AudioSource"
    //他のオブジェクトのコンポーネント
    protected Transform playerTransform;                          //"Transform(プレイヤー)"

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

    public static class Characte
    {
        public enum Direction
        {
            Horizontal = 90,
            Vertical = 180,
        }
    }
}