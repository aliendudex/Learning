using UnityEngine;
using UnityEngine.InputSystem;

public class StartScreen : MonoBehaviour
{
    // Assign the panel in the Inspector
    public GameObject startScreen;
    private bool gameStarted = false;

    private void Start()
    {
        // Pause game time
        Time.timeScale = 0f;
        startScreen.SetActive(true);
    }

    private void Update()
    {
        if (!gameStarted && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            gameStarted = true;
            startScreen.SetActive(false);
            // Resume game
            Time.timeScale = 1f;
        }
    }
}
