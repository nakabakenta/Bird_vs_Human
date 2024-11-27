using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //ˆ—
    private float speed = 2.0f;         //ˆÚ“®‘¬“x
    private float maxPositionX = 100.0f;//Å‘åˆÚ“®À•W.X

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.x < maxPositionX && PlayerController.hp > 0)
        {
            this.transform.position += speed * transform.right * Time.deltaTime;//‰E•ûŒü‚ÉˆÚ“®‚·‚é
        }
    }
}
