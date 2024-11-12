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
        Stage.stage = 1;         //
        sceneLoader.StageScene();
    }
    //ステージ 2
    public void Stage2()
    {
        Stage.stage = 2;         //
        sceneLoader.StageScene();
    }
    //ステージ 3
    public void Stage3()
    {
        Stage.stage = 3;         //
        sceneLoader.StageScene();
    }
    //ステージ 4
    public void Stage4()
    {
        Stage.stage = 4;         //
        sceneLoader.StageScene();
    }
    //ステージ 5
    public void Stage5()
    {
        Stage.stage = 5;         //
        sceneLoader.StageScene();
    }
}
