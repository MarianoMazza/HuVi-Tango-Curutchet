using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    sbyte totalObjectives;

    [SerializeField]
    GameObject nextObject;

    sbyte objectiveCount;

    public void IncreaseObjectiveCount()
    {
        objectiveCount++;
        DetermineIfObjectiveIsAccomplished();
    }

    public void DecreaseObjectiveCount()
    {
        objectiveCount--;
        DetermineIfObjectiveIsAccomplished();
    }

    private void DetermineIfObjectiveIsAccomplished()
    {
        if(objectiveCount >= totalObjectives)
        {
            nextObject.SetActive(true);
        }
    }
}
