using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int hp;     //‘Ì—Í
    public float speed;//ˆÚ“®‘¬“x

    private Animator animator = null;

    private Transform a;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();

        a = GameObject.Find("lb_chickadee").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.z > a.position.z)
        {
            this.transform.position += speed * transform.forward * Time.deltaTime;
            animator.SetBool("Jump", true);
        }
        else
        {
            this.transform.position -= speed * transform.forward * Time.deltaTime;
            animator.SetBool("Jump", false);
        }

        animator.SetBool("Death", true);
    }
}
