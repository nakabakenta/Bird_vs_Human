using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelect : MonoBehaviour
{
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject stageSelectUI;//�X�e�[�W�Z���N�g

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (StageSelectButton.buttonClick == true) 
        {
            stageSelectUI.SetActive(false);
        }
    }
}
