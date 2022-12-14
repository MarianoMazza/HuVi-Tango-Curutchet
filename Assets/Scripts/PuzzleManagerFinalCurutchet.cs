using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerFinalCurutchet : PuzzleManager
{
    [SerializeField]
    LevelManager levelManager;

    protected override void DetermineIfObjectiveIsAccomplished()
    {
        if (this.GetObjectiveCount() >= this.GetTotalObjectives())
        {
            this.GetNextObject().SetActive(true);
            levelManager.EndCurutchetRoom();
        }
    }
}
