using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableWithSound : Interactable
{
    [SerializeField]
    AudioClip dialogue;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    GameObject nextObject;

    [SerializeField]
    bool activateNextAnimation;

    [SerializeField]
    float timeToAnimateNext;

    [SerializeField]
    float timeToAnimateThisObject;

    [SerializeField]
    GameObject rangerPosition;

    [SerializeField]
    GameObject ranger;

    public override void Interact()
    {
        RepositionRanger();
        Speak();
        StartCoroutine(AnimateAfterTime(gameObject, timeToAnimateThisObject));
        SpawnNextObject();
        DisableThisCollider();
    }

    public void DisableThisCollider()
    {
        gameObject.GetComponent<Collider>().enabled = false;
    }

    public void RepositionRanger()
    {
        if (rangerPosition != null && ranger != null)
        {
            ranger.transform.position = new Vector3(rangerPosition.transform.position.x, ranger.transform.position.y, rangerPosition.transform.position.z);
            ranger.transform.forward = new Vector3(rangerPosition.transform.forward.x, ranger.transform.forward.y, rangerPosition.transform.forward.z);
        }
    }

    public void SpawnNextObject()
    {
        if (nextObject != null)
        {
            nextObject.SetActive(true);
            if (activateNextAnimation)
            {
                StartCoroutine(AnimateAfterTime(nextObject, timeToAnimateNext));
            }
        }
    }

    private IEnumerator AnimateAfterTime(GameObject gameObject, float time)
    {
        if (gameObject.GetComponent<Animator>())
        {
            yield return new WaitForSeconds(time);
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    public void AnimateNextObject()
    {
        nextObject.GetComponent<Animator>().enabled = true;
    }

    public void Speak()
    {
        if (dialogue != null)
        {
            audioSource.clip = dialogue;
            audioSource.Play();
        }
    }

    public void SetDialogue(AudioClip _dialogue)
    {
        dialogue = _dialogue;
    }

    public AudioClip GetDialogue()
    {
        return dialogue;
    }

    public AudioSource GetAudioSource()
    {
        return audioSource;
    }
}