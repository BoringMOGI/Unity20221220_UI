using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    [SerializeField] TMP_Text queastionText;

    Toggle[] toggles = null;
    CanvasGroup canvasGroup;
    int selected = -1;

    public bool isSelected => selected >= 0;
    public int Selected => selected;

    private void Awake()
    {
        /*
        toggles = new Toggle[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
            toggles[i] = transform.GetChild(i).GetComponent<Toggle>();
        */

        // transform.GetComponentInChildren<T>
        // => 내 자식 오브젝트 중에서 T 컴포넌트를 검색해 리턴한다.
        //    없으면 null이고 만약 여러개 있으면 먼저 발견한 하나를 리턴.

        /*
        // transform의 자식들을 순차적으로 child에 대입한다.
        foreach(GameObject child in transform)
        {
            Debug.Log(child.name);
        }         
        */

        toggles = transform.GetComponentsInChildren<Toggle>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void SetupText(string queastion)
    {
        queastionText.text = queastion;
    }
    public void PressToggle()
    {
        // 무언가 토글이 선택되었을 때 검색.
        selected = -1;
        for(int i = 0; i< toggles.Length; i++)
        {
            if (toggles[i].isOn)
            {
                selected = i;
                break;
            }
        }

        Debug.Log(selected);
        // 어떠한 토글이 선택되어 있으면 Alpha값을 0.6f로 조정하자.
        canvasGroup.alpha = (selected < 0) ? 1.0f : 0.6f;
    }
}
