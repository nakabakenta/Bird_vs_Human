using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    //ステータス
    public float moveSpeed;//弾の移動速度
    //座標
    protected Vector3 viewPortPosition;
    //処理
    protected Vector3 direction;//オブジェクトの方向
    //このオブジェクトのコンポーネント
    public GameObject effect;               //"GameObject(エフェクト)"
    public AudioClip audioClip;             //"audioClip"
    protected AudioSource audioSource;      //"AudioSource"
    private BoxCollider boxCollider;        //"BoxCollider"
    private CapsuleCollider capsuleCollider;//"CapsuleCollider"
    //他のオブジェクトのコンポーネント
    protected Transform playerTransform;//"Transform(プレイヤー)"

    public void BaseStart()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();

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

        if (this.tag == "EnemyBullet")
        {
            //他のオブジェクトのコンポーネントを取得
            playerTransform = GameObject.Find("Player").transform;//"Transform(プレイヤー)"
        }
    }

    public void BaseUpdate()
    {
        //このオブジェクトのワールド座標をビューポート座標に変換して取得する
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;
        viewPortPosition.y = Camera.main.WorldToViewportPoint(this.transform.position).y;

        if (viewPortPosition.x < 0 || viewPortPosition.x > 1 || 
            viewPortPosition.y < 0)
        {
            Destroy();
        }
    }

    //関数"Destroy"
    virtual public void Destroy()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }

    //衝突判定(OnTriggerEnter)
    virtual public void OnTriggerEnter(Collider collision)
    {
        if (this.tag == "PlayerBullet" && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy"))
        {
            Destroy();
        }
        else if (this.tag == "EnemyBullet" && collision.gameObject.tag == "Player")
        {
            Destroy();
        }
    }
}