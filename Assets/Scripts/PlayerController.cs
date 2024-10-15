using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //���@�X�e�[�^�X
    public int hp;   //���@�̗̑�
    public int power;//���@�̍U����
    public int speed;//���@�̈ړ����x

    private int coolTime;//���@�̍U���Ԋu

    public GameObject mainBullet;

    // Start is called before the first frame update
    void Start()
    {
        coolTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //��ړ�
        if(Input.GetKey(KeyCode.W))
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
        if (Input.GetMouseButton(0) && coolTime == 0)
        {
            Instantiate(mainBullet, this.transform.position, Quaternion.identity);
        }
        coolTime++;
    }

    void Shot()
    {
        
    }
}
