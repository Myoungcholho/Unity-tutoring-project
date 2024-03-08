using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image borderImg;
    
    // Start is called before the first frame update
    void Start()
    {
        // �ڽ� border ����� ó��
        borderImg.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //���콺�� �̹��� ���� �ö����� �̹��� ũ�� ����
        borderImg.enabled = true;
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        //���콺�� �̹��� ������� �������
        borderImg.enabled = false;
    }
 
 

}
