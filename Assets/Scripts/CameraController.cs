using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float speed = 2.0f;           //�J�����ړ����x
    private float limitPositionX = 100.0f;//�J�����ړ�����.X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x < limitPositionX)
        {
            this.transform.position += speed * transform.right * Time.deltaTime;//�E�����Ɉړ�����
        }
    }
}
