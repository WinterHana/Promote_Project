using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using LitJson;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SavePlayerStat {
    public int exp;                 // ����ġ
    public int money;               // ���� ��
    public int maxMoney;            // ��ǥ�� ��
    public int endurance;           // ������
    public int strength;            // �ٷ�
    public int intelligence;        // ����
    public int atkDamege;           // ������ �ִ� ������
    public float health;            // ü��
    public float maxHealth;         // �ִ� ü��
    public float working;           // �Ƿε�
    public float maxWorking;        // �ִ� �Ƿε�
    public int times;               // �ð�
    public int dialogue;            // ��ȭ ����

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
        // �⺻���� ���� ����
/*        if (!File.Exists(path_origin))
        {
            PlayerStat.instance.saveData(0, 0, 50000000, 0, 0, 0, 10, 200, 200, 50, 50, 0, 0);
            Debug.Log("JsonLoad IF�� ����ó��");
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

            Debug.Log("JsonLoad �۵�");
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

        Debug.Log("JsonSave �۵�");
    }
}
