using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        DataManager.instance.JsonLoad(); 
    }

    private void OnApplicationQuit()
    {
        DataManager.instance.JsonSave();
    }
}
