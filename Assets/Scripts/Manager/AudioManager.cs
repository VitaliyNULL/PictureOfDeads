using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public AudioSource audioSource;
    [SerializeField] public AudioClip typingText;
    [SerializeField] public AudioClip phoneCall;
    [SerializeField] public AudioClip phoneRotate;
    [SerializeField] public AudioClip strike1;
    [SerializeField] public AudioClip strike2;
    [SerializeField] public AudioClip scary;
    public float lenght;
    private Coroutine phoneCallTime;
    

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Audio in the scene");
        }

        instance = this;
    }
    public static AudioManager GetInstance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("Phone") != 2)
        {
            if (phoneCallTime == null)
            {
                phoneCallTime = StartCoroutine(StartPhoneCall());
            }
        }
    }

    //methods for call sounds
    public void Scary()
    {
        audioSource.PlayOneShot(scary);
        lenght = scary.length;
    }
    public void StartTyping()
    {
        audioSource.PlayOneShot(typingText);
    }
    public void FinishAudio()
    {
        audioSource.Stop();
    }

    private IEnumerator StartPhoneCall()
    {
        audioSource.PlayOneShot(phoneCall);
        yield return new WaitForSeconds(phoneCall.length);
        phoneCallTime = null;
    }
    public void Strike1()
    {
        audioSource.PlayOneShot(strike1);
    }
    public void Strike2()
    {
        audioSource.PlayOneShot(strike2);
    }


}
