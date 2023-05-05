using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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

    public void saveData(int _exp, int _money, int _maxMoney, int _endurance, int _strength,
    int _intelligence, int _atkDamege, float _health, float _maxHealth, float _working, float _maxWorking)
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
    }
}

public class DataManager : MonoBehaviour
{
    string path;
    public static DataManager instance;

    // ���ȿ� ���� �����ϴ� ��ġ�� ���� ����
    float maxHealthSave;
    int atkDamegeSave;

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

        path = Path.Combine(Application.dataPath, "Resources/database.json");
        JsonLoad();
    }

    public void JsonLoad() {
        
        // �⺻���� ���� ����
        if (!File.Exists(path))
        {
            PlayerStat.instance.saveData(0, 0, 0, 0, 0, 0, 10, 100, 100, 50, 50);
        }
        else { 
            string loadJson = File.ReadAllText(path);
            SavePlayerStat savePlayerStat = new SavePlayerStat();
            savePlayerStat = JsonUtility.FromJson<SavePlayerStat>(loadJson);

            if (savePlayerStat != null)
            {
                PlayerStat.instance.saveData(savePlayerStat.exp, savePlayerStat.money, savePlayerStat.maxMoney,
                    savePlayerStat.endurance, savePlayerStat.strength, savePlayerStat.intelligence, savePlayerStat.atkDamege,
                    savePlayerStat.health, savePlayerStat.maxHealth, savePlayerStat.working, savePlayerStat.maxWorking);
            }
        }
    }

    public void JsonSave() {
        SavePlayerStat savePlayerStat = new SavePlayerStat();

        savePlayerStat.saveData(PlayerStat.instance.exp, PlayerStat.instance.money, PlayerStat.instance.maxMoney,
            PlayerStat.instance.endurance, PlayerStat.instance.strength, PlayerStat.instance.intelligence, PlayerStat.instance.atkDamege,
            PlayerStat.instance.health, PlayerStat.instance.maxHealth, PlayerStat.instance.working, PlayerStat.instance.maxWorking);

        string json = JsonUtility.ToJson(savePlayerStat, true);

        File.WriteAllText(path, json);
    }
}
