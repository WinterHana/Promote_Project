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
        Debug.Log("�۵�");
        
        if (PlayerStat.instance.working >= 10)
        {
            PlayerStat.instance.endurance++;        // �γ��� ����
            PlayerStat.instance.working -= 1;      // �ൿġ ����
            playerInfo.Hpworking.MyCurrentValue = -1;
            DataManager.instance.JsonSave();
            DataManager.instance.JsonLoad();
        }
    }
}
