using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAlly : MonoBehaviour
{
    //����
    private float viewPointX;//�r���[�|�C���g���W.X
    //�I�u�W�F�N�g
    public GameObject[] ally = new GameObject[3];//���ԃI�u�W�F�N�g
    //�R���|�[�l���g(���̃I�u�W�F�N�g)
    private Transform thisTransform;//"Transform"
    private BoxCollider boxCollider;//"BoxCollider"
    //�R���|�[�l���g(���̃I�u�W�F�N�g)
    private Transform playerTransform;//"Transform(�v���C���[)"

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = this.gameObject.transform;                //���̃I�u�W�F�N�g��"Transform"���擾
        playerTransform = GameObject.Find("Player").transform;    //�Q�[���I�u�W�F�N�g"Player"��T����"Transform"���擾
        boxCollider = this.gameObject.GetComponent<BoxCollider>();//���̃I�u�W�F�N�g��"BoxCollider"���擾

        Instantiate(ally[GameManager.playerNumber], this.transform.position, this.transform.rotation, thisTransform);
    }

    // Update is called once per frame
    void Update()
    {
        //�r���[�|�C���g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//���X���W

        if(viewPointX < 0)
        {
            Destroy();//�֐�"Destroy"�����s
        }
    }

    //�֐�"Destroy"
    void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Player"�������ꍇ
        if (collision.gameObject.tag == "Player")
        {
            this.transform.eulerAngles = new Vector3(this.transform.rotation.x, 90.0f, this.transform.rotation.z);
            Destroy();//�֐�"Destroy"�����s����
        }
    }
}