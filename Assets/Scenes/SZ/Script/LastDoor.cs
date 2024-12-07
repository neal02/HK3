using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LastDoor : MonoBehaviour
{
    BoxCollider2D BoxCollider2D;

    public GameObject SpaceClick;

    private bool isRange = false;
    void Start()
    {
        BoxCollider2D = GetComponent<BoxCollider2D>();
        SpaceClick.SetActive(false);
        BoxCollider2D.enabled = false;
    }

    void Update()
    {
        if(transform.position.y >= -4f)
        {
            BoxCollider2D.enabled = true;
        }

        if (isRange && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("GateScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpaceClick.SetActive(true);
            isRange = true;
        }
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SpaceClick.SetActive(false);
            isRange = false;
        }
    }
}
