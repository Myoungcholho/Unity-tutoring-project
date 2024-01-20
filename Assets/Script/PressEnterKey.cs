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
        // ���� ��ũ��Ʈ�� �߰��� GameObject�� Text ������Ʈ�� ���ٸ� ������ �����ϱ� ���� �߰��մϴ�.
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
        // ���� �ؽ�Ʈ�� Ȱ��ȭ/��Ȱ��ȭ ���¸� ����
        text.enabled = !text.enabled;
    }
}
