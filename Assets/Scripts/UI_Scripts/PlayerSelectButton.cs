using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerSelectButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    //����
    public static bool buttonSelect;//�{�^������������
    private bool setActive;         //�I�u�W�F�N�g�\���̉�
    //���̃I�u�W�F�N�g�̃R���|�[�l���g
    public GameObject buttonAlpha;  //"GameObject(��I��)"
    public AudioClip cursor;        //"AudioClip(�J�[�\��)"
    public AudioClip select;        //"AudioClip(�I��)"
    private Button button;          //"Button" 
    private AudioSource audioSource;//"AudioSource"
    private SceneLoader sceneLoader;//"Script(SceneLoader)"

    // Start is called before the first frame update
    void Start()
    {
        //���̃I�u�W�F�N�g�̃R���|�[�l���g���擾
        button = this.GetComponent<Button>();
        audioSource = this.GetComponent<AudioSource>();//"AudioSource"
        sceneLoader = this.GetComponent<SceneLoader>();//"Script(SceneLoader)"
        //������������
        buttonSelect = false;                          //�{�^����"�����Ă��Ȃ�"�ɂ���
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //�{�^����"�����Ă��Ȃ�"�ꍇ
        if (buttonSelect == false)
        {
            notSelect.SetActive(false);
            audioSource.PlayOneShot(cursor);//"select(�J�[�\��)"��炷

            //�X�Y��
            if (button.gameObject.name == "Button_Sparrow")
            {
                GameManager.playerNumber = PlayerList.Player.number[0];//"GameManager"��"playerNumber"��"�X�Y��(0)"�ɂ���
            }
            //�J���X
            else if (button.gameObject.name == "Button_Crow")
            {
                GameManager.playerNumber = PlayerList.Player.number[1];//"GameManager"��"playerNumber"��"�J���X(1)"�ɂ���
            }
            //�R�K��
            else if (button.gameObject.name == "Button_Chickadee")
            {
                GameManager.playerNumber = PlayerList.Player.number[2];//"GameManager"��"playerNumber"��"�R�K��(2)"�ɂ���
            }
            //�y���M��
            else if (button.gameObject.name == "Button_Penguin")
            {
                GameManager.playerNumber = PlayerList.Player.number[3];//"GameManager"��"playerNumber"��"�y���M��(3)"�ɂ���
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonSelect == false)
        {
            notSelect.SetActive(true);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //�{�^����"�����Ă��Ȃ�"�ꍇ
        if (buttonSelect == false)
        {
            buttonSelect = true;            //�{�^����"������"�ɂ���
            audioSource.PlayOneShot(select);//"select(�I��)"��炷
            Invoke("SceneLoad", 2.0f);      //�֐�"SceneLoad"��"2.0f"��Ɏ��s����
        }

        InvokeRepeating("Flash", 0.0f, 0.25f);//�֐�"Flash"��"0.0f"��Ɏ��s����"0.25f"���ɌJ��Ԃ�
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
        sceneLoader.StageSelect();//"SceneLoader"�̊֐�"StageSelect"�����s����
    }
}