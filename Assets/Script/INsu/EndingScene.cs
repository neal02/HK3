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
        // 게임이 빌드된 상태에서만 작동
        Application.Quit();

        // 에디터에서는 게임을 종료하는 코드
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
