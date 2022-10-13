using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManiacRun : MonoBehaviour
{
    private bool isRun;
    private Animator animator;
    private ActManager actManager;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        isRun = true;
        animator = GetComponent<Animator>();
        actManager = GameObject.Find("ActManager").GetComponent<ActManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isRun)
        {
            transform.Translate(Vector3.right * 15 * Time.deltaTime);
        }

        if (DialogueManager.GetInstance().dialogIsOver)
        {
            StartCoroutine(Scary());
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            isRun = false;
            animator.SetTrigger("stay");
            DialogueManager.GetInstance().EnterDialogueMode(gameManager.sixScene);
            actManager.actLevelSD.Save(4,0);
        }
    }

    private IEnumerator Scary()
    {
        gameManager.StartTheEnd();
        AudioManager.GetInstance().Scary();
        yield return new WaitForSeconds(AudioManager.GetInstance().lenght);
    }
}
