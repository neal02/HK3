using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalPath : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    public GameObject PortalIcon;
    public GameObject SpaceClick;

    private bool isFirst = true;
    private bool isRange = false;

    public static int PathCondition = 0;

    Quaternion rotation = Quaternion.identity;
    Vector3[] pos;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();


    }

    void Update()
    {

        if (isRange && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("TreeScenes");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        PortalIcon.SetActive(true);
        SpaceClick.SetActive(true);
        isRange = true;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            SpaceClick.SetActive(false);
            PortalIcon.SetActive(false);
            isRange = false;
        }
    }
}