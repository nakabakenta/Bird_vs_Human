using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunBullet : MonoBehaviour
{
    //�X�e�[�^�X
    private float speed;//�e�̈ړ����x
    //����
    private float viewPointX; //�r���[�|�C���g���W.X
    private Vector3 direction;//�I�u�W�F�N�g�̕���
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private Transform playerTransform;//"Transform(�v���C���[)"

    // Start is called before the first frame update
    void Start()
    {
        //�X�e�[�^�X��ݒ�
        speed = EnemyList.HaveGunEnemy.bulletSpeed;
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        playerTransform = GameObject.Find("Player").transform;//"Transform(�v���C���[)"

        direction = (playerTransform.position - this.transform.position).normalized;
        this.transform.rotation = Quaternion.LookRotation(direction);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * transform.forward * Time.deltaTime;//�O�����Ɉړ�����

        //�r���[�|�C���g���W���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//���X���W

        //
        if (viewPointX < 0)
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }
}
