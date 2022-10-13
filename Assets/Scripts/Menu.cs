using SaveData;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Start()
    {
        ActTaskInfo actTaskInfo = new ActTaskInfo();
        actTaskInfo.Save();
        ActLevelsInfo actLevelsInfo = new ActLevelsInfo();
        actLevelsInfo.Save(0,0);
        ItemsInfoJson infoJson = new ItemsInfoJson();
        infoJson.Save();
        SaveInventory saveInventory = new SaveInventory();
        saveInventory.Save(new bool[7],new ItemType[7]);
        PlayerPrefs.DeleteAll();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Home");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
