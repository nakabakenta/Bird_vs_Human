using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //カメラの移動速度
    public float moveSpeed;
    //カメラの可動域
    public Vector3 moveRange;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < moveRange.x && PlayerController.status != "Death") 
        {
            this.transform.position += moveSpeed * transform.right * Time.deltaTime;//右方向に移動する
        }
    }
}
