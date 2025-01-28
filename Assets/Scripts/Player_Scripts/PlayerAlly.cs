using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlly : CharacteBase
{
    //�I�u�W�F�N�g
    public GameObject[] ally = new GameObject[3];//���ԃI�u�W�F�N�g

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        Instantiate(ally[GameManager.selectPlayer], this.transform.position, this.transform.rotation, thisTransform);
    }

    // Update is called once per frame
    void Update()
    {
        //���̃I�u�W�F�N�g�̃��[���h���W���r���[�|�[�g���W�ɕϊ����Ď擾����
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        if (viewPortPosition.x < 0)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Player"�̏ꍇ
        if (collision.gameObject.tag == "Player" && PlayerBase.ally < 2)
        {
            Destroy();//�֐�"Destroy"�����s����
        }
    }
}