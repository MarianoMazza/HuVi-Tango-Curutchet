using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class Gaze : MonoBehaviour
{
    [SerializeField]
    MOVER mover;

    [SerializeField]
    DemoPlayerController demoPlayerController;

    [SerializeField]
    AudioSource audioSource;

    public Animator objectAnimator;
    public float myTime = 0f;
    public Transform radialPorgress;
    public bool gazeTimerUp = false;
    public GameObject text;
    Interactable seenObjectScript;

    private void Start()
    {
        objectAnimator = GetComponent<Animator>();
        radialPorgress.GetComponent<Image>().fillAmount = myTime;
    }

    private void Update()
    {
        bool isNotListeningToAudio = audioSource== null || !audioSource.isPlaying;
        if (isNotListeningToAudio) {
            myTime += Time.deltaTime;
            radialPorgress.GetComponent<Image>().fillAmount = myTime / 2;
            if (myTime >= 2f && !gazeTimerUp)
            {
                Interact();
            }
        }
    }

    private void OnEnable()
    {
        mover.enabled = false;
    }

    public async void ResetCounter()
    {
        gazeTimerUp = false;
        myTime = 0f;
        radialPorgress.GetComponent<Image>().fillAmount = myTime;
        await Task.Delay(70);
        if (demoPlayerController) 
        {
            demoPlayerController.enabled = true;
        }
        mover.enabled = true;
    }

    public void Interact()
    {
        gazeTimerUp = true;
        seenObjectScript.Interact();
        this.ResetCounter();
        this.enabled = false;
    }

    public void SetSeenObjectScript(Interactable interactableScript)
    {
        seenObjectScript = interactableScript;
    }

    public void SetAudioSource(AudioSource audioSource)
    {
        this.audioSource = audioSource;
    }
}