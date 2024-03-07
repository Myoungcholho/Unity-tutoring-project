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
        // 마우스 스크롤 입력을 무시합니다.
       // eventData.scrollDelta = Vector2.zero;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그를 시작할 때 스크롤뷰를 차단합니다.
      //  if (eventData.button == PointerEventData.InputButton.Left)
         //   eventData.pointerDrag = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 스크롤 중에도 스크롤뷰를 차단합니다.
      //  if (eventData.button == PointerEventData.InputButton.Left)
     //       eventData.pointerDrag = null;
    }
}
