using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTelephone : InteractionObject 
{
    public DialogueInfo dialogueInfo;

    public override void Interaction() 
    {
        diaManager.Action(dialogueInfo.objID, dialogueInfo.dialogueID);
    }
}
