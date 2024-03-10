using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollViewController : MonoBehaviour , IScrollHandler, IBeginDragHandler, IDragHandler
{
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.01f;
    private bool scrollingRight = true;
    public bool stageSelected { get; set; }     // 스테이지 선택 여부
    private bool isScrolling = false;           // 스크롤 중인지 여부

    void Start()
    {
        InitInputManager.instance.escPress += StartCoroutineScroll;

    }


    // 음 이부분 좀 아쉬운데.. 고민좀 해보자
    public void StartCoroutineScroll()
    {
        // 스크롤중이라면 들어오지 못함.
        if (!isScrolling)
        {
            isScrolling = true;
            StartCoroutine(ScrollRoutine(0));
        }
    }

    public void StartCoroutineScroll(int stage)
    {
        // 스크롤중이라면 들어오지 못함.
        if (!isScrolling)
        {
            isScrolling = true;
            StartCoroutine(ScrollRoutine(stage));
        }
    }

    private IEnumerator ScrollRoutine(int stage)
    {
        //esc로 들어오는 경우 판단
        if (!stageSelected)
        {
            Debug.Log("break 걸림");
            isScrolling = false;
            yield break;
        }

        while(true)
        {
            if (scrollingRight)
                scrollRect.horizontalNormalizedPosition += Time.fixedDeltaTime * scrollSpeed;
            else
                scrollRect.horizontalNormalizedPosition -= Time.fixedDeltaTime * scrollSpeed;

            if (scrollingRight)
            {
                if (scrollRect.horizontalNormalizedPosition >= 1f)
                {
                    scrollingRight = false;
                    isScrolling = false;                     // 스크롤 끝
                    StageManager.instance.StageNum = stage;     // 스테이지 값 할당 초기화
                    break;
                }
            }
            else
            {
                if (scrollRect.horizontalNormalizedPosition <= 0f)
                {
                    scrollingRight = true;
                    stageSelected = false;
                    isScrolling = false;                    // 스크롤 끝
                    StageManager.instance.StageNum = 0;     // 스테이지 값 할당 초기화
                    break;
                }
            }
           
            yield return null;
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        // 마우스 스크롤 입력을 무시합니다.
        eventData.scrollDelta = Vector2.zero;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 드래그를 시작할 때 스크롤뷰를 차단합니다.
        if (eventData.button == PointerEventData.InputButton.Left)
            eventData.pointerDrag = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 스크롤 중에도 스크롤뷰를 차단합니다.
        if (eventData.button == PointerEventData.InputButton.Left)
            eventData.pointerDrag = null;
    }
}
