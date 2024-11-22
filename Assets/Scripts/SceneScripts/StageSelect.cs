using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    //このオブジェクトのコンポーネント
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//この"Script(SceneLoader)"を取得する
    }
    //ステージセレクト一覧
    //ステージ 1
    public void Stage1()
    {
        Stage.nowStage = 1;      //"Stage"の"nowStage"を"1"にする
        sceneLoader.StageScene();//"Script(SceneLoader)"の"関数(StageScene)"を実行する
    }
    //ステージ 2
    public void Stage2()
    {
        Stage.nowStage = 2;       //"Stage"の"nowStage"を"2"にする
        sceneLoader.StageScene();//"Script(SceneLoader)"の"関数(StageScene)"を実行する
    }
    //ステージ 3
    public void Stage3()
    {
        Stage.nowStage = 3;      //"Stage"の"nowStage"を"3"にする
        sceneLoader.StageScene();//"Script(SceneLoader)"の"関数(StageScene)"を実行する
    }
    //ステージ 4
    public void Stage4()
    {
        Stage.nowStage = 4;      //"Stage"の"nowStage"を"4"にする
        sceneLoader.StageScene();//"Script(SceneLoader)"の"関数(StageScene)"を実行する
    }
    //ステージ 5
    public void Stage5()
    {
        Stage.nowStage = 5;      //"Stage"の"nowStage"を"5"にする
        sceneLoader.StageScene();//"Script(SceneLoader)"の"関数(StageScene)"を実行する
    }
}
