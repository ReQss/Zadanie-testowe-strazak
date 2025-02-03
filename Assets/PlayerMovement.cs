using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;

    public float speed = 5f;

    private Vector3 moveDirection;

    public static int health = 3;
    public static int currentDangerIndex = 1;

    public TextMeshProUGUI currentHealth;
    public TextMeshProUGUI currentDangerLevelUI;

    private void Start()
    {
        // zablokowanie kursora i przypisanie elementow UI
        Cursor.lockState = CursorLockMode.Locked;
        currentDangerLevelUI = GameObject.Find("CurrentDangerLevel").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Restart gry po śmierci
        ResetGame();
        currentHealth.text = health.ToString();
        // ruch postaci
        PlayerMove();

        // Zmiana poziomu regulacji
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            currentDangerIndex++;
            if (currentDangerIndex > 9) currentDangerIndex = 0;
            currentDangerLevelUI.text = currentDangerIndex.ToString();
        }
    }
    public void ResetGame()
    {
        // Restart gry po śmierci
        if (health == 0)
        {
            health = 3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void PlayerMove()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        if (moveX != 0 || moveZ != 0)
        {
            moveDirection = new Vector3(moveX, 0, moveZ).normalized;
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            moveDirection.y = 0;
        }
        else moveDirection = Vector3.zero;


        controller.Move(moveDirection * speed * Time.deltaTime);
    }
}
