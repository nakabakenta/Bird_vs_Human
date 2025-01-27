using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StageButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
{
    public static string selectButton;//�I�����Ă���{�^��
    public static bool buttonClick;   //�{�^���̃N���b�N��

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        buttonClick = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //�{�^����"�N���b�N���Ă��Ȃ�"�ꍇ
        if (buttonClick == false)
        {
            if (selectButton != null)
            {
                //�I�����Ă���{�^����T��
                Transform findAlpha = GameObject.Find(selectButton).transform.Find("Alpha_UI_Base_64_03");
                Transform findSelectMark = GameObject.Find(selectButton).transform.Find("UI_Base_64_04");
                //�I�����Ă���{�^����T���ăR���|�[�l���g���擾
                RectTransform findRectTransform = GameObject.Find(selectButton).GetComponent<RectTransform>();
                //�I�����Ă���{�^����������
                findRectTransform.anchoredPosition = new Vector2(buttonPosition.x, findRectTransform.anchoredPosition.y);
                findAlpha.gameObject.SetActive(true);
                findSelectMark.gameObject.SetActive(false);
            }

            selectButton = button.gameObject.name;//�I�����Ă���{�^���̖��O������

            //
            if (selectButton == "Button_Restart")
            {
                if(Stage.nowStage == 1)
                {
                    GameManager.nowScene = "Stage1";
                }
                else if(Stage.nowStage == 2)
                {
                    GameManager.nowScene = "Stage2";
                }
                else if(Stage.nowStage == 3)
                {
                    GameManager.nowScene = "Stage3";
                }
                else if(Stage.nowStage == 4)
                {
                    GameManager.nowScene = "Stage4";
                }
                else if(Stage.nowStage == 5)
                {
                    GameManager.nowScene = "Stage5";
                } 
            }
            //
            else if (selectButton == "Button_BackToMenu")
            {
                GameManager.nowScene = "PlayerSelect";
            }
            //
            else if (selectButton == "Button_NextStage")
            {
                
            }
            //
            else if (selectButton == "Button_GameClear")
            {
                GameManager.nowScene = "GameClear";
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
            if (selectButton == "Button_NextStage")
            {
                if (Stage.nowStage == 1)
                {
                    GameManager.nowScene = "Stage2";
                    Stage.nowStage = 2;
                }
                else if (Stage.nowStage == 2)
                {
                    GameManager.nowScene = "Stage3";
                    Stage.nowStage = 3;
                }
                else if (Stage.nowStage == 3)
                {
                    GameManager.nowScene = "Stage4";
                    Stage.nowStage = 4;
                }
                else if (Stage.nowStage == 4)
                {
                    GameManager.nowScene = "Stage5";
                    Stage.nowStage = 5;
                }
            }

            buttonClick = true;                   //�{�^����"�N���b�N����"�ɂ���
            audioSource.PlayOneShot(click);       //"�N���b�N"��炷
            LoadScene();

            InvokeRepeating("Flash", 0.0f, 0.25f);//�֐�"Flash"��"0.0f"��Ɏ��s�A"0.25f"���ɌJ��Ԃ�
            //Invoke("LoadScene", 2.0f);            //�֐�"LoadScene"��"2.0f"��Ɏ��s
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
            //�q�I�u�W�F�N�g�̖��O��"Alpha_UI_Base_02"�̏ꍇ
            if (child.name == "Alpha_UI_Base_64_03")
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
}
