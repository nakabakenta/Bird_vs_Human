using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float cameraSpeed;//ƒJƒƒ‰ˆÚ“®‘¬“x


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += cameraSpeed * transform.right * Time.deltaTime;//‰E•ûŒü‚ÉˆÚ“®‚·‚é
    }
}
