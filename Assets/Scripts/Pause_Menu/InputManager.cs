using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    // Standardbelegungen
    public KeyCode moveForward = KeyCode.W;
    public KeyCode moveBackward = KeyCode.S;
    public KeyCode moveLeft = KeyCode.A;
    public KeyCode moveRight = KeyCode.D;

    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // optional, wenn du den InputManager über Szenen hinweg behalten möchtest
    }
    else
    {
        Destroy(gameObject);
    }
}
}
