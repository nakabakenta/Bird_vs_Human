using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NestStage : MonoBehaviour
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//����"Script(SceneLoader)"���擾����
    }

    public void NextStage()
    {
        if (Stage.nowStage == 1)
        {
            Stage.nowStage = 2;
        }
        else if (Stage.nowStage == 2)
        {
            Stage.nowStage = 3;
        }
        else if (Stage.nowStage == 3)
        {
            Stage.nowStage = 4;
        }
        else if (Stage.nowStage == 4)
        {
            Stage.nowStage = 5;
        }
        else if (Stage.nowStage == 5)
        {
            
        }

        sceneLoader.StageScene();
    }
}
