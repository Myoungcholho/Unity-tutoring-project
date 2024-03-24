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
        // 자식 border 미출력 처리
        borderImg.enabled = false;
        scrollView = GameObject.Find("Scroll View").GetComponent<ScrollViewController>();
        textChanger = FindObjectOfType<TextChanger>();
        stageManager = StageManager.instance;
    }

    // 클릭 시 실행할 동작을 여기에 추가
    public void OnPointerClick(PointerEventData eventData)
    {
        if (stageManager.screenMode != 0)
            return;

        stageManager.screenMode = 1;
        scrollView.stageSelected = true;
        scrollView.StartCoroutineScroll(stageNumber);
    }

    //마우스가 이미지 위에 올라갔을때
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (stageManager.screenMode != 0)
            return;

        borderImg.enabled = true;
        textChanger.StageTextUpdate(stageNumber +". " + uiTextStr);
    }

    //마우스가 이미지 벗어났을때 원래대로
    public void OnPointerExit(PointerEventData evenData)
    {
        if (stageManager.screenMode != 0)
            return;

        borderImg.enabled = false;
    }
}
