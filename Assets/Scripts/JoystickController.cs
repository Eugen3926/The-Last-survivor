using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using Photon.Pun;
using Photon.Realtime;

public class JoystickController : MonoBehaviour
{
    public static event onTouchEvent onTouchDownEvent;
    public delegate void onTouchEvent(float joyX, float joyZ);

    FixedJoystick _joystick;    

    private void Start()
    {        
        _joystick = this.transform.GetComponent<FixedJoystick>();
    }    

    private void FixedUpdate()
    {        
        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            onTouchDownEvent?.Invoke(_joystick.Horizontal, _joystick.Vertical);
        }
    }
}
