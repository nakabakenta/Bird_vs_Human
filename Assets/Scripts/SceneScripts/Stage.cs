using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    private SceneLoader sceneLoader;//

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();//Script"SceneLoader"‚ðŽæ“¾‚·‚é
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerController.hp <= 0)
        {
            sceneLoader.GameOver();
        }
    }
}
