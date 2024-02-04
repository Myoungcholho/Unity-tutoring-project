using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    
    // 이미지의 크기를 조절하는 변수
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
        //마우스가 이미지 위에 올라갔을때 이미지 크기 조절
        transform.localScale = originalSize * hoverScaleFactor;
    }

    public void OnPointerExit(PointerEventData evenData)
    {
        //마우스가 이미지 벗어났을때 원래대로
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
