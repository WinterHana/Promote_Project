using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SavePlayerStat {
    public int exp;                 // 경험치
    public int money;               // 현재 돈
    public int maxMoney;            // 목표한 돈
    public int endurance;           // 지구력
    public int strength;            // 근력
    public int intelligence;        // 지능
    public int atkDamege;           // 적에게 주는 데미지
    public float health;            // 체력
    public float maxHealth;         // 최대 체력
    public float working;           // 피로도
    public float maxWorking;        // 최대 피로도
    public int times;               // 시간
    public int dialogue;            // 대화 순서

    public void saveData(int _exp, int _money, int _maxMoney, int _endurance, int _strength,
    int _intelligence, int _atkDamege, float _health, float _maxHealth, 
    float _working, float _maxWorking, int _times, int _dialogue)
    {
        exp = _exp;
        money = _money;
        maxMoney = _maxMoney;
        endurance = _endurance;
        strength = _strength;
        intelligence = _intelligence;
        atkDamege = _atkDamege;
        health = _health;
        maxHealth = _maxHealth;
        working = _working;
        maxWorking = _maxWorking;
        times = _times;
        dialogue = _dialogue;
    }
}

public class DataManager : MonoBehaviour
{
    string path;
    string path_origin;
    public static DataManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void JsonLoad() {
        // 기본적인 스탯 설정
/*        if (!File.Exists(path_origin))
        {
            PlayerStat.instance.saveData(0, 0, 50000000, 0, 0, 0, 10, 200, 200, 50, 50, 0, 0);
            Debug.Log("JsonLoad IF문 예외처리");
        }*/
        if(true)
        {
            string loadJson = File.ReadAllText(path_origin);
            SavePlayerStat savePlayerStat = new SavePlayerStat();
            savePlayerStat = JsonUtility.FromJson<SavePlayerStat>(loadJson);
            
            PlayerStat.instance.saveData(savePlayerStat.exp, savePlayerStat.money, savePlayerStat.maxMoney,
                savePlayerStat.endurance, savePlayerStat.strength, savePlayerStat.intelligence, savePlayerStat.atkDamege,
                savePlayerStat.health, savePlayerStat.maxHealth, savePlayerStat.working, savePlayerStat.maxWorking,
                savePlayerStat.times, savePlayerStat.dialogue);

            Debug.Log("JsonLoad 작동");
        }
    }

    public void JsonSave() {
        SavePlayerStat savePlayerStat = new SavePlayerStat();

        savePlayerStat.saveData(PlayerStat.instance.exp, PlayerStat.instance.money, PlayerStat.instance.maxMoney,
            PlayerStat.instance.endurance, PlayerStat.instance.strength, PlayerStat.instance.intelligence, PlayerStat.instance.atkDamege,
            PlayerStat.instance.health, PlayerStat.instance.maxHealth, PlayerStat.instance.working, PlayerStat.instance.maxWorking,
            PlayerStat.instance.times, PlayerStat.instance.dialogue);

        string json = JsonUtility.ToJson(savePlayerStat, true);

        File.WriteAllText(path_origin, json);

        Debug.Log("JsonSave 작동");
    }
}
