using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        sceneLoader = this.GetComponent<SceneLoader>();//����"Script(SceneLoader)"���擾����
    }

    // Update is called once per frame
    void Update()
    {
        //�}�E�X��(�� || �E)�N���b�N��������
        if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
        {
            sceneLoader.PlayerSelect();//"Script(SceneLoader)"��"�֐�(PlayerSelect)"�����s����
        }
    }
}
