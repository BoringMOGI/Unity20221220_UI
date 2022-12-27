using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Chatting : MonoBehaviour
{
    [SerializeField] ScrollRect scrollView;
    [SerializeField] TMP_InputField inputField;
    [SerializeField] TMP_Text recordPrefab;

    EventSystem eventSystem;

    /*
     *  1.�Է�â�� Ȱ��ȭ�Ѵ�.
        2.�ؽ�Ʈ�� �Է��Ѵ�.
        - �Է�O : ���ڿ��� ������ �ؽ�Ʈ ����
        - �Է�X : �ƹ��͵� ����
        3.������ �ؽ�Ʈ�� Content�� �ڽ����� ����.
        - �ڵ����� ContentSizeFitter�� ����� ���
        - VerticalLayoutGroup�� �ڽ��� ��ġ�� ����. 


        �Է� ���°� �ƴ� �� ����Ű�� ������ > ��ǲ �ʵ� ����
        ��ǲ�ʵ尡 ���õ� ���¿��� ����Ű�� ������ > ä�� ġ��
        �ƹ��͵� �Է��� �ȵ� ���¿��� ����Ű�� ������ > �Է� ����
     */

    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    public void OnEndEdit(string str)
    {
        // �Էµ� ���ڰ� �� ���ڰ� �ƴ� �� ó��.
        if (!string.IsNullOrEmpty(str))
        {
            inputField.text = string.Empty;
            TMP_Text newText = Instantiate(recordPrefab, scrollView.content);
            newText.text = str;
            inputField.OnSelect(null);      // �Ϲ� Select�� ��Ȱ��ȭ ���¸� �ٲ����� �ʴ´�.
        }
        // ���ڰ� ���Դٴ� ���� �Է��� ����ϰڴٴ� ���̴�.
        else if(eventSystem.currentSelectedGameObject == inputField.gameObject)
        {
            eventSystem.SetSelectedGameObject(null);
        }
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return))
        {
            // ���� �������� ���� ������Ʈ�� ��ǲ �ʵ�� �ٸ� ���.
            if(eventSystem.currentSelectedGameObject != inputField.gameObject)
            {
                inputField.Select();
            }
        }
    }
}
