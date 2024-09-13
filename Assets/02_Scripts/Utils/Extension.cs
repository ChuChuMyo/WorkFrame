using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
        //유틸에 있던걸 한번 다시 요약해서 쓸꺼야 라는 뜻
    {
        return Util.GetorAddComponent<T>(go);
    }
    public static void BindUIEvent(this GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_Base.BindUIEvent(go, action, type);
    }
}

