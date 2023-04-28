using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEventController : MonoBehaviour
{
    [SerializeField] PlayerInfo playerInfo;
    private void Start()
    {
        playerInfo = GameObject.FindGameObjectWithTag("HPCanvas").GetComponent<PlayerInfo>();
    }

    public void UpEndurance()
    {
        Debug.Log("작동");
        
        if (PlayerStat.instance.working >= 10)
        {
            PlayerStat.instance.endurance++;        // 인내심 증가
            PlayerStat.instance.working -= 1;      // 행동치 감소
            playerInfo.Hpworking.MyCurrentValue = -1;
            DataManager.instance.JsonSave();
            DataManager.instance.JsonLoad();
        }
    }
}
