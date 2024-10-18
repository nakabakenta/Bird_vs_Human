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
        this.transform.position += speed * transform.forward * Time.deltaTime;
    }

    //��ʊO����
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);//����
    }
}
