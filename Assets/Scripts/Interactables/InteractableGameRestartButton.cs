using UnityEngine.SceneManagement;

public class InteractableGameRestartButton : Interactable
{
    public override void Interact()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
