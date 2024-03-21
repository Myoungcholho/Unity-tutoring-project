using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image borderImg;
    public int stageNumber;
    public string uiTextStr;
    private TextChanger textChanger;
    private ScrollViewController scrollView;
    private StageManager stageManager;

    // Start is called before the first frame update
    void Start()
    {
        // �ڽ� border ����� ó��
        borderImg.enabled = false;
        scrollView = GameObject.Find("Scroll View").GetComponent<ScrollViewController>();
        textChanger = FindObjectOfType<TextChanger>();
        stageManager = StageManager.instance;
    }

    // Ŭ�� �� ������ ������ ���⿡ �߰�
    public void OnPointerClick(PointerEventData eventData)
    {
        if (stageManager.screenMode != 0)
            return;

        stageManager.screenMode = 1;
        scrollView.stageSelected = true;
        scrollView.StartCoroutineScroll(stageNumber);
    }

    //���콺�� �̹��� ���� �ö�����
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (stageManager.screenMode != 0)
            return;

        borderImg.enabled = true;
        textChanger.StageTextUpdate(stageNumber +". " + uiTextStr);
    }

    //���콺�� �̹��� ������� �������
    public void OnPointerExit(PointerEventData evenData)
    {
        if (stageManager.screenMode != 0)
            return;

        borderImg.enabled = false;
    }
}
