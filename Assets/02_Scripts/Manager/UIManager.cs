using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    int _order = 10; //혹시 모르니까 여유를 두고 먼저 생성할게 있다면 10보다 작은 수로 팝업할 수 있게 함.
    //가장 마지막에 띄운 팝업이 가장 먼저 사라져야하기 때문에
    UI_Scene _sceneUI = null;
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    Stack<UI_Inven> _invenStack = new Stack<UI_Inven>();
    //스텍은 마지막에 들어간게 나옴
    //Q는 먼저들어간게 나옴
    public T ShowPopupUI<T>(string name = null) where T : UI_Popup
        //T는 아무T나 받는게 아니라 무조건 UI 팝업을 상속받는 애로 만들자
    {
        if (string.IsNullOrEmpty(name))//이름을 안받았으면
            name = typeof(T).Name;//T의 이름으로

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");//팝업생성
        T popup = Util.GetorAddComponent<T>(go); // 컴퍼넌트가 붙어있지 않다면 추가
        _popupStack.Push(popup);
        /*GameObject root = GameObject.Find("@UI_Root");
        if(root == null)
        {
            root = new GameObject { name = "@UI_Root" };
        }*/

        go.transform.SetParent(Root.transform);
        return popup;
    }
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
        //T는 아무T나 받는게 아니라 무조건 UI 팝업을 상속받는 애로 만들자
    {
        if (string.IsNullOrEmpty(name))//이름을 안받았으면
            name = typeof(T).Name;//T의 이름으로

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");//팝업생성
        T sceneUI = Util.GetorAddComponent<T>(go); // 컴퍼넌트가 붙어있지 않다면 추가
        _sceneUI = sceneUI;
       
        /*GameObject root = GameObject.Find("@UI_Root");
        if (root == null)
        {
            root = new GameObject { name = "@UI_Root" };
        }*/

        go.transform.SetParent(Root.transform);
        return sceneUI;
    }
    public T MakeSubItem<T>(Transform parent = null, string name = null)where T : UI_Base
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");
        if (parent != null)
            go.transform.SetParent(parent);

        return Util.GetorAddComponent<T>(go);
    }
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }
    public void SetCanvas(GameObject go,bool sort = true)
    {
        //캔버스 추출
        Canvas canvas = Util.GetorAddComponent<Canvas>(go);
        //랜더모드는 무조건 ScreenSpaceOverlay(이경우에만 소팅되기 때문에)
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        //캔버스 안에 캔버스가 중첩해서 있을 때 그 부모가 어떤 값을 가지던 자신은 무조건 내 소팅 오더를 가질꺼야
        //overrideSorting을 통해 혹시라도 중첩캔버스라 자식 캔버스가 있더라도 부모 캔버스가 어떤 값을 가지던
        //나는 내 오더값을 가지려 할때 true
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else //팝업이랑 상관없는 일반 UI
        {
            canvas.sortingOrder = 0;
        }
    }
    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void ClosePopupUI(UI_Popup popup) // 삭제할것을 지정할 수 있으니 좀 더 안전하게 삭제할 수 있다.
    {
        if (_popupStack.Count == 0)
            return;
        // Peek() : Stack <T>의 맨 위에서 개체를 제거하지 않고 반환합니다. (꺼내지 않고 엿본다고 생각)
        if(_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }
        ClosePopupUI();
    }
    
    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }
    public void CloseSceneUI()
    {
        if (_invenStack.Count == 0)
            return;

        UI_Inven inven = _invenStack.Pop();
        Managers.Resource.Destroy(inven.gameObject);
        inven = null;
        _order--;
    }
    public void CloseSceneUI(UI_Inven Inven) // 삭제할것을 지정할 수 있으니 좀 더 안전하게 삭제할 수 있다.
    {
        if (_invenStack.Count == 0)
            return;
        // Peek() : Stack <T>의 맨 위에서 개체를 제거하지 않고 반환합니다. (꺼내지 않고 엿본다고 생각)
        if (_invenStack.Peek() != Inven)
        {
            Debug.Log("Close Inven Failed!");
            return;
        }
        CloseSceneUI();
    }
    public void CloseAllSceneUI()
    {
        while (_invenStack.Count > 0)
            CloseSceneUI();
    }

    public void Clear()
    {
        CloseAllPopupUI();
        _sceneUI = null;
    }
}
