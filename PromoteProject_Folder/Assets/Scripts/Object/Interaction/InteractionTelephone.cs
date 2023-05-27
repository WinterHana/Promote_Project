using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTelephone : InteractionObject 
{
    public DialogueInfo dialogueInfo;

    private void Update()
    {
        dialogueInfo.dialogueID = PlayerStat.instance.dialogue;
    }

    public override void Interaction() 
    {
        diaManager.Action(dialogueInfo.objID, dialogueInfo.dialogueID);
    }
}
