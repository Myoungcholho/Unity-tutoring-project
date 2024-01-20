using System.Collections; //
using System.Collections.Generic; //
using UnityEngine; //
using TMPro;

public class PressEnterKey : MonoBehaviour
{
    private TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        // 만약 스크립트가 추가된 GameObject에 Text 컴포넌트가 없다면 에러를 방지하기 위해 추가합니다.
        text = GetComponent<TextMeshProUGUI>();

        if (text == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on the GameObject or in the script.");
            return;
        }

        StartBlinking();
    }

    void StartBlinking()
    {
        InvokeRepeating("ToggleVisibility", 0f, 0.5f);
    }

    void ToggleVisibility()
    {
        // 현재 텍스트의 활성화/비활성화 상태를 반전
        text.enabled = !text.enabled;
    }
}
