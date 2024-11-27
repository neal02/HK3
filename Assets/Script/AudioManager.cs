using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume = 0.1f;  // �⺻ ���� ����
    AudioSource bgmPlayer;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume = 0.2f;  // �⺻ ���� ����
    public float bossAttackVolume = 0.01f;
    public int channels = 10;       // �⺻ ä�� �� ����
    AudioSource[] sfxPlayers;
    int channelIndex;

    public enum Sfx { Attack, dash, Death, Hit, Jump, Run, destroy, Boss_Attack, black_out };

    void Awake()
    {
        instance = this;
        Init();
        PlayBGM(); // ������� �ڵ� ���
    }

    void Init()
    {
        // ����� �÷��̾� �ʱ�ȭ
        GameObject bgmObject = new GameObject("BgmPlayer");
        bgmObject.transform.parent = transform;
        bgmPlayer = bgmObject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;

        // ȿ���� �÷��̾� �ʱ�ȭ
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

    // BGM ��� �޼���
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

    // ���ο� �޼���: ȿ���� ���߱�
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
