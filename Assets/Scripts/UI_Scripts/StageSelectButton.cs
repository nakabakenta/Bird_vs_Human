using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageSelectButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
{
    public static string selectButton;//�I�����Ă���{�^��
    public static bool buttonClick;   //�{�^���̃N���b�N��

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        moveButton = true;
        selectButton = null;
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

            //�X�e�[�W1
            if (button.gameObject.name == "Button_Stage1")
            {
                GameManager.nextScene = "Stage1";
            }
            //�X�e�[�W2
            else if (button.gameObject.name == "Button_Stage2")
            {
                GameManager.nextScene = "Stage2";
            }
            //�X�e�[�W3
            else if (button.gameObject.name == "Button_Stage3")
            {
                GameManager.nextScene = "Stage3";
            }
            //�X�e�[�W4
            else if (button.gameObject.name == "Button_Stage4")
            {
                GameManager.nextScene = "Stage4";
            }
            //�X�e�[�W5
            else if (button.gameObject.name == "Button_Stage5")
            {
                GameManager.nextScene = "Stage5";
            }

            EnterButton();//�֐�"EnterButton"�����s����
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //�{�^����"�N���b�N���Ă��Ȃ�"�ꍇ
        if (buttonClick == false)
        {
            buttonClick = true;//�{�^����"�N���b�N����"�ɂ���
            ClickButton();     //�֐�"ClickButton"�����s����
        }
    }
}