using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Image borderImg;
    public int stageNumber;

    void Start()
    {
        borderImg.enabled = false;
    }

    // 클릭 시 실행할 동작을 여기에 추가
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    //마우스가 이미지 위에 올라갔을때
    public void OnPointerEnter(PointerEventData eventData)
    {
        borderImg.enabled = true;
    }

    //마우스가 이미지 벗어났을때 원래대로
    public void OnPointerExit(PointerEventData evenData)
    {
        borderImg.enabled = false;
    }
}
