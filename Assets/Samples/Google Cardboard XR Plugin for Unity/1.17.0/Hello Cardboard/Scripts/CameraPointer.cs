using UnityEngine;

public class CameraPointer : MonoBehaviour
{
    private const float _maxDistance = 10;
    private GameObject _gazedAtObject = null;
    public GvrReticlePointer gvrReticlePointer;

    public void Update()
    {
        // Casts ray towards camera's forward direction, to detect if a GameObject is being gazed at.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, _maxDistance))
        {
            // GameObject detected in front of the camera.
            if (_gazedAtObject != hit.transform.gameObject)
            {
                ExitGazedObject();
                ResetGazeScript();
                Interactable gazedObjectInteractableScript = hit.transform.gameObject.GetComponent<Interactable>();
                if (gazedObjectInteractableScript != null && gazedObjectInteractableScript.isActiveAndEnabled) {
                    _gazedAtObject = hit.transform.gameObject;
                    this.GetComponent<Gaze>().enabled = true;
                    this.GetComponent<Gaze>().SetSeenObjectScript(gazedObjectInteractableScript);
                    gvrReticlePointer.SetPointerTarget(hit.transform.gameObject.transform.position,true);
                }
            }
        }
        else
        {
            ExitGazedObject();
            ResetGazeScript();
        }
    }

    private void ExitGazedObject()
    {
        gvrReticlePointer.OnPointerExit(_gazedAtObject);
        _gazedAtObject?.SendMessage("OnPointerExit");
        _gazedAtObject = null;
    }

    private void ResetGazeScript()
    {
        if (this.GetComponent<Gaze>().isActiveAndEnabled)
        {
            this.GetComponent<Gaze>().ResetCounter();
            this.GetComponent<Gaze>().enabled = false;
        }
    }
}