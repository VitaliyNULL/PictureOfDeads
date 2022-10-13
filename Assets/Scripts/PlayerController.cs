using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Rb and movement
    private Rigidbody2D playedRb;
    private Animator animator;
    private float speed;

    public float normalSpeed = 4f;

    // Item communication
    public bool isItemHere;

    private GameManager gameManager;

    //Founded object value
    private GameObject foundedItem;

    private ItemType foundedItemType;

    //Arrays to save data
    private bool[] itemsToSave;

    private ItemType[] itemTypesToSave;

    private bool isDoorHere;
    private bool isWalleyToHome;
    private bool isEaselHere;
    private ActManager actManager;
    private bool isPhoneHere;
    private bool isNoteHere;
    private Coroutine calling;
    private bool isJannaHere;
    private bool isBarDoorHere;
    private bool isRichardHere;
    private bool isCoatHere;
    private HeroMove heroMove;

    void Start()
    {
        heroMove = GetComponent<HeroMove>();
        playedRb = GetComponent<Rigidbody2D>();
        speed = 0f;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        actManager = GameObject.Find("ActManager").GetComponent<ActManager>();
        //debug 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }

        playedRb.velocity = new Vector2(speed, playedRb.velocity.y);
        animator.SetFloat("Speed", Math.Abs(speed));
    }

    private void Update()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        if (calling != null)
        {
            animator.StopPlayback();
            return;
        }

        itemsToSave = gameManager.inventory.cells;
        itemTypesToSave = gameManager.inventory.item;
        if (!heroMove.hasStayed)
        {
            if (Input.GetKey(KeyCode.A))
            {
                speed = -normalSpeed;
                transform.eulerAngles = new Vector2(0, 0);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                speed = normalSpeed;
                transform.eulerAngles = new Vector2(0, 180);
            }
            else speed = 0f;
        }

        // take an items
        {
            if (Input.GetKeyDown(KeyCode.Space) && isItemHere)
            {
                Debug.Log(itemsToSave.Length + " Length");
                for (int i = 0; i < itemsToSave.Length; i++)
                {
                    if (!itemsToSave[i])
                    {
                        Debug.Log(i + " is empty");
                        itemsToSave[i] = true;
                        itemTypesToSave[i] = foundedItemType;
                        gameManager.saveInventory.Save(itemsToSave, itemTypesToSave);
                        Debug.Log(i + " item type is " + foundedItemType);
                        Debug.Log(i + " cell was filled by " + foundedItem.name);
                        switch (foundedItemType)
                        {
                            case ItemType.Pills:
                                PlayerPrefs.SetInt("Pills", 1);
                                break;
                            case ItemType.Key:
                                PlayerPrefs.SetInt("Key", 1);
                                break;
                            case ItemType.PaintTube:
                                PlayerPrefs.SetInt("PaintTube", 1);
                                break;
                            case ItemType.Bottle:
                                PlayerPrefs.SetInt("Bottle", 1);
                                break;
                            case ItemType.Cigarettes:
                                PlayerPrefs.SetInt("Cigarettes", 1);
                                break;
                        }

                        foundedItem.SetActive(false);
                        break;
                    }
                    else if (itemsToSave[itemsToSave.Length - 2])
                    {
                        Debug.Log("Inventory is full");
                        break;
                    }
                    else
                    {
                        Debug.Log(i + " is not empty");
                    }
                }
            }
        }
        // something for moving on the plot
        {
            if (actManager.actLevel.act == 0)
            {
                if (actManager.actLevel.task == 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && isPhoneHere)
                    {
                        if (PlayerPrefs.GetInt("Phone") != 2)
                        {
                            DialogueManager.GetInstance().EnterDialogueMode(gameManager.zeroScene);
                            PlayerPrefs.SetInt("Phone", 2);
                            Debug.Log(PlayerPrefs.GetInt("Phone"));
                            gameManager.HideInteract();
                        }
                    }
                }

                if (actManager.actLevel.task == 1)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && isEaselHere && PlayerPrefs.GetInt("PaintTube") == 1)
                    {
                        Debug.Log("anusanusKUM");
                        DialogueManager.GetInstance().EnterDialogueMode(gameManager.firstScene);
                        gameManager.HideInteract();
                        actManager.actLevel.task = 2;
                        actManager.actLevelSD.Save(0, 2);
                    }
                }

                if (actManager.actLevel.task == 2)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && isNoteHere)
                    {
                        Destroy(foundedItem);
                        PlayerPrefs.SetInt("Notes", PlayerPrefs.GetInt("Notes") + 1);
                    }

                    if (PlayerPrefs.GetInt("Notes") == 3)
                    {
                        isPhoneHere = false;
                        actManager.actLevel.task = 3;
                        actManager.actLevelSD.Save(0, 3);
                    }
                }

                if (actManager.actLevel.task == 3)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && isPhoneHere)
                    {
                        if (calling == null)
                        {
                            calling = StartCoroutine(Calling());
                        }
                    }
                }

                if (actManager.actLevel.task == 4)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && isDoorHere && PlayerPrefs.GetInt("Key") == 1)
                    {
                        gameManager.StartVisible("Park");
                        actManager.actLevel.act = 1;
                        actManager.actLevel.task = 0;
                        actManager.actLevelSD.Save(1, 0);
                    }
                }
            }

            if (actManager.actLevel.act == 1)
            {
                if (actManager.actLevel.task == 0)
                {
                    if (heroMove.hasStayed && PlayerPrefs.GetInt("FirstPark") != 1)
                    {
                        DialogueManager.GetInstance().EnterDialogueMode(gameManager.firstPark);
                        PlayerPrefs.SetInt("FirstPark", 1);
                    }

                    if (DialogueManager.GetInstance().dialogIsOver && PlayerPrefs.GetInt("FirstPark") == 1)
                    {
                        gameManager.StartVisible("Bar");
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space) && isDoorHere && PlayerPrefs.GetInt("Key") == 1)
                {
                    gameManager.StartVisible("Park");
                }

                if (actManager.actLevel.task == 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && isJannaHere)
                    {
                        DialogueManager.GetInstance().EnterDialogueMode(gameManager.thirdScene);
                        actManager.actLevelSD.Save(1, 1);
                    }
                }


                if (actManager.actLevel.task == 1)
                {
                    if (isRichardHere && PlayerPrefs.GetInt("Richard") != 1)
                    {
                        PlayerPrefs.SetInt("Richard", 1);
                        DialogueManager.GetInstance().EnterDialogueMode(gameManager.fourthScene);
                    }

                    if (Input.GetKeyDown(KeyCode.Space) && isBarDoorHere)
                    {
                        gameManager.StartVisible("Park");
                    }

                    if (heroMove.hasStayed && PlayerPrefs.GetInt("SecondPark") != 1)
                    {
                        DialogueManager.GetInstance().EnterDialogueMode(gameManager.secondPark);
                        PlayerPrefs.SetInt("SecondPark", 1);
                        PlayerPrefs.SetInt("RichardAnus", 2);
                    }

                    if (DialogueManager.GetInstance().dialogIsOver && PlayerPrefs.GetInt("RichardAnus") == 2)
                    {
                        gameManager.StartScrimer();
                    }
                }
            }

            if (actManager.actLevel.act == 2)
            {
                if (actManager.actLevel.task == 0)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && isEaselHere)
                    {
                        gameManager.HideInteract();
                        DialogueManager.GetInstance().EnterDialogueMode(gameManager.fifthScene);
                        gameManager.ShowPressSpace();
                        actManager.actLevelSD.Save(3, 0);
                    }
                }
            }

            if (actManager.actLevel.act == 3)
            {
                if (actManager.actLevel.task == 0)
                {
                    if (PlayerPrefs.GetInt("BarLily") != 1 && Input.GetKeyDown(KeyCode.Space) &&
                        DialogueManager.GetInstance().dialogIsOver)
                    {
                        PlayerPrefs.SetInt("BarLily", 1);
                        SceneManager.LoadScene("BarLily");
                    }

                    if (Input.GetKeyDown(KeyCode.Space) && isCoatHere)
                    {
                        PlayerPrefs.SetInt("Coat", 1);
                        GameObject.Find("ItemManager").GetComponent<ItemManager>().coat.SetActive(false);
                        gameManager.HideInteract();
                        actManager.actLevelSD.Save(3, 1);
                    }
                }

                if (actManager.actLevel.task == 1)
                {
                    if (Input.GetKeyDown(KeyCode.Space) && isBarDoorHere)
                    {
                        gameManager.StartVisible("ValleyLily");
                    }
                }
            }

            if (actManager.actLevel.act == 4)
            {
                if (actManager.actLevel.task == 0)
                {
                }
            }
        }
    }


    //calling to Janna 
    public IEnumerator Calling()
    {
        AudioManager.GetInstance().audioSource.PlayOneShot(AudioManager.GetInstance().phoneRotate);
        yield return new WaitForSeconds(AudioManager.GetInstance().phoneRotate.length);
        DialogueManager.GetInstance().EnterDialogueMode(gameManager.secondScene);
        gameManager.HideInteract();
        calling = null;
        actManager.actLevel.task = 4;
        actManager.actLevelSD.Save(0, 4);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Strike")
        {
            gameManager.strike.SetActive(true);
        }

        if (col.tag == "Torch" && PlayerPrefs.GetInt("Torch") != 1)
        {
            gameManager.light.SetActive(false);
            gameManager.maniac.SetActive(true);
            AudioManager.GetInstance().Strike1();
            PlayerPrefs.SetInt("Torch", 1);
        }

        if (col.tag == "Coat" && actManager.actLevel.act == 3 && PlayerPrefs.GetInt("Coat") != 1)
        {
            isCoatHere = true;
            gameManager.ShowItemHere();
        }

        if (col.tag == "Item")
        {
            Items items = col.gameObject.GetComponent<Items>();
            foundedItemType = items.itemType;
            Debug.Log("Item is here");
            if (foundedItemType == ItemType.Key && actManager.actLevel.task == 4)
            {
                Debug.Log("Task now " + actManager.actLevel.task);

                isItemHere = true;
                foundedItem = col.gameObject;
                gameManager.ShowItemHere();
            }

            if (foundedItemType == ItemType.PaintTube && actManager.actLevel.task == 1)
            {
                Debug.Log("Task now " + actManager.actLevel.task);
                isItemHere = true;
                foundedItem = col.gameObject;
                gameManager.ShowItemHere();
            }

            if (foundedItemType == ItemType.Pills)
            {
                Debug.Log("Task now " + actManager.actLevel.task);

                isItemHere = true;
                foundedItem = col.gameObject;
                gameManager.ShowItemHere();
            }

            if (foundedItemType == ItemType.Bottle && actManager.actLevel.act == 1 && actManager.actLevel.task == 0)
            {
                isItemHere = true;
                foundedItem = col.gameObject;
                gameManager.ShowItemHere();
            }
        }

        if (col.tag == "Easel" && PlayerPrefs.GetInt("PaintTube") == 1 && actManager.actLevel.act == 0 &&
            actManager.actLevel.task != 2)
        {
            Debug.Log("Easel is here");
            isEaselHere = true;
            gameManager.ShowInteract();
        }

        if (col.tag == "Easel" && PlayerPrefs.GetInt("PaintTube") == 1 && actManager.actLevel.act == 2 &&
            actManager.actLevel.task == 0)
        {
            Debug.Log("Easel is here");
            isEaselHere = true;
            gameManager.ShowInteract();
        }

        if (col.tag == "Door" && PlayerPrefs.GetInt("Key") == 1)
        {
            gameManager.ShowDoorOpen();
            isDoorHere = true;
        }

        if (col.tag == "WalleyToHome")
        {
            gameManager.ShowWalleyExit();
            isWalleyToHome = true;
        }

        if (col.tag == "Phone" && PlayerPrefs.GetInt("Phone") != 2)
        {
            isPhoneHere = true;
            gameManager.ShowInteract();
        }

        if (col.tag == "Phone" && actManager.actLevel.act == 0 && actManager.actLevel.task == 3)
        {
            isPhoneHere = true;
            gameManager.ShowInteract();
        }

        if (col.tag == "Note" && actManager.actLevel.act == 0 && actManager.actLevel.task == 2)
        {
            isNoteHere = true;
            foundedItem = col.gameObject;
            gameManager.ShowItemHere();
        }

        if (col.tag == "Janna" && PlayerPrefs.GetInt("Bottle") == 1)
        {
            isJannaHere = true;
            gameManager.ShowInteract();
        }

        if (col.tag == "BarDoor" && PlayerPrefs.GetInt("Richard") == 1)
        {
            isBarDoorHere = true;
            gameManager.ShowDoorOpen();
        }

        if (col.tag == "BarDoor" && actManager.actLevel.act == 3 && actManager.actLevel.task == 1)
        {
            isBarDoorHere = true;
            gameManager.ShowDoorOpen();
        }

        if (col.tag == "Richard")
        {
            isRichardHere = true;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Coat" && actManager.actLevel.act == 3 && PlayerPrefs.GetInt("Coat") != 1)
        {
            isCoatHere = false;
            gameManager.HideItemHere();
        }

        if (col.tag == "Richard")
        {
            isRichardHere = false;
        }

        if (col.tag == "Item")
        {
            Items items = col.gameObject.GetComponent<Items>();
            foundedItemType = items.itemType;
            Debug.Log("Item is here");
            if (foundedItemType == ItemType.Key && actManager.actLevel.task == 4)
            {
                Debug.Log("Task now " + actManager.actLevel.task);

                isItemHere = false;
                foundedItem = col.gameObject;
                gameManager.HideItemHere();
            }

            if (foundedItemType == ItemType.PaintTube && actManager.actLevel.task == 1)
            {
                Debug.Log("Task now " + actManager.actLevel.task);
                isItemHere = false;
                foundedItem = col.gameObject;
                gameManager.HideItemHere();
            }

            if (foundedItemType == ItemType.Pills)
            {
                Debug.Log("Task now " + actManager.actLevel.task);

                isItemHere = false;
                foundedItem = col.gameObject;
                gameManager.HideItemHere();
            }

            if (foundedItemType == ItemType.Bottle && actManager.actLevel.act == 1 && actManager.actLevel.task == 0)
            {
                isItemHere = false;
                foundedItem = null;
                gameManager.HideItemHere();
            }
        }

        if (col.tag == "Easel" && PlayerPrefs.GetInt("PaintTube") == 1 && actManager.actLevel.act == 2 &&
            actManager.actLevel.task == 0)
        {
            Debug.Log("Easel is here");
            isEaselHere = false;
            gameManager.HideInteract();
        }

        if (col.tag == "Door" && PlayerPrefs.GetInt("Key") == 1)
        {
            gameManager.HideDoorOpen();
            isDoorHere = false;
        }

        if (col.tag == "WalleyToHome")
        {
            gameManager.HideWalleyExit();
            isWalleyToHome = false;
        }

        if (col.tag == "Phone" && PlayerPrefs.GetInt("Phone") != 2)
        {
            isPhoneHere = false;
            gameManager.HideInteract();
        }

        if (col.tag == "Phone" && actManager.actLevel.act == 0 && actManager.actLevel.task == 3)
        {
            isPhoneHere = false;
            gameManager.HideInteract();
        }

        if (col.tag == "Note" && actManager.actLevel.act == 0 && actManager.actLevel.task == 2)
        {
            isNoteHere = false;
            foundedItem = null;
            gameManager.HideItemHere();
        }

        if (col.tag == "Janna" && PlayerPrefs.GetInt("Bottle") == 1)
        {
            isJannaHere = false;
            gameManager.HideInteract();
        }

        if (col.tag == "BarDoor" && PlayerPrefs.GetInt("Richard") == 1)
        {
            isBarDoorHere = false;
            gameManager.HideDoorOpen();
        }
    }
}
//