using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ClickManipulator : MouseManipulator
{
    private Action _mouseDownEvent;
    private Action _mouseUpEvent;

    public ClickManipulator(Action mouseDownEvent = null, Action mouseUpEvent = null)
    {
        _mouseUpEvent = mouseUpEvent;
        _mouseDownEvent = mouseDownEvent;
        activators.Add(new ManipulatorActivationFilter { button = MouseButton.LeftMouse });
    }

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<MouseDownEvent>(OnMouseDown);
        target.RegisterCallback<MouseUpEvent>(OnMouseUp);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<MouseDownEvent>(OnMouseDown);
        target.UnregisterCallback<MouseUpEvent>(OnMouseUp);
    }

    private void OnMouseDown(MouseDownEvent e)
    {
        if(CanStartManipulation(e))
            _mouseDownEvent?.Invoke();
    }

    private void OnMouseUp(MouseUpEvent e)
    {
        _mouseUpEvent?.Invoke();
        target.ReleaseMouse();
    }
}
