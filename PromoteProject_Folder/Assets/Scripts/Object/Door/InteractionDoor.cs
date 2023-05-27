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

        // �ȳ��� ���
        foreach (DoorController door in doors)
        {
            if (door.open) {
                ExplainTextController tmp = Instantiate(text);
                tmp.guide = "���� ���Ƚ��ϴ�.";
                StartCoroutine(TextDelay());
            }
            else {
                ExplainTextController tmp = Instantiate(text);
                tmp.guide = "���� �������ϴ�.";
                StartCoroutine(TextDelay());
            }
        }
    }

    IEnumerator TextDelay()
    {
        yield return new WaitForSeconds(0.2f);
    }
}
