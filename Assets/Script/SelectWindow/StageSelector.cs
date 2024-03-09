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
        // �ڽ� border ����� ó��
        borderImg.enabled = false;
        scrollView = GameObject.Find("Scroll View").GetComponent<ScrollViewController>();
    }

    // Ŭ�� �� ������ ������ ���⿡ �߰�
    public void OnPointerClick(PointerEventData eventData)
    {
        scrollView.stageSelected = true;
        scrollView.StartCoroutineScroll(stageNumber);
    }

    //���콺�� �̹��� ���� �ö����� �̹��� ũ�� ����
    public void OnPointerEnter(PointerEventData eventData)
    {
        borderImg.enabled = true;
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        //���콺�� �̹��� ������� �������
        borderImg.enabled = false;
    }
 
 

}
