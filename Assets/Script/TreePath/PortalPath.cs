using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalPath : MonoBehaviour
{
    public GameObject PortalIcon;
    private bool isRange = false;

    void Start()
    {
        PortalIcon.SetActive(false);
    }

    void Update()
    {
        if (isRange && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("TreeScenes");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PortalIcon.SetActive(true);
            isRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PortalIcon.SetActive(false);
            isRange = false;
        }
    }
}