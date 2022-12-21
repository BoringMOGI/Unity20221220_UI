using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    [SerializeField] TMP_Text questionText;
    [SerializeField] Toggle[] toggles;

    int index;
    public void OnPress(int index)
    {
        this.index = index;
        Debug.Log($"{index}번째를 선택했다.");
    }
}
