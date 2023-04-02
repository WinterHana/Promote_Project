using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    private Animator animator;
    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
    }

}
