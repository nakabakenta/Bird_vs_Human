using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdAlly : MonoBehaviour
{
    public static int count;
    private bool follow;
    //�R���|�[�l���g
    private BoxCollider boxCollider;  //BoxCollider
    private Transform playerTransform;//Transform(�v���C���[)

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = this.gameObject.GetComponent<BoxCollider>();//���̃I�u�W�F�N�g��"BoxCollider"���擾
        playerTransform = GameObject.Find("Player").transform;    //
        follow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (follow == true && count == 1)
        {
            this.transform.position = new Vector3(playerTransform.position.x - 1.0f, playerTransform.position.y, playerTransform.position.z);
        }
        else if(follow == true && count == 2)
        {
            this.transform.position = new Vector3(playerTransform.position.x - 2.0f, playerTransform.position.y, playerTransform.position.z);
        }

        if (PlayerController.isDamage == true)
        {
            Destroy(this.gameObject);//���̃I�u�W�F�N�g������
        }
    }

    //�Փ˔���(OnTriggerEnter)
    void OnTriggerEnter(Collider collision)
    {
        //�Փ˂����I�u�W�F�N�g�̃^�O��"Player"��������
        if (collision.gameObject.tag == "Player" && count < 3)
        {
            follow = true;
            count++;
        }
    }
}
