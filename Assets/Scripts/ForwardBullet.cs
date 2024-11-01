using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardBullet : MonoBehaviour
{
    public int speed;        //�e�ړ����x
    private float viewPointX;//�r���[�|�C���g���W.X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * transform.right * Time.deltaTime;//

        //�ړ���̃r���[�|�[�g���W�l���擾
        viewPointX = Camera.main.WorldToViewportPoint(this.transform.position).x;//���X���W

        //
        if (viewPointX > 1)
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //���L�̃^�O���t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Enemy")//�G
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }
}
