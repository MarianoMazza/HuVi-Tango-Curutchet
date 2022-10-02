using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    GameObject guideTangoRoom;

    [SerializeField]
    GameObject guideCurutchetRoom;

    [SerializeField]
    InteractableWithSound finalGuideTangoRoom;

    [SerializeField]
    InteractableWithSound finalGuideCurutchetRoom;

    [SerializeField]
    AudioClip firstDialogue;

    [SerializeField]
    AudioClip finalDialogue;

    [SerializeField]
    GameObject optionForNextRoomTango;

    [SerializeField]
    GameObject optionToEndGameTango;

    [SerializeField]
    GameObject optionForNextRoomCurutchet;

    [SerializeField]
    GameObject optionToEndGameCurutchet;

    int completedRoomsCount = 0;

    public void EndTangoRoom()
    {
        SelectAppropriateDialogueAndNextObject(finalGuideTangoRoom, optionForNextRoomTango, optionToEndGameTango, guideTangoRoom);
    }

    public void EndCurutchetRoom()
    {
        SelectAppropriateDialogueAndNextObject(finalGuideCurutchetRoom, optionForNextRoomCurutchet, optionToEndGameCurutchet, guideCurutchetRoom);
    }

    private void SelectAppropriateDialogueAndNextObject(InteractableWithSound finalRoomGuide, GameObject optionForNextRoom,
        GameObject optionToEndGame, GameObject roomGuide)
    {
        completedRoomsCount++;
        if (completedRoomsCount == 1)
        {
            finalRoomGuide.SetDialogue(firstDialogue);
            finalRoomGuide.SetNextObject(optionForNextRoom);
        }
        else
        {
            finalRoomGuide.SetDialogue(finalDialogue);
            finalRoomGuide.SetNextObject(optionToEndGame);
        }
        finalRoomGuide.gameObject.SetActive(true);
        roomGuide.SetActive(false);
    }
}