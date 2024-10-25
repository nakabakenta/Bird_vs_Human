using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //ステータス変数
    public int hp;     //体力
    public float speed;//移動速度

    private int random = 0;       //
    private float coolTime = 0.0f;//クールタイム
    private bool isAnimation = false;

    //コンポーネント取得変数
    private Transform setTransform;  //Transform変数
    private Animator animator = null;//Animator変数
    

    // Start is called before the first frame update
    void Start()
    {
        setTransform = this.gameObject.GetComponent<Transform>();//このオブジェクトのTransformを取得
        animator = this.GetComponent<Animator>();                //このオブジェクトのAnimatorを取得
        animator.SetBool("Walk", true);                          //AnimatorのWalk(歩行モーション)を有効化する
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPosition = setTransform.localPosition;//オブジェクトの
        Vector3 localAngle = setTransform.localEulerAngles;//
        localPosition.x = 0.0f;//
        localPosition.y = 0.0f;//
        localAngle.y = 180.0f; //
        setTransform.localPosition = localPosition;       //ローカル座標での座標を設定
        setTransform.localEulerAngles = localAngle;       //

        //
        if (hp > 0 && isAnimation == false)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//左方向に移動する
        }
        //
        else if (GameManager.playerHp <= 0)
        {
            animator.SetBool("Dance", true);//AnimatorのDance(ダンスモーション)を有効化する
        }
        //
        if (GameManager.playerHp > 0 && isAnimation == true)
        {
            coolTime += Time.deltaTime;//クールタイムにTime.deltaTimeを足す

            //
            if(random == 1)
            {
                //
                if (coolTime >= 2.0f)
                {
                    coolTime = 0.0f;                       //
                    animator.SetInteger("AttackMotion", 0);//
                    animator.SetBool("Walk", true);        //AnimatorのWalk(歩行モーション)を有効化する
                    isAnimation = false;
                }
            }
            //
            else if(random == 2)
            {
                //
                if (coolTime >= 1.5f)
                {
                    coolTime = 0.0f;
                    animator.SetInteger("AttackMotion", 0);//
                    animator.SetBool("Walk", true);        //AnimatorのWalk(歩行モーション)を有効化する
                    isAnimation = false;
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
        if (collision.gameObject.tag == "Bullet")
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
        if (isAnimation)
        {
            return;
        }

        isAnimation = true;
        random = (int)Random.Range(1, 3);           //ランダム処理(1～2)
        animator.SetBool("Walk", false);            //AnimatorのWalk(歩行モーション)を無効化する
        animator.SetInteger("AttackMotion", random);//AnimatorのAttackMotion(1～2)を有効化する
        Debug.Log(random);                          //Debug.Log(random)
    }

    //ダメージ判定関数
    void Damage()
    {
        hp -= 1;//体力を-1する

        //体力が0以下だったら
        if (hp <= 0)
        {
            this.tag = "Death";             //タグをDeathに変更する
            animator.SetBool("Walk", false);//AnimatorのWalk(歩行モーション)を無効化する
            animator.SetBool("Death", true);//AnimatorのDeath(死亡時のモーション)を有効化する
        }
    }
}
