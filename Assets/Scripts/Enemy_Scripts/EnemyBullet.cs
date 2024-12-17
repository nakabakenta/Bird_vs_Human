using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //����
    private float speed;     //�e�̈ړ����x
    private float viewPointX;//�r���[�|�C���g���W.X

    // Start is called before the first frame update
    void Start()
    {
        //speed = EnemyList.EnemyBullet.speed[Stage.nowStage - 1];
        speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * transform.up * Time.deltaTime;//

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
