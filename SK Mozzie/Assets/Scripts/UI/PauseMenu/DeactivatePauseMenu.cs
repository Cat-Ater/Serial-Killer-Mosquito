using UnityEngine;

public class DeactivatePauseMenu : MonoBehaviour
{
    public void Deactivate()
    {
        UIManager.Instance.ClosePauseMenu();
    }
}