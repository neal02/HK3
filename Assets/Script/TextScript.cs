using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // TextMeshPro 사용을 위한 네임스페이스

public class DialogueController : MonoBehaviour
{
    public string[] dialogueLines= { "dsfajhsjkdahfshdfhsadiofhdsoihfoh",
        "asifugsudfgsydgufsgsgufiahsidhasuifhisudhsfdihfihsdfihfds",
        "adas"
    }; // 대화 내용
    public TextMeshProUGUI dialogueText; // TextMeshProUGUI 컴포넌트
    public GameObject player; // 플레이어 오브젝트
    public float interactionDistance = 2f; // 대화 가능 거리

    private int currentLineIndex = 0; // 현재 진행 중인 대화 줄
    private bool isInteracting = false; // 대화 중인지 여부
    private Transform playerTransform; // 플레이어 위치


    private void Start()
    {
        playerTransform = player.transform;
        dialogueText.text = ""; // 초기화
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        // 2D 거리 계산 (z 축은 무시)
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
        currentLineIndex = 0; // 대화 시작 시 초기화
        player.GetComponent<playerControl>().StopMovement(); // 이동 멈추기
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
        dialogueText.text = ""; // 대화창 비우기
        isInteracting = false;
        player.GetComponent<playerControl>().ResumeMovement(); // 이동 재개
    }
}
