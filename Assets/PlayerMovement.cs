using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraTransform;

    public float speed = 5f;
    public float damping = 0.1f;

    private Vector3 velocity;
    private Vector3 moveDirection;
    public static int health = 3;
    public static int currentDangerIndex = 1;
    public TextMeshProUGUI currentHealth;

    public TextMeshProUGUI currentDangerLevelUI;
    private void Start()
    {
        currentDangerLevelUI = GameObject.Find("CurrentDangerLevel").GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (health == 0)
        {
            health = 3;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        currentHealth.text = health.ToString();
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();
        Vector3 targetDirection = (forward * moveZ + right * moveX).normalized;
        if (moveX != 0 || moveZ != 0)
        {
            moveDirection = targetDirection;
        }
        else
        {
            moveDirection *= damping;
        }
        controller.Move(moveDirection * speed * Time.deltaTime);

        controller.Move(velocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            currentDangerIndex++;
            if (currentDangerIndex > 9) currentDangerIndex = 0;
            currentDangerLevelUI.text = currentDangerIndex.ToString();
        }
    }
}
