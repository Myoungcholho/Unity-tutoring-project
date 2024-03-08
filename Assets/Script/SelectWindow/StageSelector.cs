using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image borderImg;
    
    // Start is called before the first frame update
    void Start()
    {
        // 자식 border 미출력 처리
        borderImg.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //마우스가 이미지 위에 올라갔을때 이미지 크기 조절
        borderImg.enabled = true;
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        //마우스가 이미지 벗어났을때 원래대로
        borderImg.enabled = false;
    }
 
 

}
