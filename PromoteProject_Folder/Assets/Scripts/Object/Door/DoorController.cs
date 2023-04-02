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

    // �� �Լ��θ� open ������ �����ϵ��� ����.

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
