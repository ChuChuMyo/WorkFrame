using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//매니저스가 있어서 굳이 컴포넌트로 만들 필요가 없으니까 일반적인 C#파일로 만듬
public class InputManager
{
    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;

    bool _pressed = false;

    //대표로 입력을 체크한 다음에 실제로 입력이 있으면 그것을 이벤트로 전파를 해주는 형식으로 구현(리스너 패턴)
    //이렇게하면 플레이어컨트롤러가 100이되던 1000개가 되던 이 루프마다 한번씩만 체크해가지고 그 이벤트를 전파하는 방식으로 구현 된 것임
    //이렇게 관리하면 또 좋은 것은 어디서 키보드 입력을 받았는지 찾기가 어려운 문제가 해소된다.
    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;
        if(Input.anyKey && KeyAction != null)
         KeyAction.Invoke();
        if(MouseAction != null)
        {
            if (Input.GetMouseButton(0)) //프레스일경우
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else // 클릭일 경우(만약에 한번이라도 프레스를 했으면 click이라는 이벤트 발생)
            {
                if(_pressed)
                    MouseAction.Invoke(Define.MouseEvent.Click);

                _pressed = false;
            }
        }

        //하나의 Form을 다른 thread에서 접근하게 될 경우에 기존의 Form과 충돌이 날 수 있다.
        //이 때 invoke를 사용하여 실행하려고 하는 메소드의 대리자(delegate)를 실행시키면 된다.
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null;
    }

}