using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
{
    public static string selectButton; //�I�����Ă���{�^��
    public static bool buttonClick;    //�{�^���̃N���b�N��

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        buttonClick = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //�{�^����"�����Ă��Ȃ�"�ꍇ
        if (buttonClick == false)
        {
            if (selectButton != null)
            {
                //�I�����Ă���{�^����T��
                Transform findAlpha = GameObject.Find(selectButton).transform.Find("Alpha_UI_Base_02");
                Transform findSelectMark = GameObject.Find(selectButton).transform.Find("UI_Base_03");
                //�I�����Ă���{�^����T���ăR���|�[�l���g���擾
                RectTransform findRectTransform = GameObject.Find(selectButton).GetComponent<RectTransform>();
                //�I�����Ă���{�^����������
                findRectTransform.anchoredPosition = new Vector2(buttonPosition.x, findRectTransform.anchoredPosition.y);
                findAlpha.gameObject.SetActive(true);
                findSelectMark.gameObject.SetActive(false);
            }

            selectButton = button.gameObject.name;//�I�����Ă���{�^���̖��O������

            //�X�e�[�W1
            if (button.gameObject.name == "Button_Stage1")
            {
                Stage.nowStage = 1;//"Stage"��"nowStage"��"1"�ɂ���
            }
            //�X�e�[�W2
            else if (button.gameObject.name == "Button_Stage2")
            {
                Stage.nowStage = 2;//"Stage"��"nowStage"��"2"�ɂ���
            }
            //�X�e�[�W3
            else if (button.gameObject.name == "Button_Stage3")
            {
                Stage.nowStage = 3;//"Stage"��"nowStage"��"3"�ɂ���
            }
            //�X�e�[�W4
            else if (button.gameObject.name == "Button_Stage4")
            {
                Stage.nowStage = 4;//"Stage"��"nowStage"��"4"�ɂ���
            }
            //�X�e�[�W5
            else if (button.gameObject.name == "Button_Stage5")
            {
                Stage.nowStage = 5;//"Stage"��"nowStage"��"5"�ɂ���
            }

            rectTransform.anchoredPosition = new Vector2(buttonPosition.x + 200, rectTransform.anchoredPosition.y);
            alpha.SetActive(false);
            selectMark.SetActive(true);
            audioSource.PlayOneShot(enter);//"����"��炷
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //�{�^����"�N���b�N���Ă��Ȃ�"�ꍇ
        if (buttonClick == false)
        {
            buttonClick = true;                   //�{�^����"�N���b�N����"�ɂ���
            audioSource.PlayOneShot(click);       //"�N���b�N"��炷
            InvokeRepeating("Flash", 0.0f, 0.25f);//�֐�"Flash"��"0.0f"��Ɏ��s�A"0.25f"���ɌJ��Ԃ�
            Invoke("SceneLoad", 2.0f);            //�֐�"SceneLoad"��"2.0f"��Ɏ��s����
        }
    }

    //�֐�"Flash"
    void Flash()
    {
        //"setActive"��"true"�̏ꍇ��"false"�A"false"�̏ꍇ��"true"�ɂ���
        setActive = !setActive;

        //�q�I�u�W�F�N�g���擾
        foreach (Transform child in transform)
        {
            //�q�I�u�W�F�N�g�̖��O��"Alpha"�̏ꍇ
            if (child.name == "Alpha_UI_Base_02")
            {
                //�q�I�u�W�F�N�g���\���ɂ���
                child.gameObject.SetActive(false);
            }
            //����ȊO�̏ꍇ
            else
            {
                //�q�I�u�W�F�N�g��"setActive"�ɂ���
                child.gameObject.SetActive(setActive);
            }
        }
    }

    //�֐�"SceneLoad"
    void SceneLoad()
    {
        sceneLoader.StageScene();//"SceneLoader"�̊֐�"StageScene"�����s����
    }
}