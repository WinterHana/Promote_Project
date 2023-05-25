using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtilleryController : MonoBehaviour
{
    [SerializeField] GameObject prfBullet;
    [SerializeField] float period;
    Transform pos;
    void Start()
    {
        StartCoroutine(countTime(period));
        pos = transform.GetChild(0).transform;
    }

    IEnumerator countTime(float period)
    {
        yield return new WaitForSeconds(period);
        GeneratePrf(prfBullet);
        StartCoroutine(countTime(period));      // 재귀로 계속 반복
    }

    void GeneratePrf(GameObject prf)
    {
        GameObject ob = Instantiate(prf) as GameObject;
        ob.transform.position = pos.position;
    }
}
