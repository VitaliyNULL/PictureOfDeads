using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using DataStruct;
using SaveData;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Inventory")] [SerializeField] private Image[] icons;
    [SerializeField] private Sprite[] items;

    [HideInInspector] public Inventory inventory;
    [HideInInspector] public SaveInventory saveInventory;

    [Header("Show Info UI elements")]
    //Show info elements
    public GameObject showInfoUI;

    public Text textInfo;

    //player info
    private PlayerController playerController;
    private ActManager actManager;
    [Header("itemIsHere")] public Text bottomTextInfo;

    //item manager
    private ItemManager itemManager;
    public Image blackScreen;
    public Text loading;

    //Act manager
    public GameObject task;
    private int currentItemIndexInfo;
    private int currentItemIndex;
    public GameObject useButton;
    private bool isTaskActive;
    // text assets
    public TextAsset zeroScene;
    public TextAsset firstScene;
    public TextAsset secondScene;
    public TextAsset thirdScene;
    public TextAsset fourthScene;
    public TextAsset fifthScene;
    public TextAsset sixScene;
    public TextAsset firstPark;
    public TextAsset secondPark;
    public GameObject richard;
    public bool isPillDrinked;
    public Image pictureOfLily;
    public Image pictureOfAlexia;
    [Header("One Time")]
    public GameObject strike;
    public GameObject light;
    public GameObject maniac;
    void Start()
    {
        
        CheckUsedItems();
        isTaskActive = false;
        actManager = GameObject.Find("ActManager").GetComponent<ActManager>();
        // actManager.actLevel.act = 2;
        // actManager.actLevel.task = 0;
        if (actManager.actLevel.act == 2 && actManager.actLevel.task == 0)
        {
            loading.text = "Day 2";
            ShowPictureOfDead();

        }else StartInvisible();
        //  if (actManager.actLevel.act == 0 && actManager.actLevel.task == 0)
        // {
        //     DialogueManager.GetInstance().EnterDialogueMode(zeroScene);
        // }
        playerController = GameObject.Find("Heroe").GetComponent<PlayerController>();
        {
            saveInventory = new SaveInventory();
            if (!File.Exists(Application.persistentDataPath + "/inventory.json"))
            {
                saveInventory.Save(new bool[7], new ItemType[7]);
            }

            inventory = saveInventory.Load();
        }
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        for (int i = 0; i < inventory.cells.Length; i++)
        {
            if (inventory.cells[i])
            {
                icons[i].sprite = items[(int)inventory.item[i]];
            }
            else
            {
                icons[i].sprite = items[0];
            }
        }

        
        if (actManager.actLevel.act == 1 && actManager.actLevel.task == 1)
        {
            richard.SetActive(true);
        }

        //Test mode code 
        // if (Input.GetKeyDown(KeyCode.X))
        // {
        //     PlayerPrefs.DeleteAll();
        //     PlayerPrefs.SetInt("Coat",0);
        //     PlayerPrefs.SetInt("Torch",0);
        // }
        
        // if (Input.GetKeyDown(KeyCode.K) && !DialogueManager.GetInstance().dialogueIsPlaying)
        // {
        //     DialogueManager.GetInstance().EnterDialogueMode(firstScene);
        // }
    }
    
    
    
   
    public void ShowCurrentItemInfo(IconIndexNum iconIndexNum)
    {
        showInfoUI.SetActive(true);
        currentItemIndexInfo = (int)inventory.item[(int)iconIndexNum];
        currentItemIndex = (int)iconIndexNum;
        Debug.Log(currentItemIndex);
        Debug.Log(currentItemIndexInfo);
        if (inventory.item[currentItemIndex] == ItemType.Pills)
        {
            useButton.SetActive(true);
        }
        else
        {
            useButton.SetActive(false);
        }

        showInfoUI.LeanMoveY(500, 0.5f).setEaseOutExpo().delay = 0.1f;
        textInfo.text = itemManager.itemsInfo.itemsInfo[currentItemIndexInfo];
    }

    public void CloseCurrentItemInfo()
    {
        showInfoUI.LeanMoveY(-Screen.height, 0.5f).setEaseInExpo().setOnComplete(NotActive);
    }

    private void NotActive()
    {
        showInfoUI.SetActive(false);
    }
    
    
    //<><><><><><><><><><><><><><><><><><><>
    // this code can be simplified to 2 methods

    public void ShowItemHere()
    {
        bottomTextInfo.gameObject.LeanMoveY(-Screen.height, 0.1f);
        bottomTextInfo.text = "Press 'Space' Button to take an item";
        bottomTextInfo.gameObject.LeanMoveY(47, 0.5f).setEaseOutExpo().delay = 0.1f;
    }
    public void ShowPressSpace()
    {
        bottomTextInfo.gameObject.LeanMoveY(-Screen.height, 0.1f);
        bottomTextInfo.text = "Press 'Space' Button to continue";
        bottomTextInfo.gameObject.LeanMoveY(47, 0.5f).setEaseOutExpo().delay = 0.1f;
    }


    public void HideItemHere()
    {
        bottomTextInfo.gameObject.LeanMoveY(-Screen.height, 0.5f).setEaseInExpo().delay = 0.1f;
    }

    public void ShowDoorOpen()
    {
        bottomTextInfo.gameObject.LeanMoveY(-Screen.height, 0.1f);
        bottomTextInfo.text = "Press 'Space' to open the door";
        bottomTextInfo.gameObject.LeanMoveY(47, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void HideDoorOpen()
    {
        bottomTextInfo.gameObject.LeanMoveY(-Screen.height, 0.5f).setEaseInExpo().delay = 0.1f;
    }

    public void ShowWalleyExit()
    {
        bottomTextInfo.gameObject.LeanMoveY(-Screen.height, 0.1f);
        bottomTextInfo.text = "Press 'Space' to exit valley";
        bottomTextInfo.gameObject.LeanMoveY(47, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void HideWalleyExit()
    {
        bottomTextInfo.gameObject.LeanMoveY(-Screen.height, 0.5f).setEaseInExpo().delay = 0.1f;
    }
    
    //^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

    IEnumerator VisibleSprite(string scene)
    {
        blackScreen.color = new Color(0, 0, 0, 0);
        for (float f = 0.0f; f <= 1; f += 0.05f)
        {
            Color color = blackScreen.color;
            color.a = f;
            blackScreen.gameObject.SetActive(true);
            blackScreen.color = color;
            yield return new WaitForSeconds(0.01f);
        }

        blackScreen.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(scene);
    }

    IEnumerator InvisibleSprite()
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 1);
        loading.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        loading.gameObject.SetActive(false);
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color color = blackScreen.color;
            color.a = f;
            blackScreen.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        blackScreen.gameObject.SetActive(false);
    }

    public void StartScrimer()
    {
        StartCoroutine(Scrimer());
    }

    public IEnumerator Scrimer()
    {
        blackScreen.color = new Color(0, 0, 0, 0);
        for (float f = 0.0f; f <= 1; f += 0.05f)
        {
            Color color = blackScreen.color;
            color.a = f;
            blackScreen.gameObject.SetActive(true);
            blackScreen.color = color;
            yield return new WaitForSeconds(0.01f);
        }

        blackScreen.color = new Color(0, 0, 0, 1);
        yield return new WaitForSeconds(2f);
        actManager.actLevelSD.Save(2,0);
        SceneManager.LoadScene("Home");
    }

    public void StartInvisible()
    {
        StartCoroutine(InvisibleSprite());
    }

    public void StartVisible(string scene)
    {
        StartCoroutine(VisibleSprite(scene));
    }

    public void UseItem()
    {
        icons[currentItemIndex].sprite = items[0];
        switch (inventory.item[currentItemIndex])
        {
            case ItemType.Pills:
                PlayerPrefs.SetInt("Pills", 2);
                isPillDrinked = true;
                actManager.actLevelSD.Save(0, 1);
                break;
        }

        inventory.item[currentItemIndex] = ItemType.None;
        inventory.cells[currentItemIndex] = false;
        saveInventory.Save(inventory.cells, inventory.item);
        CloseCurrentItemInfo();
    }

    public void CheckUsedItems()
    {
        if (PlayerPrefs.GetInt("Pills") == 2)
        {
            isPillDrinked = true;
        }
    }

    public void ShowTasks()
    {
        if (isTaskActive)
        {
            task.SetActive(false);
            isTaskActive = false;
        }
        else if (!isTaskActive)
        {
            isTaskActive = true;
            task.SetActive(true);
        }
    }

    public void ShowInteract()
    {
        bottomTextInfo.gameObject.LeanMoveY(-Screen.height, 0.1f);
        bottomTextInfo.text = "Press 'Space' to interaction";
        bottomTextInfo.gameObject.LeanMoveY(47, 0.5f).setEaseOutExpo().delay = 0.1f;
    }

    public void HideInteract()
    {
        bottomTextInfo.gameObject.LeanMoveY(-Screen.height, 0.5f).setEaseInExpo().delay = 0.1f;
    }

    public void ShowPictureOfDead()
    {
        Image image;
        if (actManager.actLevel.act == 2 && actManager.actLevel.task == 0)
        {
            image = pictureOfLily;
            StartCoroutine(ShowImage(image));
        }
    }

    private IEnumerator ShowImage(Image image)
    {
        blackScreen.gameObject.SetActive(true);
        blackScreen.color = new Color(0, 0, 0, 1);
        AudioManager.GetInstance().Strike1();
        image.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        image.gameObject.SetActive(false);
        loading.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        loading.gameObject.SetActive(false);
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color color = blackScreen.color;
            color.a = f;
            blackScreen.color = color;
            yield return new WaitForSeconds(0.01f);
        }
        blackScreen.gameObject.SetActive(false);
    }

    public void StartTheEnd()
    {
        StartCoroutine(TheEnd());
    }
    public IEnumerator TheEnd()
    {
        blackScreen.color = new Color(0, 0, 0, 0);
        for (float f = 0.0f; f <= 1; f += 0.05f)
        {
            Color color = blackScreen.color;
            color.a = f;
            blackScreen.gameObject.SetActive(true);
            blackScreen.color = color;
            yield return new WaitForSeconds(0.04f);
        }
        loading.gameObject.SetActive(true);
        loading.text = "The End";
        blackScreen.color = new Color(0, 0, 0, 1);
        SceneManager.LoadScene("Menu");
        yield return new WaitForSeconds(3f);
    }
    
}