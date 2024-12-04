using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//����"Script(SceneLoader)"���擾����
    }
    //�X�e�[�W�Z���N�g�ꗗ
    //�X�e�[�W 1
    public void Stage1()
    {
        Stage.nowStage = 1;      //"Stage"��"nowStage"��"1"�ɂ���
        sceneLoader.StageScene();//"Script(SceneLoader)"��"�֐�(StageScene)"�����s����
    }
    //�X�e�[�W 2
    public void Stage2()
    {
        Stage.nowStage = 2;       //"Stage"��"nowStage"��"2"�ɂ���
        sceneLoader.StageScene();//"Script(SceneLoader)"��"�֐�(StageScene)"�����s����
    }
    //�X�e�[�W 3
    public void Stage3()
    {
        Stage.nowStage = 3;      //"Stage"��"nowStage"��"3"�ɂ���
        sceneLoader.StageScene();//"Script(SceneLoader)"��"�֐�(StageScene)"�����s����
    }
    //�X�e�[�W 4
    public void Stage4()
    {
        Stage.nowStage = 4;      //"Stage"��"nowStage"��"4"�ɂ���
        sceneLoader.StageScene();//"Script(SceneLoader)"��"�֐�(StageScene)"�����s����
    }
    //�X�e�[�W 5
    public void Stage5()
    {
        Stage.nowStage = 5;      //"Stage"��"nowStage"��"5"�ɂ���
        sceneLoader.StageScene();//"Script(SceneLoader)"��"�֐�(StageScene)"�����s����
    }
}
