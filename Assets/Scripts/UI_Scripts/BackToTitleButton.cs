using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackToTitleButton : ButtonBase, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public static string selectButton = null;//�I�����Ă���{�^��
    public static bool buttonClick;          //�{�^���̃N���b�N��

    // Start is called before the first frame update
    void Start()
    {
        GetComponent();
        moveButton = false;
        buttonClick = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //�{�^����"�N���b�N���Ă��Ȃ�"�ꍇ
        if (buttonClick == false)
        {
            selectButton = button.gameObject.name;//�I�����Ă���{�^���̖��O������

            if (selectButton == "Button_BackToTitle")
            {
                GameManager.nextScene = "Title";
            }

            EnterButton();//�֐�"EnterButton"�����s����
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonClick == false)
        {
            ExitButton();
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

    public override void ResetButton()
    {
        selectButton = null;
    }
}
