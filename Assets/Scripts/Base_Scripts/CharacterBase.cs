using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public int hp;          //体力
    public int attack;      //攻撃力
    public float speed;     //移動速度
    public float jump;      //ジャンプ力
    public string nowStatus;//状態

    //関数"Damage"
    void Damage()
    {
        hp -= 1;//体力を"-1"する
    }

    //関数"Death"
    public virtual void Death()
    {
        hp = 0;//体力を"0"にする
    }

    //関数"Destroy"
    public virtual void Destroy()
    {
        Destroy(this.gameObject);//このオブジェクトを消す
    }
}
