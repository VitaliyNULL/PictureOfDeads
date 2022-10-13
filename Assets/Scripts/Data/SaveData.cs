using System.IO;
using DataStruct;
using UnityEngine;


namespace SaveData
{
    //saving inventory items
    public class SaveInventory
    {
        public void Save(bool[] arr, ItemType[] item)
        {
            string json;
            Inventory inventory;
            if (!File.Exists(Application.persistentDataPath + "/inventory.json"))
            {
                inventory = new Inventory();
                inventory.cells = arr;
                inventory.item = item;
                // json = JsonUtility.ToJson(inventory);
                // File.WriteAllText(Application.persistentDataPath + "/inventory.json", json);
            }
            else
            {
                inventory = Load();
                inventory.cells = arr;
                inventory.item = item;
            }

            json = JsonUtility.ToJson(inventory);
            File.WriteAllText(Application.persistentDataPath + "/inventory.json", json);
        }

        public Inventory Load()
        {
            string path = Application.persistentDataPath + "/inventory.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<Inventory>(json);
            }

            return new Inventory();
        }
    }
    
    //saving info about items 1 time
    public class ItemsInfoJson
    {
        public void Save()
        {
            ItemsInfo itemsInfo = new ItemsInfo();
            itemsInfo.itemsInfo = new string[6];
            itemsInfo.itemsInfo[0] = " ";
            itemsInfo.itemsInfo[1] = "Pills from your doctor";
            itemsInfo.itemsInfo[2] = "Key from your house";
            itemsInfo.itemsInfo[3] = "This tube can help you with draw";
            itemsInfo.itemsInfo[4] = "Ladies love wine";
            itemsInfo.itemsInfo[5] = "Cigaretes";
            
            string json = JsonUtility.ToJson(itemsInfo);
            File.WriteAllText(Application.persistentDataPath+"/itemsInfo.json",json);
        }

        public ItemsInfo Load()
        {
            string path = Application.persistentDataPath + "/itemsInfo.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<ItemsInfo>(json);
            }
            return new ItemsInfo();
        }
    }

    //saving data about acts
    public class ActLevelsInfo
    {
        public void Save(int actlevel, int task)
        {
            Debug.Log("Start saving level info");
            ActLevel actLeveltoSave = new ActLevel();
            actLeveltoSave.act = actlevel;
            actLeveltoSave.task = task;
            string json = JsonUtility.ToJson(actLeveltoSave);
            File.WriteAllText(Application.persistentDataPath + "/actlevel.json", json);

        }

        public ActLevel Load()
        {
            string path = Application.persistentDataPath + "/actlevel.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<ActLevel>(json);
            }
            return new ActLevel();
        }
    }
    //saving info about act and task 1 time
    public class ActTaskInfo
    {
        public void Save()
        {
            
            ActTask actTask = new ActTask();
            actTask.firstAct = new string[5];
            actTask.firstAct[0] = "I need to take a pills";
            actTask.firstAct[1] = "The doctor says that drawing is a good treatment";
            actTask.firstAct[2] = "I need to investigate that case";
            actTask.firstAct[3] = "I need to call Janna";
            actTask.firstAct[4] = "Hm.... Henson`s old bar";
            actTask.secondAct = new string[2];
            actTask.secondAct[0] = "I need to talk with Janna";
            actTask.secondAct[1] = "I need to go home";
            actTask.thirdAct = new string[1];
            actTask.thirdAct[0] = "I need to draw that woman from dream";
            actTask.fourthAct = new string[2];
            actTask.fourthAct[0] = "I need to find my raincoat";
            actTask.fourthAct[1] = "I need to go home from backdoor";

            string json = JsonUtility.ToJson(actTask);
            File.WriteAllText(Application.persistentDataPath+"/acttask.json",json);
        }

        public ActTask Load()
        {
            string path = Application.persistentDataPath + "/acttask.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                return JsonUtility.FromJson<ActTask>(json);
            }
            return new ActTask();
        }
    }
}