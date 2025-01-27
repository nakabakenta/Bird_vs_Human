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
            GameManager.nowScene = "StageSelect";
            buttonClick = true;                   //�{�^����"�N���b�N����"�ɂ���
            audioSource.PlayOneShot(click);       //"�N���b�N"��炷
            InvokeRepeating("Flash", 0.0f, 0.25f);//�֐�"Flash"��"0.0f"��Ɏ��s�A"0.25f"���ɌJ��Ԃ�
            Invoke("LoadScene", 2.0f);            //�֐�"LoadScene"��"2.0f"��Ɏ��s
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