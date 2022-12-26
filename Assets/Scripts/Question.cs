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
        // => �� �ڽ� ������Ʈ �߿��� T ������Ʈ�� �˻��� �����Ѵ�.
        //    ������ null�̰� ���� ������ ������ ���� �߰��� �ϳ��� ����.

        /*
        // transform�� �ڽĵ��� ���������� child�� �����Ѵ�.
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
        // ���� ����� ���õǾ��� �� �˻�.
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
        // ��� ����� ���õǾ� ������ Alpha���� 0.6f�� ��������.
        canvasGroup.alpha = (selected < 0) ? 1.0f : 0.6f;
    }
}
