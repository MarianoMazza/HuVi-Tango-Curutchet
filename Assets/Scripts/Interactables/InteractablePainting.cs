using UnityEngine;
using UnityEngine.Video;
using System.Threading.Tasks;

public class InteractablePainting : Interactable
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

    private int milisecondsBeforeTeleporting = 1000;

    private void Awake()
    {

    }

    public override void Interact()
    {
        if (video360)
        {
            TakePlayerToSee360Video();
        }
        else if (prizeIfThereIsNoVideo)
        {
            prizeIfThereIsNoVideo.SetActive(true);
        }
        else
        {
            this.GetComponent<Animator>().enabled = true;
        }
    }

    private async void TakePlayerToSee360Video()
    {
        playerCanvas.SetTrigger("fadeToWhite");
        await Task.Delay(milisecondsBeforeTeleporting);
        sphere360VideoTransform.GetComponent<VideoPlayer>().clip = video360;
        sphere360VideoTransform.GetComponent<Sphere360>().SetPlayerTransform(player.transform);
        player.transform.position = sphere360VideoTransform.position;
        player.GetComponent<CharacterController>().enabled = false;
        sphere360VideoTransform.gameObject.SetActive(true);
    }
}