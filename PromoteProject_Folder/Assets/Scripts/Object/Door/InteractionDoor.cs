using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : InteractionObject
{
    [SerializeField] List<DoorController> doors;
    public ExplainTextController text;

    public override void Interaction()
    {
        foreach (DoorController door in doors)
        {
            if(door.open) door.open = false;
            else door.open = true;
        }

        // 안내문 출력
        foreach (DoorController door in doors)
        {
            if (door.open) {
                ExplainTextController tmp = Instantiate(text);
                tmp.guide = "문이 열렸습니다.";
                StartCoroutine(TextDelay());
            }
            else {
                ExplainTextController tmp = Instantiate(text);
                tmp.guide = "문이 닫혔습니다.";
                StartCoroutine(TextDelay());
            }
        }
    }

    IEnumerator TextDelay()
    {
        yield return new WaitForSeconds(0.2f);
    }
}
