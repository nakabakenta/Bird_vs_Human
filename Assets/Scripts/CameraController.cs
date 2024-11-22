using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private float speed = 2.0f;           //カメラ移動速度
    private float limitPositionX = 100.0f;//カメラ移動制限.X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.x < limitPositionX)
        {
            this.transform.position += speed * transform.right * Time.deltaTime;//右方向に移動する
        }
    }
}
