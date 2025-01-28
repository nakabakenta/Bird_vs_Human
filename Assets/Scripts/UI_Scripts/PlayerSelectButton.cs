using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSelectButton : ButtonBase, IPointerEnterHandler, IPointerClickHandler
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

            //�X�Y��
            if (selectButton == "Button_Sparrow")
            {
                GameManager.selectPlayer = (int)PlayerBase.Player.PlayerName.Sparrow;//
            }
            //�J���X
            else if (selectButton == "Button_Crow")
            {
                GameManager.selectPlayer = (int)PlayerBase.Player.PlayerName.Crow;//
            }
            //�R�K��
            else if (selectButton == "Button_Chickadee")
            {
                GameManager.selectPlayer = (int)PlayerBase.Player.PlayerName.Chickadee;//
            }
            //�y���M��
            else if (selectButton == "Button_Penguin")
            {
                GameManager.selectPlayer = (int)PlayerBase.Player.PlayerName.Penguin;//
            }

            EnterButton();//�֐�"EnterButton"�����s����
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //�{�^����"�N���b�N���Ă��Ȃ�"�ꍇ
        if (buttonClick == false)
        {
            GameManager.nextScene = "StageSelect";
            buttonClick = true;                   //�{�^����"�N���b�N����"�ɂ���
            ClickButton();                        //�֐�"ClickButton"�����s����
        }
    }
}