using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBullet : MonoBehaviour
{
    public int speed;//�e�̑��x

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += speed * transform.forward * Time.deltaTime;
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //���L�̃^�O���t�����I�u�W�F�N�g�ɏՓ˂�����
        if (collision.gameObject.tag == "Enemy"||//�G
            collision.gameObject.tag == "Delete")//�폜
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }
}
