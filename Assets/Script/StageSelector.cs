using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    
    // �̹����� ũ�⸦ �����ϴ� ����
    public float hoverScaleFactor = 1.2f;

    private Vector3 originalSize;

    public Button[] stageButton;
    public GameObject[] levelPanel;

    // Start is called before the first frame update
    void Start()
    {
        originalSize = transform.localScale;

        for(int i=0; i < levelPanel.Length; i++)
        {
            levelPanel[i].SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //���콺�� �̹��� ���� �ö����� �̹��� ũ�� ����
        transform.localScale = originalSize * hoverScaleFactor;
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        //���콺�� �̹��� ������� �������
        transform.localScale = originalSize;
    }
 
    public void StageButtonClick(int stageButton)
    {
        for (int i=0; i < levelPanel.Length; i++)
        {
            levelPanel[i].SetActive(false);
        }
        levelPanel[stageButton].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
