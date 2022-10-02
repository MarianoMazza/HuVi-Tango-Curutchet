using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManagerFinalTango : PuzzleManager
{
    [SerializeField]
    LevelManager levelManager;

    protected override void DetermineIfObjectiveIsAccomplished()
    {
        if (this.GetObjectiveCount() >= this.GetTotalObjectives())
        {
            this.GetNextObject().SetActive(true);
            levelManager.EndTangoRoom();
        }
    }
}
