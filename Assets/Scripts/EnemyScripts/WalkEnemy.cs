using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //ステータス
    private int hp = EnemyStatus.WalkEnemy.hp;        //体力
    private float speed = EnemyStatus.WalkEnemy.speed;//移動速度
    //
    private int random = 0;       //ランダム
    private float coolTime = 0.0f;//クールタイム
    private bool isAttack = false;//攻撃中かどうか

    //コンポーネント取得変数
    private Transform setTransform;  //Transform
    private Animator animator = null;//Animator

    // Start is called before the first frame update
    void Start()
    {
        setTransform = this.gameObject.GetComponent<Transform>();//このオブジェクトのTransformを取得
        animator = this.GetComponent<Animator>();                //このオブジェクトのAnimatorを取得
        animator.SetInteger("Motion", 0);                        //Animatorの"Motion, 0"(歩行モーション)を有効にする
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPosition = setTransform.localPosition;//オブジェクトの
        Vector3 localAngle = setTransform.localEulerAngles;//
        localPosition.y = 0.0f;//
        localPosition.z = 0.0f;//
        localAngle.y = EnemyStatus.rotationY;//
        setTransform.localPosition = localPosition;       //ローカル座標での座標を設定
        setTransform.localEulerAngles = localAngle;       //

        //
        if (hp > 0 && isAttack == false)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//左方向に移動する
        }
        //
        else if (PlayerController.hp <= 0)
        {
            animator.SetInteger("Motion", 3);//Animatorの"Motion, 3"(ダンスモーション)を有効にする
        }
        //
        if (PlayerController.hp > 0 && isAttack == true)
        {
            coolTime += Time.deltaTime;//クールタイムにTime.deltaTimeを足す

            //ランダムが"1"だったら
            if (random == 1)
            {
                //
                if (coolTime >= 2.0f)
                {
                    coolTime = 0.0f;                 //
                    animator.SetInteger("Motion", 0);//
                    isAttack = false;
                }
            }
            //ランダムが"2"だったら
            else if (random == 2)
            {
                //
                if (coolTime >= 1.5f)
                {
                    coolTime = 0.0f;
                    animator.SetInteger("Motion", 0);//
                    isAttack = false;
                }
            }
        }
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //タグPlayerの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Player")
        {
            Animation();
        }
        //タグBulletの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Bullet" && this.tag != "Death")
        {
            Damage();//関数Damageを呼び出す
        }
        //タグDeleteの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Delete")
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }

    //アニメーション関数
    void Animation()
    {
        if (isAttack)
        {
            return;
        }

        isAttack = true;
        random = (int)Random.Range(1, 3);           //ランダム処理(1～2)
        animator.SetInteger("Motion", random);//AnimatorのAttackMotion(1～2)を有効化する
        Debug.Log(random);                          //Debug.Log(random)
    }

    //ダメージ判定関数
    void Damage()
    {
        hp -= 1;//体力を-1する

        //体力が0以下だったら
        if (hp <= 0)
        {
            GameManager.score += EnemyStatus.WalkEnemy.score;//
            this.tag = "Death";              //タグをDeathに変更する
            animator.SetInteger("Motion", 4);//
        }
    }
}
