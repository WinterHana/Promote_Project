using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : PlayerAttackObject
{
    Vector3 pos;
    [SerializeField] float delta = 3.0f;         // 좌우로 이동 가능한 최대 속도
    [SerializeField] float moveSpeed = 3.0f;
    float rotateSpeed = 1000.0f;

    protected override void Start()
    {
        base.Start();
        pos = transform.position; 
    }

    private void Update()
    {
        Vector3 v = pos;
        v.x += delta * Mathf.Sin(Time.time * moveSpeed);
        transform.position = v;

        transform.rotation = Quaternion.Euler(0, 0, Time.time * rotateSpeed);
    }
}
