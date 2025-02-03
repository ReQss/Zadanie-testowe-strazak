using TMPro;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public float interactionRange = 3f;
    private Transform player;
    private Animator animator;
    public Animator alertAnimator;
    public TextMeshProUGUI alertText;
    public int dangerLevel;
    public TextMeshProUGUI dangerLevelUI;
    private bool isDoorNeutralized = false;
    private bool isDoorDestroyed = false;
    private Renderer objectRenderer;

    void Start()
    {
        // wczytanie komponentow z gry
        objectRenderer = GetComponent<Renderer>();
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        dangerLevel = Random.Range(1, 9);
        dangerLevelUI = GameObject.Find("DangerLevel").GetComponent<TextMeshProUGUI>();
        alertAnimator = GameObject.Find("Alert").GetComponent<Animator>();
        alertText = GameObject.Find("AlertText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {

        if (Vector3.Distance(transform.position, player.position) <= interactionRange)
        {
            // sprawdzanie czy gracz jest blisko i podejmowanie akcji
            if (isDoorDestroyed && Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Te drzwi zostaly zniszczone na zawsze");
                alertText.text = "Te drzwi zostaly zniszczone na zawsze";
                alertAnimator.SetTrigger("openAlert");
                return;
            }
            if (Input.GetKeyDown(KeyCode.E) && isDoorNeutralized)
            {
                Debug.Log("Te drzwi zostaly zneuatrilizowane");
                alertText.text = "Te drzwi zostaly zneuatrilizowane";
                alertAnimator.SetTrigger("openAlert");
                return;
            }
            if (Input.GetKeyDown(KeyCode.E) && (PlayerMovement.currentDangerIndex == dangerLevel))
            {
                Debug.Log("Drzwi są w zasięgu - nacisnąłeś E!");
                alertText.text = "Zneutralizowales drzwi";
                alertAnimator.SetTrigger("openAlert");
                animator.SetTrigger("openDoor");
                isDoorNeutralized = true;
                dangerLevel = 0;
            }
            else if (Input.GetKeyDown(KeyCode.E) && (PlayerMovement.currentDangerIndex != dangerLevel))
            {
                isDoorDestroyed = true;
                Debug.Log("You set wrong danger level and lost health");
                alertText.text = "Ustawiles zly poziom zagrozenia i straciles zycie, drzwi juz na zawsze pozostana zamkniete";
                objectRenderer.material.color = Color.red;
                alertAnimator.SetTrigger("openAlert");
                PlayerMovement.health -= 1;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Danger level is" + dangerLevel);

                dangerLevelUI.text = dangerLevel.ToString();
                alertText.text = "Te drzwi zostaly zneuatrilizowane";
                alertAnimator.SetTrigger("Poziom zagrozenia to " + dangerLevel);
            }

        }

    }
}
