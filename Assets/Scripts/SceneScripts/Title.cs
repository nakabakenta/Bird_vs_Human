using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private SceneLoader sceneLoader;//

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();//
    }

    // Update is called once per frame
    void Update()
    {
        //�}�E�X(�� or �E)�N���b�N��������
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            sceneLoader.PlayerSelect();
        }
    }
}
