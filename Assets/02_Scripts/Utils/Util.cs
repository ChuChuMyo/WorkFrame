using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Util
{
    public static GameObject FindChild(GameObject go, string name, bool recursive = false)
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        if (transform == null)
        {
            return null;
        }
        return transform.gameObject;
    }

    //(최상의 부모, 이름은 비교하지 않고 그 타입에만 해당하면 리턴(컴터넌트이름),
    // 재귀적으로 사용 자식만 찾을건지 자식의 자식도 찾을 것인지
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false)
        where T : UnityEngine.Object
    {
        if (go == null)
            return null;
        if (recursive == false) //직속 자식만
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();
                    if (component != null)
                        return component;
                }
            }
        }
        else // 자식의 자식까지
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {   //이름이 비어있거나 내가 찾으려는 이름과 같다면
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        return null;
    }
   //게임 오브젝트에 해당 컴퍼넌트가 없으면 부착해주는 함수
    public static T GetorAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
        {
            component = go.AddComponent<T>();
        }
        return component;
    }//GetorAddComponent<T>(GameObject go) // 호출형태
    
    
}
