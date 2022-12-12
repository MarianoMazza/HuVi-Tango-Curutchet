using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class Sphere360 : MonoBehaviour {
	
	VideoPlayer video;
	public float secondsToFinishVideo;
    Transform playerTransform;
    Vector3 originalPlayerPosition;

    void OnEnable()
    {
        video = GetComponent<VideoPlayer>();
        video.Prepare();
        video.Play();
        StartCoroutine(TakePlayerBackToPreviousSpot(secondsToFinishVideo));
    }

    IEnumerator TakePlayerBackToPreviousSpot(float secondsToFinishVideo)
    {
        yield return new WaitForSeconds(secondsToFinishVideo);
        playerTransform.gameObject.transform.position = this.originalPlayerPosition;
        playerTransform.GetComponent<CharacterController>().enabled = true;
        video.Stop();
        this.gameObject.SetActive(false);
    }

    public void SetPlayerTransform(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
        this.originalPlayerPosition = playerTransform.position;
    }

    public void SetSecondsToFinishVideo(int secondsToFinishVideo)
    {
        this.secondsToFinishVideo = secondsToFinishVideo;
    }

}
