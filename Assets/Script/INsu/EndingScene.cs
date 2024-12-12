using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndingScene : MonoBehaviour
{


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene("TutoScene");

        }
        if (Input.GetKeyDown(KeyCode.Escape)) {
            QuitGame();
        }
    }
    void QuitGame()
    {
        // ������ ����� ���¿����� �۵�
        Application.Quit();

        // �����Ϳ����� ������ �����ϴ� �ڵ�
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
