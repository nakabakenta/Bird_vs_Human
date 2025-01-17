using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    //����
    public static string selectButton;  //�I�����Ă���{�^��
    public static bool buttonClick;     //�{�^���̃N���b�N��
    private Vector2 buttonPosition;     //�{�^���̈ʒu
    private bool setActive;             //�I�u�W�F�N�g�\���̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject alpha;            //"GameObject(������)"
    public GameObject selectMark;       //"GameObject(�I���}�[�N)"
    public AudioClip enter;             //"AudioClip(����)"
    public AudioClip click;             //"AudioClip(�N���b�N)"
    private Button button;              //"Button" 
    private RectTransform rectTransform;//"RectTransform"
    private AudioSource audioSource;    //"AudioSource"
    private SceneLoader sceneLoader;    //"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        //������������
        buttonPosition = rectTransform.anchoredPosition;
        buttonClick = false;
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾����
        button = this.GetComponent<Button>();
        rectTransform = this.GetComponent<RectTransform>();
        audioSource = this.GetComponent<AudioSource>();
        sceneLoader = this.GetComponent<SceneLoader>();
        //���̃I�u�W�F�N�g�̃R���|�[�l���g��������
        selectMark.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //�{�^����"�N���b�N���Ă��Ȃ�"�ꍇ
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

            //�X�Y��
            if (selectButton == "Button_Sparrow")
            {
                GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"��"playerNumber"��"�X�Y��(0)"�ɂ���
            }
            //�J���X
            else if (selectButton == "Button_Crow")
            {
                GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"��"playerNumber"��"�J���X(1)"�ɂ���
            }
            //�R�K��
            else if (selectButton == "Button_Chickadee")
            {
                GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"��"playerNumber"��"�R�K��(2)"�ɂ���
            }
            //�y���M��
            else if (selectButton == "Button_Penguin")
            {
                GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"��"playerNumber"��"�y���M��(3)"�ɂ���
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
            buttonClick = true;            //�{�^����"�N���b�N����"�ɂ���
            audioSource.PlayOneShot(click);//"�N���b�N"��炷
            Invoke("SceneLoad", 2.0f);     //�֐�"SceneLoad"��"2.0f"��Ɏ��s
        }

        InvokeRepeating("Flash", 0.0f, 0.25f);//�֐�"Flash"��"0.0f"��Ɏ��s�A"0.25f"���ɌJ��Ԃ�
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
        sceneLoader.StageSelect();//"SceneLoader"�̊֐�"StageSelect"�����s
    }
}