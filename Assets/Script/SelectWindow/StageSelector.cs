using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image borderImg;
    public int stageNumber;
    private ScrollViewController scrollView;

    // Start is called before the first frame update
    void Start()
    {
        // 자식 border 미출력 처리
        borderImg.enabled = false;
        scrollView = GameObject.Find("Scroll View").GetComponent<ScrollViewController>();
    }

    // 클릭 시 실행할 동작을 여기에 추가
    public void OnPointerClick(PointerEventData eventData)
    {
        scrollView.stageSelected = true;
        scrollView.StartCoroutineScroll(stageNumber);
    }

    //마우스가 이미지 위에 올라갔을때 이미지 크기 조절
    public void OnPointerEnter(PointerEventData eventData)
    {
        borderImg.enabled = true;
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        //마우스가 이미지 벗어났을때 원래대로
        borderImg.enabled = false;
    }
 
 

}
