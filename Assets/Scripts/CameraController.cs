using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //����
    private float speed = 2.0f;         //�ړ����x
    private float maxPositionX = 100.0f;//�ő�ړ����W.X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < maxPositionX && PlayerController.hp > 0)
        {
            this.transform.position += speed * transform.right * Time.deltaTime;//�E�����Ɉړ�����
        }
    }
}
