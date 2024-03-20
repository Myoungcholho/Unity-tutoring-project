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
    private bool stageSelectable;
    private bool isCleared;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    void Start()
    {
        borderImg.enabled = false;

        int stageSelectIdx = stageNumber-1 >= 0 ? stageNumber-1 : 0;
        int clearedIdx = stageNumber - 2 >= 0 ? stageNumber - 2 : 0;

        stageSelectable = StageManager.instance.isStageCleared[stageSelectIdx];
        isCleared = StageManager.instance.isStageCleared[clearedIdx];

        StageManager.instance.isStageCleared.OnCleared += UpdateIsCleared;

        CrownRendering();
        UpdateAlpha();
    }

    // 클릭 시 실행할 동작을 여기에 추가
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!stageSelectable)
            return;

        StageManager.instance.LevelNum = stageNumber;
    }

    //마우스가 이미지 위에 올라갔을때
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!stageSelectable)
            return;

        borderImg.enabled = true;
    }

    //마우스가 이미지 벗어났을때 원래대로
    public void OnPointerExit(PointerEventData evenData)
    {
        if (!stageSelectable)
            return;

        borderImg.enabled = false;
    }

    private void UpdateIsCleared(int num,bool value)
    {
        // 현재 스테이지가 호출된게 아니라면 return
        if (num != stageNumber)
            return;

        isCleared = value;
        int stageSelectIdx = stageNumber - 1 >= 0 ? stageNumber - 1 : 0;
        int clearedIdx = stageNumber - 2 >= 0 ? stageNumber - 2 : 0;
        stageSelectable = StageManager.instance.isStageCleared[stageSelectIdx];
        isCleared = StageManager.instance.isStageCleared[clearedIdx];
        CrownRendering();
        UpdateAlpha();
    }

    private void CrownRendering()
    {
        crownImg.enabled = isCleared;
    }
    private void UpdateAlpha()
    {
        Color color = img.color;
        if (isCleared)
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
