using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    //����
    public T Load<T>(string path) where T : Object
    {
        //1. original �̹� ������ �ٷ� ���

        //���࿡ �������� ��쿡 ���������� Ǯ���� �ѹ� ã�ƺ��� �װ��� �ٷ� ��ȯ����
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

        //2. Ȥ�� Ǯ���� ������Ʈ ������ �װ��� ��ȯ
        if(original.GetComponent<Poolable>() != null)
        {
            return Managers.Pool.Pop(original, parent).gameObject;
        }

        GameObject go = Object.Instantiate(original, parent);

        int index = go.name.IndexOf("(Clone)"); //"(Clone)"���ڿ��� ã�Ƽ� �ε����� ��ȯ
        if (index > 0)
        {
            go.name = go.name.Substring(0, index); //UI_Inven_Item//(Clone)
        }

        return go;
    }

    //�����غ����� �� �����δ� �ʿ����.
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        //3. ���࿡ Ǯ���� �ʿ��� ������Ʈ��� �ٷ� �����ϴ� ���� �ƴ϶� Ǯ�� �Ŵ������� ��Ź
        Poolable poolabe = go.GetComponent<Poolable>();
        if(poolabe != null)
        {
            Managers.Pool.Push(poolabe);
            return;
        }

        Object.Destroy(go);
    }

}