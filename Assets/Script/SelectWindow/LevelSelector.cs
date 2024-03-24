using UnityEditor.Build;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image borderImg;
    public Image crownImg;
    public int stageNumber;
    private Image img;
    private bool stageSelectable;       // �������� ���� ���� ����
    private bool isCleared;             // �������� Ŭ���� ����
    private StageManager stageManager;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    void Start()
    {
        borderImg.enabled = false;
        stageManager = StageManager.instance;
        int clearedIdx = stageNumber - 1 >= 0 ? stageNumber - 1 : 0;

        stageSelectable = stageManager.isStageSelected[stageNumber-1];
        isCleared = stageManager.isStageCleared[clearedIdx];

        stageManager.isStageCleared.OnCleared += UpdateIsCleared;

        CrownRendering();
        UpdateAlpha();
    }

    // Ŭ�� �� ������ ������ ���⿡ �߰�
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!stageSelectable || stageManager.screenMode != 1)
            return;

        StageManager.instance.LevelNum = stageNumber;
    }

    //���콺�� �̹��� ���� �ö�����
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!stageSelectable || stageManager.screenMode != 1)
            return;

        borderImg.enabled = true;
    }

    //���콺�� �̹��� ������� �������
    public void OnPointerExit(PointerEventData evenData)
    {
        if (!stageSelectable || stageManager.screenMode != 1)
            return;

        borderImg.enabled = false;
    }

    private void UpdateIsCleared(int num,bool value)
    {
        // ���� ���������� ȣ��Ȱ� �ƴ϶�� return
        if(num + 1 == stageNumber)
        {
            stageSelectable = true;
            UpdateAlpha();
            return;
        }
        else if (num == stageNumber)
        {
            isCleared = value;
            CrownRendering();
        }
    }

    private void CrownRendering()
    {
        crownImg.enabled = isCleared;
    }
    private void UpdateAlpha()
    {
        Color color = img.color;
        if (stageSelectable)
        {
            color.a = 255f / 255f;
        }
        else
        {
            color.a = 130f / 255f;
        }
        img.color = color;
    }
}
