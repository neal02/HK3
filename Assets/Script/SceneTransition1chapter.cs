using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransition1chapter : MonoBehaviour
{
    public Image fadeImage; // 검은 이미지를 참조
    public float fadeDuration = 1.5f; // 페이드 시간

    private void Start()
    {
        // 처음 씬 시작 시 페이드 인 (화면 밝아지기)
        StartCoroutine(FadeIn());
    }

    public void EndBattleAndFadeOut(string nextSceneName)
    {
        // 페이드 아웃 후 씬 전환
        StartCoroutine(FadeOut(nextSceneName));
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1f - (elapsedTime / fadeDuration); // 알파 값 감소
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0f; // 완전히 투명하게
        fadeImage.color = color;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")) {
            StartCoroutine(FadeOut("Main"));

        }
    }

    IEnumerator FadeOut(string nextSceneName)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        AudioManager.instance.PlaySfx(AudioManager.Sfx.black_out);
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = elapsedTime / fadeDuration; // 알파 값 증가
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1f; // 완전히 불투명하게
        fadeImage.color = color;

        // 씬 전환
        SceneManager.LoadScene("Main");
    }
}
