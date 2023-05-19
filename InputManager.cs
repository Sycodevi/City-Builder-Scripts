using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IInputManager
{
    private Action<Vector3> OnPointerSecondChangeHandler;
    private Action<Vector3> OnPointerDownHandler;
    private Action OnPointerSecondUpHandler;
    private Action<Vector3> OnPointerChangeHandler;
    private Action OnPointerUpHandler;

    private LayerMask mouseInputMask;

    public LayerMask MouseInputMask { get => mouseInputMask; set => mouseInputMask = value; }

    // Update is called once per frame
    void Update()
    {
        GetPointerPosition();
        GetPointerDrag();
    }

    private void GetPointerPosition()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            /*Vector3? position = GetMousePosition();
            if (position.HasValue)
            {
                OnPointerDownHandler?.Invoke(position.Value);
                position = null;
            }*/
            CallActionOnPointer((position) => OnPointerDownHandler?.Invoke(position));

        }
        if (Input.GetMouseButton(0))
        {
            /*Vector3? position = GetMousePosition();
            if (position.HasValue)
            {
                OnPointerChangeHandler?.Invoke(position.Value);
                position = null;
            }*/
            CallActionOnPointer((position) => OnPointerChangeHandler?.Invoke(position));
        }
        if(Input.GetMouseButtonUp(0))
        {
            OnPointerUpHandler?.Invoke();
        }
    }

    private void CallActionOnPointer(Action<Vector3> action)
    {
        Vector3? position = GetMousePosition();
        if (position.HasValue)
        {
            action(position.Value);
            position = null;
        }
    }
    private Vector3? GetMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3? position = null;
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, mouseInputMask))
        {
            position = hit.point - transform.position;
        }

        return position;
    }

    private void GetPointerDrag()
    {
        if (Input.GetMouseButton(1))
        {
            var position = Input.mousePosition;
            //Debug.Log(position);
            OnPointerSecondChangeHandler?.Invoke(position);
        }
        if (Input.GetMouseButtonUp(1))
        {
            OnPointerSecondUpHandler?.Invoke();
        }
    }

    public void AddListenerOnPointerDownEvent(Action<Vector3> listener)
    {
        OnPointerDownHandler += listener;
    }
    public void RemoveListenerOnPointerDownEvent(Action<Vector3> listener)
    {
        OnPointerDownHandler -= listener;
    }

    public void AddListenerOnPointerSecondChangeEvent(Action<Vector3> listener)
    {
        OnPointerSecondChangeHandler += listener;
    }
    public void RemoveListenerOnPointerSecondChangeEvent(Action<Vector3> listener)
    {
        OnPointerSecondChangeHandler -= listener;
    }
    public void AddListenerOnPointerSecondUpEvent(Action listener)
    {
        OnPointerSecondUpHandler += listener;
    }
    public void RemoveListenerOnPointerSecondUpEvent(Action listener)
    {
        OnPointerSecondUpHandler -= listener;
    }

    public void AddListenerOnPointerUpEvent(Action listener)
    {
        OnPointerUpHandler += listener;
    }

    public void AddListenerOnPointerChangeEvent(Action<Vector3> listener)
    {
        OnPointerChangeHandler += listener;
    }

    public void RemoveListenerOnPointerUpEvent(Action listener)
    {
        OnPointerUpHandler -= listener;
    }

    public void RemoveListenerOnPointerChangeEvent(Action<Vector3> listener)
    {
        OnPointerChangeHandler -= listener;
    }

    /*public Vector3 GridPosCalculator(Vector3 inputPosition) 
    {
        int x = Mathf.FloorToInt((float)inputPosition.x / cellSize);
        int z = Mathf.FloorToInt((float)inputPosition.z / cellSize);
        return new Vector3(x*cellSize,0,z*cellSize);
    }*/


}
