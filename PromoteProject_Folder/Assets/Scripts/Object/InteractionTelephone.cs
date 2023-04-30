using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTelephone : InteractionObject 
{
    public int objID;
    public int dialogueID;

    public override void Interaction() 
    {
        diaManager.Action(objID, dialogueID);
    }
}
