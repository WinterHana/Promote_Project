using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTile : InteractionObject
{
    [SerializeField] List<TileController> tiles;
    public ExplainTextController text;

    public override void Interaction()
    {
        foreach (TileController tile in tiles)
        {
            tile.TileControl();
            ExplainTextController tmp = Instantiate(text);
            tmp.guide = "������ ����Ǿ����ϴ�.";
            StartCoroutine(TextDelay());
        }
    }


    IEnumerator TextDelay()
    {
        yield return new WaitForSeconds(0.2f);
    }
}
