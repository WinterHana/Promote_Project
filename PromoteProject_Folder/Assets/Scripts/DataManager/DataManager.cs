using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData {
    public int exp;               // ����ġ
    public int endurance;         // ������
    public int strength;          // �ٷ�
    public int intelligence;      // ����
    public float health;          // ü��
    public float maxHealth;       // �ִ� ü��
    public float working;         // �Ƿε�
    public float maxWorking;      // �ִ� �Ƿε�
}

public class DataManager : MonoBehaviour
{
    string path;
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

    private void Start()
    {
        path = Path.Combine(Application.dataPath, "Database/database.json");
        JsonLoad();
    }

    public void JsonLoad() {
        SaveData saveData = new SaveData();

        if (!File.Exists(path))
        {
            PlayerStat.instance.health = 100;
            PlayerStat.instance.working = 50;
            PlayerStat.instance.maxHealth = 100;
            PlayerStat.instance.maxWorking = 50;
        }
        else { 
            string loadJson = File.ReadAllText(path);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            if (saveData != null) { 
                PlayerStat.instance.health = saveData.health;
                PlayerStat.instance.working = saveData.working;
                PlayerStat.instance.maxHealth = saveData.maxHealth;
                PlayerStat.instance.maxWorking = saveData.maxWorking;
                PlayerStat.instance.exp = saveData.exp;
                PlayerStat.instance.endurance = saveData.endurance;
                PlayerStat.instance.strength = saveData.strength;
                PlayerStat.instance.intelligence = saveData.intelligence;
            }
        }
    }

    public void JsonSave() {
        SaveData saveData = new SaveData();

        saveData.health = PlayerStat.instance.health;
        saveData.maxHealth = PlayerStat.instance.maxHealth;
        saveData.working = PlayerStat.instance.working;
        saveData.maxWorking = PlayerStat.instance.maxWorking;
        saveData.exp = PlayerStat.instance.exp;
        saveData.endurance = PlayerStat.instance.endurance;
        saveData.strength = PlayerStat.instance.strength;
        saveData.intelligence = PlayerStat.instance.intelligence;

        string json = JsonUtility.ToJson(saveData, true);

        File.WriteAllText(path, json);
    }
}
