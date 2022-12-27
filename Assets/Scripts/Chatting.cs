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
     *  1.입력창을 활성화한다.
        2.텍스트를 입력한다.
        - 입력O : 문자열을 가져와 텍스트 생성
        - 입력X : 아무것도 안함
        3.생성된 텍스트를 Content의 자식으로 대입.
        - 자동으로 ContentSizeFitter가 사이즈를 계산
        - VerticalLayoutGroup이 자식의 위치를 조정. 


        입력 상태가 아닐 때 엔터키를 누르면 > 인풋 필드 선택
        인풋필드가 선택된 상태에서 엔터키를 누르면 > 채팅 치기
        아무것도 입력이 안된 상태에서 엔터키를 누르면 > 입력 종료
     */

    private void Start()
    {
        eventSystem = EventSystem.current;
    }

    public void OnEndEdit(string str)
    {
        // 입력된 문자가 빈 문자가 아닐 때 처리.
        if (!string.IsNullOrEmpty(str))
        {
            inputField.text = string.Empty;
            TMP_Text newText = Instantiate(recordPrefab, scrollView.content);
            newText.text = str;
            inputField.OnSelect(null);      // 일반 Select는 비활성화 상태를 바꿔주지 않는다.
        }
        // 빈문자가 들어왔다는 말은 입력을 취소하겠다는 말이다.
        else if(eventSystem.currentSelectedGameObject == inputField.gameObject)
        {
            eventSystem.SetSelectedGameObject(null);
        }
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return))
        {
            // 현재 선택중인 게임 오브젝트가 인풋 필드랑 다를 경우.
            if(eventSystem.currentSelectedGameObject != inputField.gameObject)
            {
                inputField.Select();
            }
        }
    }
}
