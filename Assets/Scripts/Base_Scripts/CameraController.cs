using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //カメラの移動速度
    public static float[] speed = new float[5]
    { 2.0f, 2.0f, 2.0f, 2.0f, 0.0f };
    //カメラの移動限界値
    private Vector2[] limitPosition = new Vector2[5]
    {
        new Vector2(100.0f, 0.0f),
        new Vector2(245.0f, 0.0f),
        new Vector2(245.0f, 0.0f),
        new Vector2(200.0f, 0.0f),
        new Vector2(10000.0f, 0.0f),
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < limitPosition[Stage.nowStage - 1].x && PlayerController.hp > 0) 
        {
            this.transform.position += speed[Stage.nowStage - 1] * transform.right * Time.deltaTime;//右方向に移動する
        }
    }
}
