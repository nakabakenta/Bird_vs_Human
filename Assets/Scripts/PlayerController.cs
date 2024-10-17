using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //���@�X�e�[�^�X
    public int hp;   //���@�̗̑�
    public int power;//���@�̍U����
    public int speed;//���@�̈ړ����x

    private float coolTime = 0.25f;//�U���̊Ԋu�������邽�߂̕ϐ�
    private float spanTime = 0.25f;//�U�����o��܂ł̊Ԋu
    

    public GameObject mainBullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coolTime += Time.deltaTime;//

        //��ړ�
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += speed * transform.up * Time.deltaTime;
        }
        //�E�ړ�
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position -= speed * transform.forward * Time.deltaTime;
        }
        //���ړ�
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= speed * transform.up * Time.deltaTime;
        }
        //���ړ�
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;
        }
        //��X�g���̂ł����Ă���
        //if (Input.GetKeyDown(KeyCode.E))
        //{

        //}
        //�U������
        if (Input.GetMouseButton(0) && coolTime > spanTime)
        {
            Instantiate(mainBullet, this.transform.position, Quaternion.identity);
            coolTime = 0.0f;
        }
    }

    void Shot()
    {
        
    }
}
