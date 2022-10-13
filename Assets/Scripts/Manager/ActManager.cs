using System.IO;
using DataStruct;
using SaveData;
using TMPro;
using UnityEngine;

public class ActManager : MonoBehaviour
{
    //playerController
    private PlayerController player;
    //actData
    [HideInInspector]public ActTask actTask;
    [HideInInspector]public ActTaskInfo actTaskSD;
    [HideInInspector]public ActLevel actLevel;
    [HideInInspector]public ActLevelsInfo actLevelSD;
    public TextMeshProUGUI text;

    private void Start()
    {
       
        {
            actLevelSD = new ActLevelsInfo();
            if (!File.Exists(Application.persistentDataPath + "/actlevel.json"))
            {
                actLevelSD.Save(0, 0);
            }

            actLevel = actLevelSD.Load();

            Debug.Log("ANUS");
            actTaskSD = new ActTaskInfo();
            if (!File.Exists(Application.persistentDataPath + "/acttask.json"))
            {
                actTaskSD.Save();
                Debug.Log("CUM");
            }

            actTask = actTaskSD.Load();
            
        }

        player = GameObject.Find("Heroe").GetComponent<PlayerController>();
    }

    private void Update()
    {
        
        UpdateData();
        //Updating hint
        if (actLevel.act == 0)
        {
            text.text = actTask.firstAct[actLevel.task];
        }
        if (actLevel.act == 1)
        {
            text.text = actTask.secondAct[actLevel.task];
        }
        if (actLevel.act == 2)
        {
            text.text = actTask.thirdAct[actLevel.task];
        }
        if (actLevel.act == 3)
        {
            text.text = actTask.fourthAct[actLevel.task];
        }

    }

    private void UpdateData()
    {
        actLevel = actLevelSD.Load();
    }
}