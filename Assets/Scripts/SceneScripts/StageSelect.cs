using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    private SceneLoader sceneLoader;//SceneLoader

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = GetComponent<SceneLoader>();//Script"SceneLoader"を取得する
    }

    //ステージ 1
    public void Stage1()
    {
        sceneLoader.Stage1();
    }
}
