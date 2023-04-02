using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionDoor : InteractionObject
{
    [SerializeField] List<DoorController> doors;

    public override void Interaction()
    {
        foreach (DoorController door in doors)
        {
            if(door.open) door.open = false;
            else door.open = true;
        }
    }
}
