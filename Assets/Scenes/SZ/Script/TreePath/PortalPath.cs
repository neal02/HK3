using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalPath : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;

    public GameObject PortalIcon;
    public GameObject FlowerPrefab;
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

        spriteRenderer.color = new Color(0, 0, 0);
        animator.enabled = false;
        pos = new Vector3[2];
        PortalIcon.SetActive(false);
        pos[0] = new Vector3(16, -3.5f, 0);
        pos[1] = new Vector3(10, -3f, 0);
    }

    void Update()
    {
        if (PathCondition == 21) {
            animator.enabled = true;
            spriteRenderer.color = new Color(1, 1, 1);
        }
        else if (PathCondition == 42) {
            animator.SetBool("isNext", true);
        }
        if (isRange && Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("TreeScenes");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            if (isFirst && PathCondition == 21) {
                isFirst = false;
                StartCoroutine(SpawnDelay());
            }
            if (PathCondition == 42) {
                PortalIcon.SetActive(true);
                SpaceClick.SetActive(true);
                isRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            SpaceClick.SetActive(false);
            PortalIcon.SetActive(false);
            isRange = false;
        }
    }
    IEnumerator SpawnDelay()
    {
        Instantiate(FlowerPrefab, pos[0], rotation);
        yield return new WaitForSeconds(1.5f);
        Instantiate(FlowerPrefab, pos[1], rotation);
        yield return new WaitForSeconds(0.75f);
    }
}