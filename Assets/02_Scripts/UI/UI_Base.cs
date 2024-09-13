using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();
    //컴퍼넌트에 연결해줄 함수 형태로 만듬(type을 사용할려면 using system;)
    public abstract void Init();
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);

        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
            {
                objects[i] = Util.FindChild(gameObject, names[i], true);
            }
            else
            {
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);
            }
            //잘 찾아주고 있는지 테스트
            if (objects[i] == null)
            {
                Debug.Log($"Failed to bind({names[i]})");
            }
        }
    }
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false) // 값이 없으면 그냥 리턴
            return null;
        return objects[idx] as T; // 오브젝트에다가 인덱스번호를 추출한 다음에 T로 캐스팅 해줌
    }
    //protected Text GetText(int idx) { return Get<Text>(idx); }
    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }
    protected TextMeshProUGUI GetTextMeshProUGUI(int idx) { return Get<TextMeshProUGUI>(idx); }
    protected GameObject GetGameObject(int idx) { return Get<GameObject>(idx); }

    //go는 action이 포함된 스크립트가 있는 게임오브젝트 // action은 구독을 시킬 액션 // mode(상태)
    /*public static void BindUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click) //, OnDragHandler
    {
        //지금은 컴퍼넌트를 적용한 상태이지만 없는경우도 있다. 이때 자동으로 적용할 수 있게 변경
        UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        evt.OnDragHandler += ((PointerEventData data) => { go.transform.position = data.position; });
    }*/
    public static void BindUIEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click) //, OnDragHandler
    {
        //지금은 컴퍼넌트를 적용한 상태이지만 없는경우도 있다. 이때 자동으로 적용할 수 있게 변경
        UI_EventHandler evt = Util.GetorAddComponent<UI_EventHandler>(go);
        //evt.OnDragHandler += ((PointerEventData data) => { go.transform.position = data.position; });
        
        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
        }
    }
}
