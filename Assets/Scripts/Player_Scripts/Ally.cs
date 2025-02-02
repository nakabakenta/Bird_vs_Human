using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ally : PlayerBase
{
    //����
    private int allyNumber;        //�����ԍ�
    private bool sacrifice = false;
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject text;       //"GameObject(�e�L�X�g)"
    private GameObject allyObject;//"GameObject(����)"

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        rotation = 90.0f;
        allyObject = Instantiate(player[GameManager.selectPlayer], this.transform.position, this.transform.rotation, thisTransform);
        animator = allyObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //���̃I�u�W�F�N�g�̃��[���h���W���r���[�|�[�g���W�ɕϊ����Ď擾����
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;

        if (viewPortPosition.x < 0 && (boxCollider.enabled == true || allyNumber > nowAlly))
        {
            Destroy();//�֐�"Destroy"�����s����
        }

        if (allyNumber > nowAlly)
        {
            if(sacrifice == false)
            {
                Death();
                sacrifice = true;
            }

            if (this.transform.position.y <= 0.0f)
            {
                this.transform.position = new Vector3(this.transform.position.x, 0.0f, this.transform.position.z);
            }
        }
    }

    public override void Death()
    {
        this.gameObject.transform.SetParent(null);//�e����O��
        base.Death();
    }

    //�Փ˔���(OnTriggerEnter)
    public override void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Player"�̏ꍇ
        if (collision.gameObject.tag == "Player" && nowAlly < Player.maxAlly)
        {
            boxCollider.enabled = false;//BoxCollider��"����"�ɂ���
            text.SetActive(false);      //�e�L�X�g���\���ɂ���
            nowAlly += 1;               //��������"+1"����
            allyNumber = nowAlly;
            
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, rotation, this.transform.rotation.z);
            this.gameObject.transform.SetParent(playerTransform);
            this.transform.position = new Vector3(playerTransform.position.x - (1.0f * allyNumber), playerTransform.position.y, playerTransform.position.z);
        }
    }
}