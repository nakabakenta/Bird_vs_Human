using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //ステータス変数
    public int hp;     //体力
    public float speed;//移動速度

    private CapsuleCollider capsuleCollider;//CapsuleCollider変数
    private Animator animator = null;       //Animator変数

    // Start is called before the first frame update
    void Start()
    {
        capsuleCollider = this.gameObject.GetComponent<CapsuleCollider>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //
        if(hp > 0)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;//
        }
        //
        else if (hp <= 0)
        {
            capsuleCollider.enabled = false;//CapsuleColliderを無効化する
            animator.SetBool("Death", true);//
        }
    }

    //当たり判定(OnTriggerEnter)
    void OnTriggerEnter(Collider collider)
    {
        //
        if (collider.gameObject.tag == "Bullet")
        {
            hp = 0;//
        }

        //
        if (collider.gameObject.tag == "Delete")
        {
            Destroy(this.gameObject);//このオブジェクトを消す
        }
    }
}
