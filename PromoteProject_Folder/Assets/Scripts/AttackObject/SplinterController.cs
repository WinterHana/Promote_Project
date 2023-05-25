using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplinterController : PlayerAttackObject
{
    Animator ani;
    [SerializeField] float period;
    [SerializeField] bool isUp;
    private void Awake()
    {
        ani = GetComponent<Animator>();
    }

    protected override void Start()
    {
        base.Start();
        isUp = false;
        StartCoroutine(countTime(period));
    }

    IEnumerator countTime(float period)
    {
        yield return new WaitForSeconds(period);
        isUp = !isUp;
        ani.SetBool("isUp", isUp);
        StartCoroutine(countTime(period));      // 재귀로 계속 반복
    }

}
