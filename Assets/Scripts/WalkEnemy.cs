using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkEnemy : MonoBehaviour
{
    //�X�e�[�^�X�ϐ�
    public int hp;     //�̗�
    public float speed;//�ړ����x

    private Animator animator = null;//�A�j���[�^�[

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //
        this.transform.position += speed * transform.forward * Time.deltaTime;
        //
        if (hp <= 0)
        {
            animator.SetBool("Death", true);
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
