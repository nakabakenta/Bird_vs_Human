using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownBullet : MonoBehaviour
{
    //����
    public float speed;      //�e�̈ړ����x
    private float viewPointY;//�r���[�|�C���g���W.Y

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position -= speed * transform.up * Time.deltaTime;//

        //�r���[�|�C���g���W���擾
        viewPointY = Camera.main.WorldToViewportPoint(this.transform.position).y;//���Y���W

        //-Y���W
        if (viewPointY < 0)
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
