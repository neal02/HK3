using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class NPC : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public string[] dialogues;
    private int dialogueIndex = 0;
    bool canDialogue = false;
    public Canvas canvas;
    public GameObject exitTextPrefab;
    bool isGameEnd = false;

    void Start()
    {
        dialoguePanel.SetActive(false);
        dialogues[0] = "dialogue 1";
        dialogues[1] = "dialogue 2";
        dialogues[2] = "dialogue 3";
    }

    void Update()
    {
        if (canDialogue)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                if (dialoguePanel.activeSelf)
                {
                    ShowNextDialogue();
                }
                else
                {
                    StartDialogue();
                }
            }
        }

        if (isGameEnd)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("1.Start");

            }
            if (Input.GetKeyDown(KeyCode.Escape)) {
                QuitGame();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canDialogue = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canDialogue = false;
        }
    }

    void StartDialogue()
    {
        dialogueIndex = 0;
        dialoguePanel.SetActive(true);
        dialogueText.text = dialogues[dialogueIndex];
    }

    void ShowNextDialogue()
    {
        dialogueIndex++;

        if (dialogueIndex < dialogues.Length)
        {
            dialogueText.text = dialogues[dialogueIndex];
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false);

        ClearSceneObjects();

        CreateBlackScreen();

        DisplayExitText();

        isGameEnd = true;
    }

    void ClearSceneObjects()
    {
        // 현재 씬의 모든 오브젝트를 찾아 제거
        var objects = FindObjectsOfType<GameObject>();

        foreach (var obj in objects)
        {
            // Canvas는 제거하지 않음
            if (obj.GetComponent<Canvas>() != null || obj == gameObject)
                continue;
            if (obj.GetComponent<Camera>() != null || obj == gameObject)
                continue;

            Destroy(obj); // 오브젝트 제거
        }
    }

    private void CreateBlackScreen()
    {
        // Panel 생성
        GameObject blackScreen = new GameObject("BlackScreen");
        blackScreen.transform.SetParent(canvas.transform, false);

        // Panel 컴포넌트 추가 및 설정
        RectTransform rectTransform = blackScreen.AddComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        Image image = blackScreen.AddComponent<Image>();
        image.color = Color.black; // 검은색 배경
    }

    private void DisplayExitText()
    {
        // "Exit" 텍스트 생성
        GameObject exitText = Instantiate(exitTextPrefab, canvas.transform);

        // 위치 및 텍스트 내용 설정
        RectTransform rectTransform = exitText.GetComponent<RectTransform>();
        rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
        rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
        rectTransform.anchoredPosition = Vector2.zero;

        Text text = exitText.GetComponent<Text>();
        if (text != null)
        {
            text.text = "Exit";
            text.fontSize = 40;
            text.color = Color.white;
            text.alignment = TextAnchor.MiddleCenter;
        }
    }

    void QuitGame()
    {
        // 게임이 빌드된 상태에서만 작동
        Application.Quit();

        // 에디터에서는 게임을 종료하는 코드
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
