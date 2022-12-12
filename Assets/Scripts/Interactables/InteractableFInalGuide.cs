using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFInalGuide : Interactable
{
    [SerializeField]
    AudioClip dialogue;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    GameObject nextObject;

    [SerializeField]
    protected float timeToSpawnNextObject;

    void Start()
    {
        
    }
    public override void Interact()
    {
        Speak();
        StartCoroutine(SpawnNextObject(timeToSpawnNextObject));
        DisableThisCollider();
    }

    public void DisableThisCollider()
    {
        this.gameObject.GetComponent<Collider>().enabled = false;
    }

    public IEnumerator SpawnNextObject(float timeToSpawnNextObject)
    {
        if (nextObject != null)
        {
            yield return new WaitForSeconds(timeToSpawnNextObject);
            nextObject.SetActive(true);
        }
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

    public void SetNextObject(GameObject _nextObject)
    {
        nextObject = _nextObject;
    }
}
