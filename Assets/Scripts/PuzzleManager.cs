using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField]
    int totalObjectives;

    [SerializeField]
    GameObject nextObject;

    int objectiveCount;

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

    protected virtual void DetermineIfObjectiveIsAccomplished()
    {
        if(objectiveCount >= totalObjectives)
        {
            nextObject.SetActive(true);
        }
    }

    public int GetTotalObjectives()
    {
        return totalObjectives;
    }

    public int GetObjectiveCount()
    {
        return objectiveCount;
    }

    public GameObject GetNextObject()
    {
        return nextObject;
    }
}
