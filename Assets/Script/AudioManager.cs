using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume = 0.1f;  // 기본 볼륨 설정
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume = 0.2f;  // 기본 볼륨 설정
    public float bossAttackVolume = 0.01f;
    public int channels = 10;       // 기본 채널 수 설정
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Attack, dash, Death, Hit, Jump, Run, destroy, Boss_Attack, black_out };

    void Awake()
    {
        instance = this;
        Init();
        PlayBGM(); // 배경음악 자동 재생
    }

    void Init()
    {
        // 배경음 플레이어 초기화
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // 효과음 플레이어 초기화
        GameObject sfxObject = new GameObject("SfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            sfxPlayers[index] = sfxObject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }
    }

    // BGM 재생 메서드
    public void PlayBGM()
    {
        if (bgmClip != null)
        {
            bgmPlayer.Play();
        }
    }

    public void StopBGM()
    {
        bgmPlayer.Stop();
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
            {
                continue;
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;
        }
    }

    // 새로운 메서드: 효과음 멈추기
    public void StopSfx(Sfx sfx)
    {
        for (int index = 0; index < sfxPlayers.Length; index++)
        {
            if (sfxPlayers[index].clip == sfxClips[(int)sfx] && sfxPlayers[index].isPlaying)
            {
                sfxPlayers[index].Stop();
                break;
            }
        }
    }
}
