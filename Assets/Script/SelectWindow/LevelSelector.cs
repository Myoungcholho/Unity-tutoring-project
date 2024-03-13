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

    // Ŭ�� �� ������ ������ ���⿡ �߰�
    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    //���콺�� �̹��� ���� �ö�����
    public void OnPointerEnter(PointerEventData eventData)
    {
        borderImg.enabled = true;
    }

    //���콺�� �̹��� ������� �������
    public void OnPointerExit(PointerEventData evenData)
    {
        borderImg.enabled = false;
    }
}
