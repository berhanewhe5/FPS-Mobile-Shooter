using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 TouchDist { get; private set; }
    public Vector2 PointerOld { get; private set; }
    protected int PointerId;
    public bool Pressed { get; private set; }
    public bool isShooting;

    public bool isFirstMove;

    void Start()
    {
        TouchDist = Vector2.zero;
        PointerOld = Vector2.zero;
        PointerId = -1;
        Pressed = false;
        isShooting = false;
        isFirstMove = true;
    }

    void Update()
    {
        if (Pressed || isShooting)
        {
            if (PointerId >= 0 && PointerId < Input.touches.Length)
            {
                if (isFirstMove)
                {
                    PointerOld = Input.touches[PointerId].position;
                    isFirstMove = false;
                }

                TouchDist = Input.touches[PointerId].position - PointerOld;
                PointerOld = Input.touches[PointerId].position;
            }
            else
            {
                if (isFirstMove)
                {
                    PointerOld = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    isFirstMove = false;
                }

                TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                PointerOld = Input.mousePosition;
            }
        }
        else
        {
            TouchDist = Vector2.zero;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isFirstMove = true;
        Pressed = true;
        PointerId = eventData.pointerId;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }
}