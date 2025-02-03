using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float interactionRange = 7f;
    private Transform player;
    private Renderer objectRenderer;
    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) <= interactionRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && PlayerMovement.currentDangerIndex == 0)
            {
                objectRenderer.material.color = Color.green;
            }
        }
    }
}
