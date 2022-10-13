using System.IO;
using System.Net;
using DataStruct;
using SaveData;
using UnityEngine;


public class ItemManager : MonoBehaviour
{
    // save/load data 
    [HideInInspector] public ItemsInfo itemsInfo;
    [HideInInspector] public ItemsInfoJson loadItemsInfo;
    public GameObject pills;
    public GameObject key;
    public GameObject tube;
    public GameObject note1;
    public GameObject note2;
    public GameObject note3;
    public GameObject bottle;
    public GameObject coat;


    void Start()
    {
        loadItemsInfo = new ItemsInfoJson();
        if (!File.Exists(Application.persistentDataPath + "/itemsInfo.json"))
        {
            loadItemsInfo.Save();
        }

        itemsInfo = loadItemsInfo.Load();
        Debug.Log(Application.persistentDataPath + "/itemsInfo.json");
        if (PlayerPrefs.GetInt("Pills") >= 1)
        {
            pills.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Key") >= 1)
        {
            key.SetActive(false);
        }

        if (PlayerPrefs.GetInt("PaintTube") >= 1)
        {
            tube.SetActive(false);
        }

        if (PlayerPrefs.GetInt("Bottle") >= 1)
        {
            bottle.SetActive(false);
        }

        switch (PlayerPrefs.GetInt("Notes"))
        {
            case 1:
                note1.SetActive(false);
                break;
            case 2:
                note1.SetActive(false);
                note2.SetActive(false);
                break;
            case 3:
                note1.SetActive(false);
                note2.SetActive(false);
                note3.SetActive(false);
                break;
        }
    }
}