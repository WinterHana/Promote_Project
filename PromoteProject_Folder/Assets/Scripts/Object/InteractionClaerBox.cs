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
        // 아무 키나 누르면 홈타운으로 이동
        if (isPopUp && Input.anyKeyDown)
            GameManager.instance.goToHomeTown();
    }

    public override void Interaction()
    {
        GameManager.instance.stageClaer = true;
        isPopUp = true;
    }
}
