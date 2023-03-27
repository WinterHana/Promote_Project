using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : Character
{
    [SerializeField] int startHP;
    public static PlayerInfo player;
    private void Awake()
    {
        DontDestroyOnLoad(this);

        if (player == null)
        {
            player = this;
        }
        else if (player != this)
        {
            Destroy(gameObject);
        }
        HP = player.startHP;
    }
}
