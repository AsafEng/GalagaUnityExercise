using System;
using UnityEngine;
using Utils;

public class InputEventManager : Singleton<InputEventManager>
{
    //Player control
    public Action<Vector2> UpdatedMovementInput; 
    public Action PressedActionButton;
}