using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : UIBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�}�E�X��(�� || �E)�N���b�N��������
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            GameManager.nowScene = "PlayerSelect";
            LoadScene();
        }
    }
}
