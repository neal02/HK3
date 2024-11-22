using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro ����� ���� ���ӽ����̽�

public class DialogueController : MonoBehaviour
{
    public string[] dialogueLines= { "dsfajhsjkdahfshdfhsadiofhdsoihfoh",
        "asifugsudfgsydgufsgsgufiahsidhasuifhisudhsfdihfihsdfihfds",
        "adas"
    }; // ��ȭ ����
    public TextMeshProUGUI dialogueText; // TextMeshProUGUI ������Ʈ
    public GameObject player; // �÷��̾� ������Ʈ
    public float interactionDistance = 2f; // ��ȭ ���� �Ÿ�

    private int currentLineIndex = 0; // ���� ���� ���� ��ȭ ��
    private bool isInteracting = false; // ��ȭ ������ ����
    private Transform playerTransform; // �÷��̾� ��ġ


    private void Start()
    {
        playerTransform = player.transform;
        dialogueText.text = ""; // �ʱ�ȭ
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        // 2D �Ÿ� ��� (z ���� ����)
        float distance = Vector2.Distance(playerTransform.position, transform.position);

        if (distance <= interactionDistance && Input.GetKeyDown(KeyCode.Return) && !isInteracting) {
            StartDialogue();
        }
        else if (isInteracting && Input.GetKeyDown(KeyCode.Return)) {
            ShowNextLine();
        }
    }

    private void StartDialogue()
    {
        isInteracting = true;
        currentLineIndex = 0; // ��ȭ ���� �� �ʱ�ȭ
        player.GetComponent<playerControl>().StopMovement(); // �̵� ���߱�
        ShowNextLine();
    }

    private void ShowNextLine()
    {
        if (currentLineIndex < dialogueLines.Length) {
            dialogueText.text = dialogueLines[currentLineIndex];
            currentLineIndex++;
        }
        else {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        dialogueText.text = ""; // ��ȭâ ����
        isInteracting = false;
        player.GetComponent<playerControl>().ResumeMovement(); // �̵� �簳
    }
}
