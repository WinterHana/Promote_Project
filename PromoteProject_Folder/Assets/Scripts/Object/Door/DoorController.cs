using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    BoxCollider2D col;
    Animator ani;
    public bool open;
     
    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        ani = GetComponent<Animator>();
        open = false;
    }
    private void Update()
    {
        doorControl();
    }

    // 이 함수로만 open 변수를 조절하도록 하자.

    void doorControl()
    {
        if (open)
        {
            ani.SetBool("open", true);
            col.enabled = false;
        }
        else
        {
            ani.SetBool("open", false);
            col.enabled = true;
        }
    }
}
