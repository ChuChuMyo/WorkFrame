using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    //랩핑
    public T Load<T>(string path) where T : Object
    {
        //1. original 이미 있으면 바로 사용

        //만약에 프리펩인 경우에 오리지널을 풀에서 한번 찾아보고 그것을 바로 반환하자
        if (typeof(T) == typeof(GameObject))
        {

            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
            {
                name = name.Substring(index + 1);
            }

            GameObject go = Managers.Pool.GetOriginal(name);

            if (go != null)
                return go as T;

        }
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {

        GameObject original = Load<GameObject>($"Prefabs/{path}");
        if(original == null)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        //2. 혹시 풀링된 오브젝트 있으면 그것을 반환
        if(original.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, parent);

        int index = go.name.IndexOf("(Clone)"); //"(Clone)"문자열을 찾아서 인덱스를 반환
        if (index > 0)
        {
            go.name = go.name.Substring(0, index); //UI_Inven_Item//(Clone)
        }

        return go;
    }

    //랩핑해본것일 뿐 실제로는 필요없다.
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        //3. 만약에 풀링이 필요한 오브젝트라면 바로 삭제하는 것이 아니라 풀링 매니저한테 위탁
        Poolable poolabe = go.GetComponent<Poolable>();
        if(poolabe != null)
        {
            Managers.Pool.Push(poolabe);
            return;
        }

        Object.Destroy(go);
    }

}