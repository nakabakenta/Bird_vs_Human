using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //�X�Y��
    public static class Sparrow
    {
        public static string name = "Sparrow";//���O
        public static int hp = 6;             //�̗�
        public static int power = 7;          //�U����
        public static int speed = 8;          //�ړ����x
    }
    //�J���X
    public static class Crow
    {
        public static string name = "Crow";//���O
        public static int hp = 8;          //�̗�
        public static int power = 10;      //�U����
        public static int speed = 6;       //�ړ����x
    }
    //�R�K��
    public static class Chickadee
    {
        public static string name = "Chickadee";//���O
        public static int hp = 6;               //�̗�
        public static int power = 5;            //�U����
        public static int speed = 10;           //�ړ����x
    }
    //�y���M��
    public static class Penguin
    {
        public static string name = "Penguin";//���O
        public static int hp = 10;            //�̗�
        public static int power = 10;         //�U����
        public static int speed = 10;         //�ړ����x
    }
}
