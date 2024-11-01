using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBullet : MonoBehaviour
{
    public int speed;//�e�̑��x
    //�r���[�|�[�g���W�ϐ�
    private float viewY;//�r���[�|�[�gY���W

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= speed * transform.up * Time.deltaTime;//

        //�ړ���̃r���[�|�[�g���W�l���擾
        viewY = Camera.main.WorldToViewportPoint(this.transform.position).y;//���Y���W

        //-Y���W
        if (viewY < 0)
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //���L�̃^�O���t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Enemy" )//�G
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }
}
