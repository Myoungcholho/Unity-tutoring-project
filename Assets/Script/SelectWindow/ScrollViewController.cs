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
    public bool stageSelected { get; set; }     // �������� ���� ����
    private bool isScrolling = false;           // ��ũ�� ������ ����

    void Start()
    {
        InitInputManager.instance.escPress += StartCoroutineScroll;

    }


    // �� �̺κ� �� �ƽ��.. ����� �غ���
    public void StartCoroutineScroll()
    {
        // ��ũ�����̶�� ������ ����.
        if (!isScrolling)
        {
            isScrolling = true;
            StartCoroutine(ScrollRoutine(0));
        }
    }

    public void StartCoroutineScroll(int stage)
    {
        // ��ũ�����̶�� ������ ����.
        if (!isScrolling)
        {
            isScrolling = true;
            StartCoroutine(ScrollRoutine(stage));
        }
    }

    private IEnumerator ScrollRoutine(int stage)
    {
        //esc�� ������ ��� �Ǵ�
        if (!stageSelected)
        {
            Debug.Log("break �ɸ�");
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
                    isScrolling = false;                     // ��ũ�� ��
                    StageManager.instance.StageNum = stage;     // �������� �� �Ҵ� �ʱ�ȭ
                    break;
                }
            }
            else
            {
                if (scrollRect.horizontalNormalizedPosition <= 0f)
                {
                    scrollingRight = true;
                    stageSelected = false;
                    isScrolling = false;                    // ��ũ�� ��
                    StageManager.instance.StageNum = 0;     // �������� �� �Ҵ� �ʱ�ȭ
                    break;
                }
            }
           
            yield return null;
        }
    }

    public void OnScroll(PointerEventData eventData)
    {
        // ���콺 ��ũ�� �Է��� �����մϴ�.
        eventData.scrollDelta = Vector2.zero;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // �巡�׸� ������ �� ��ũ�Ѻ並 �����մϴ�.
        if (eventData.button == PointerEventData.InputButton.Left)
            eventData.pointerDrag = null;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ��ũ�� �߿��� ��ũ�Ѻ並 �����մϴ�.
        if (eventData.button == PointerEventData.InputButton.Left)
            eventData.pointerDrag = null;
    }
}
