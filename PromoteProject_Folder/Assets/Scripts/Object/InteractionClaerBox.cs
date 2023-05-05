using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionClaerBox : InteractionObject
{
    [SerializeField] bool isPopUp;
    [SerializeField] string nextScene;

    private void Start()
    {
        isPopUp = false;
    }

    private void Update()
    {   
        // �ƹ� Ű�� ������ ȨŸ������ �̵�
        if (isPopUp && Input.anyKeyDown)
            GameManager.instance.goToHomeTown();
    }

    public override void Interaction()
    {
        GameManager.instance.stageClaer = true;
        StartCoroutine(delayTime());
    }

    IEnumerator delayTime()
    {
        yield return new WaitForSeconds(3.0f);
        isPopUp = true;
    }
}
