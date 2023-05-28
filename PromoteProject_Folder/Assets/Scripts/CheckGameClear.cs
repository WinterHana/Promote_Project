using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckGameClear : MonoBehaviour
{
    private void Start()
    {
        if (PlayerStat.instance.times >= GameManager.instance.checkClearTime)
        {
            GameManager.instance.gameClearCheck();
        }
    }
}
