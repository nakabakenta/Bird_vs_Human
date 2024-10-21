using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //�X�e�[�^�X�ϐ�
    public int hp;     //�̗�
    public float speed;//�ړ����x

    private CapsuleCollider capsuleCollider;//CapsuleCollider�ϐ�
    private Animator animator = null;       //Animator�ϐ�

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
            capsuleCollider.enabled = false;//CapsuleCollider�𖳌�������
            animator.SetBool("Death", true);//
        }
    }

    //�����蔻��(OnTriggerEnter)
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
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }
}
