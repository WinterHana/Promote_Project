using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionBed : InteractionObject
{
    public override void Interaction()
    {
        GameManager.instance.TodayChange();
    }
}
