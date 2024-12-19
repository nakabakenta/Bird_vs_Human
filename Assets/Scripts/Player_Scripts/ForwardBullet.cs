using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardBullet : MonoBehaviour
{
    //����
    public float speed;      //�e�̈ړ����x
    private float viewPointX;//�r���[�|�C���g���W.X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += (speed + CameraController.speed[Stage.nowStage - 1]) * transform.right * Time.deltaTime;//

        //�r���[�|�C���g���W���擾
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
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Enemy"��������
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy")
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }
}
