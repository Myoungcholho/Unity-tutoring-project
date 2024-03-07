using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour , IScrollHandler, IBeginDragHandler, IDragHandler
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.01f;


    void Start()
    {
        //StartCoroutine(ScrollRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ScrollRoutine()
    {
        while(true)
        {
            scrollRect.horizontalNormalizedPosition += Time.deltaTime * scrollSpeed;
            if (scrollRect.horizontalNormalizedPosition >= 1f)
            {
                scrollRect.horizontalNormalizedPosition = 0f;
            }
            yield return null;
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        // ���콺 ��ũ�� �Է��� �����մϴ�.
       // eventData.scrollDelta = Vector2.zero;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�׸� ������ �� ��ũ�Ѻ並 �����մϴ�.
      //  if (eventData.button == PointerEventData.InputButton.Left)
         //   eventData.pointerDrag = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ��ũ�� �߿��� ��ũ�Ѻ並 �����մϴ�.
      //  if (eventData.button == PointerEventData.InputButton.Left)
     //       eventData.pointerDrag = null;
    }
}
