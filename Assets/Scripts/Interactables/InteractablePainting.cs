using UnityEngine;
using UnityEngine.Video;
using System.Threading.Tasks;

public class InteractablePainting : InteractableWithSound
{
    [SerializeField]
    VideoClip video360;

    [SerializeField]
    GameObject player;

    [SerializeField]
    Animator playerCanvas;

    [SerializeField]
    Transform sphere360VideoTransform;

    [SerializeField]
    GameObject prizeIfThereIsNoVideo;

    [SerializeField]
    GameObject ejector;

    [SerializeField]
    int videoSecondsDuration;

    [SerializeField]
    float volume = 1;

    [SerializeField]
    AudioClip audioAfterSendingPlayerBack;

    private int milisecondsBeforeTeleporting = 1000;

    public override void Interact()
    {
        base.Interact();
        if (video360)
        {
            TakePlayerToSee360Video();
        }
    }

    private async void TakePlayerToSee360Video()
    {
        playerCanvas.SetTrigger("fadeToWhite");
        await Task.Delay(milisecondsBeforeTeleporting);
        sphere360VideoTransform.GetComponent<VideoPlayer>().clip = video360;
        sphere360VideoTransform.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, volume);
        sphere360VideoTransform.GetComponent<Sphere360>().SetPlayerTransform(player.transform);
        sphere360VideoTransform.GetComponent<Sphere360>().SetSecondsToFinishVideo(this.videoSecondsDuration);
        player.transform.position = sphere360VideoTransform.position;
        player.GetComponent<CharacterController>().enabled = false;
        sphere360VideoTransform.gameObject.SetActive(true);
        await Task.Delay(((int)sphere360VideoTransform.GetComponent<Sphere360>().secondsToFinishVideo * 1000) + 3000);
        ActivateEjector();
        if(audioAfterSendingPlayerBack != null)
        {
            this.SetDialogue(audioAfterSendingPlayerBack);
            this.Speak();
        }
    }

    private void ActivateEjector() {
        if(ejector)
        {
            ejector.SetActive(true);
        }
    }
}