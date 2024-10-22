using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //ステータス変数
    public int hp;     //体力
    public float speed;//移動速度

    private float coolTime = 0.0f;//クールタイム
    private bool waik = true;    //歩行フラグ

    //コンポーネント取得変数
    private Animator animator = null;       //Animator変数

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();//このオブジェクトのAnimatorを取得
        animator.SetBool("Walk", true);//AnimatorのWalk(歩行モーション)を有効化する
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (hp > 0 && waik == true)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//左方向に移動する
        }
        //
        else if(PlayerController.hp <= 0)
        {
            waik = false;
            animator.SetBool("Dance", true);//AnimatorのDance(ダンスモーション)を有効化する
        }
        //
       if(waik == false)
        {
            coolTime += Time.deltaTime;//クールタイムにTime.deltaTimeを足す

            if(coolTime >= 3.5f)
            {
                coolTime = 0.0f;
                animator.SetBool("Attack", false);//AnimatorのAttack(攻撃モーション)を無効化する
                animator.SetBool("Walk", true);   //AnimatorのWalk(歩行モーション)を無効化する
                waik = true;
            }
        }
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //タグPlayerの付いたオブジェクトに衝突したら
        if (collision.gameObject.tag == "Player")
        {
            waik = false;
            animator.SetBool("Walk", false); //AnimatorのWalk(歩行モーション)を無効化する
            animator.SetBool("Attack", true);//AnimatorのAttack(攻撃モーション)を有効化する
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

    //ダメージ判定
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
