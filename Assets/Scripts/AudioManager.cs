using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;          // 오디오 믹서.
    [SerializeField] Image volumnImage;         // 볼륨 이미지.
    [SerializeField] AudioSource audioSource;   // 소리를 내는 스피커.
    [SerializeField] TMP_Text currentTimeText;  // 현재 시간 텍스트.
    [SerializeField] TMP_Text maxTimeText;      // 전체 시간 텍스트.
    [SerializeField] Slider slider;             // 진행도 슬라이더.
    [SerializeField] AudioClip[] clips;         // 노래 배열.

    bool isLockUpdate;                          // 업데이트를 막는가?

    void Start()
    {
        audioSource.clip = null;
        OnMoveMusic(true);
    }

    void Update()
    {
        // float time : 몇 초인지를 float로 나타낸다.
        // int timeSample : 주파수(hz)를 int로 나타낸다.
        TimeSpan timeSpan = TimeSpan.FromSeconds(audioSource.time);         // 초를 대입해 계산한다.
        currentTimeText.text = timeSpan.ToString(@"mm\:ss");

        // 업데이트가 막혀있지 않을 때 value를 갱신한다.
        if (!isLockUpdate)
        {
            slider.value = audioSource.time / audioSource.clip.length;      // 전체 길이 대비 현재 몇 %인가?
            if(audioSource.time >= audioSource.clip.length)                 // 노래가 끝났다.                
                OnMoveMusic(true);                                          // 다음 노래 재생.
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            float volumn = 0f;
            mixer.GetFloat("Bgm", out volumn);
            if (volumn <= -80f)
                volumn = -20f;

            volumn = Mathf.Clamp(volumn + 2f, -20f, 0f);
            volumnImage.fillAmount = (20 + volumn) / 20f;

            mixer.SetFloat("Bgm", volumn);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            float volumn = 0f;
            mixer.GetFloat("Bgm", out volumn);


            // 조절한 볼륨을 이용해 볼륨바를 움직인다.
            volumn = Mathf.Clamp(volumn - 2f, -20f, 0f);
            volumnImage.fillAmount = (20 + volumn) / 20f;

            // 볼륨을 내리다 -20이하가 되면 -80으로 고정한다.
            if (volumn <= -20f)
                volumn = -80f;

            mixer.SetFloat("Bgm", volumn);
        }

    }
    public void OnChangedAudioTime(Slider slider)
    {
        float value = slider.value;
        audioSource.time = audioSource.clip.length * value;     // 슬라이더 비율에 따라 노래 시간 변경.
    }

    // 외부에서 자동 갱신을 막거나 푸는 함수.
    public void SwitchLockUpdate(bool isLock)
    {
        isLockUpdate = isLock;
    }
    public void OnMoveMusic(bool isNext)
    {
        int index = Array.IndexOf(clips, audioSource.clip);         // 클립 배열에서 현재 클립이 몇 번째인가?
        index += (isNext ? 1 : -1);                                 // 값에 따라 index를 더하거나 뺀다.
        if(index < 0 || index >= clips.Length)                      // 범위를 벗어났으면
            index = isNext ? 0 : clips.Length - 1;                  // 값에 따라 범위 조정.

        audioSource.clip = clips[index];                            // 새로운 클립 대입.
        audioSource.time = 0f;                                      // 처음부터 시작.
        audioSource.Play();                                         // 오디오 재생.

        // 현재 오디오 소스에 들어가있는 클립의 전체 길이를 가져와 분,초로 나타낸다.
        TimeSpan maxSpan = TimeSpan.FromSeconds(audioSource.clip.length);
        maxTimeText.text = maxSpan.ToString(@"mm\:ss");
    }
}
