using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //���@�X�e�[�^�X
    public int hp;   //���@�̗̑�
    public int power;//���@�̍U����
    public int speed;//���@�̍U�����x
    public int cost; //���@�̒ǉ�������莞�̏����c��

    private int moveSpeed;        //���@�̏����ړ����x
    private int acceleration = 10;//���@�̉������l

    public GameObject mainBullet;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        //��ړ�
        if(Input.GetKey(KeyCode.W))
        {
            this.transform.position += moveSpeed * transform.up * Time.deltaTime;
        }
        //�E�ړ�
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position -= moveSpeed * transform.forward * Time.deltaTime;
        }
        //���ړ�
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position -= moveSpeed * transform.up * Time.deltaTime;
        }
        //���ړ�
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += moveSpeed * transform.forward * Time.deltaTime;
        }
        //����
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(moveSpeed < 60)
            {
                moveSpeed += acceleration;
            }
        }
        //����
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (moveSpeed > 20)
            {
                moveSpeed -= acceleration;
            }
        }
        //�U������
        if(Input.GetMouseButton(0))
        {
            Instantiate(mainBullet, this.transform.position, Quaternion.identity);
        }
    }

    void Shot()
    {
        
    }
}
