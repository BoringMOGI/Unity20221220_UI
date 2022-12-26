using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;   // �Ҹ��� ���� ����Ŀ.
    [SerializeField] TMP_Text currentTimeText;  // ���� �ð� �ؽ�Ʈ.
    [SerializeField] TMP_Text maxTimeText;      // ��ü �ð� �ؽ�Ʈ.
    [SerializeField] Slider slider;             // ���൵ �����̴�.
    [SerializeField] AudioClip[] clips;         // �뷡 �迭.

    bool isLockUpdate;                          // ������Ʈ�� ���°�?

    void Start()
    {
        audioSource.clip = null;
        OnMoveMusic(true);
    }

    void Update()
    {
        // float time : �� �������� float�� ��Ÿ����.
        // int timeSample : ���ļ�(hz)�� int�� ��Ÿ����.
        TimeSpan timeSpan = TimeSpan.FromSeconds(audioSource.time);         // �ʸ� ������ ����Ѵ�.
        currentTimeText.text = timeSpan.ToString(@"mm\:ss");

        // ������Ʈ�� �������� ���� �� value�� �����Ѵ�.
        if (!isLockUpdate)
        {
            slider.value = audioSource.time / audioSource.clip.length;      // ��ü ���� ��� ���� �� %�ΰ�?
            if(audioSource.time >= audioSource.clip.length)                 // �뷡�� ������.                
                OnMoveMusic(true);                                          // ���� �뷡 ���.
        }
    }
    public void OnChangedAudioTime(Slider slider)
    {
        float value = slider.value;
        audioSource.time = audioSource.clip.length * value;     // �����̴� ������ ���� �뷡 �ð� ����.
    }

    // �ܺο��� �ڵ� ������ ���ų� Ǫ�� �Լ�.
    public void SwitchLockUpdate(bool isLock)
    {
        isLockUpdate = isLock;
    }
    public void OnMoveMusic(bool isNext)
    {
        int index = Array.IndexOf(clips, audioSource.clip);         // Ŭ�� �迭���� ���� Ŭ���� �� ��°�ΰ�?
        index += (isNext ? 1 : -1);                                 // ���� ���� index�� ���ϰų� ����.
        if(index < 0 || index >= clips.Length)                      // ������ �������
            index = isNext ? 0 : clips.Length - 1;                  // ���� ���� ���� ����.

        audioSource.clip = clips[index];                            // ���ο� Ŭ�� ����.
        audioSource.time = 0f;                                      // ó������ ����.
        audioSource.Play();                                         // ����� ���.

        // ���� ����� �ҽ��� ���ִ� Ŭ���� ��ü ���̸� ������ ��,�ʷ� ��Ÿ����.
        TimeSpan maxSpan = TimeSpan.FromSeconds(audioSource.clip.length);
        maxTimeText.text = maxSpan.ToString(@"mm\:ss");
    }
}
