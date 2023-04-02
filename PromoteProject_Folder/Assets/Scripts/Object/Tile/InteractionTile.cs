using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTile : InteractionObject
{
    [SerializeField] List<TileController> tiles;

    public override void Interaction()
    {
        foreach (TileController tile in tiles)
        {
            tile.TileControl();
        }
    }

}
