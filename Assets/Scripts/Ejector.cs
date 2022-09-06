using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Ejector : MonoBehaviour
{
    [SerializeField]
    GameObject[] objectsToBeEjected = new GameObject[0];

    [SerializeField]
    int force = 200;

    int ejectedObjectsCounter;
    int pushAngle = 90;
    int pushAngleModification;
    int timeBeforeNextEject = 3;

    private void Start()
    {
        pushAngle = 90 / objectsToBeEjected.Length;
        pushAngleModification = pushAngle;
        StartCoroutine(Eject());
    }

    private IEnumerator Eject()
    {
        if(ejectedObjectsCounter < objectsToBeEjected.Length)
        {
            GameObject objectToBeEjected = objectsToBeEjected[ejectedObjectsCounter];
            objectToBeEjected.SetActive(true);
            Rigidbody rigidbody = objectToBeEjected.GetComponent<Rigidbody>();
            rigidbody.AddForce(CalculatePushDirection() * force);
            ejectedObjectsCounter++;
            yield return new WaitForSeconds(timeBeforeNextEject);
            rigidbody.isKinematic = true;
            StartCoroutine(Eject());
        }
        else
        {
            this.gameObject.GetComponent<Ejector>().enabled = false;
        }
    }

    private Vector3 CalculatePushDirection()
    {
        Vector3 pushDirection = transform.forward;
        if (objectsToBeEjected.Length != 1)
        {
            pushDirection = Quaternion.AngleAxis(pushAngle, transform.up) * transform.forward;
            pushAngle -= pushAngleModification;
        }

        return pushDirection;
    }
}
