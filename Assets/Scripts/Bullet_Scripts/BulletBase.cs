using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    //�X�e�[�^�X
    public float moveSpeed;//�e�̈ړ����x
    //���W
    public Vector3 viewPortPosition;

     public void BaseUpdate()
    {
        //���̃I�u�W�F�N�g�̃��[���h���W���r���[�|�[�g���W�ɕϊ����Ď擾����
        viewPortPosition.x = Camera.main.WorldToViewportPoint(this.transform.position).x;
        viewPortPosition.y = Camera.main.WorldToViewportPoint(this.transform.position).y;

        if (viewPortPosition.x < 0 || viewPortPosition.x > 1 || 
            viewPortPosition.y < 0)
        {
            Destroy();
        }
    }

    //�֐�"Destroy"
    public void Destroy()
    {
        Destroy(this.gameObject);//���̃I�u�W�F�N�g������
    }

    //�Փ˔���(OnTriggerEnter)
    virtual public void OnTriggerEnter(Collider collision)
    {
        if (this.tag == "Bullet" && (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "BossEnemy"))
        {
            Destroy();
        }
        else if (this.tag == "EnemyBullet" && collision.gameObject.tag == "Player")
        {
            Destroy();
        }
    }
}