using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Item : UI_Base
{
    enum GameObjects
    {
        Item_Icon,
        ItemNameText,
    }
    string _name;
    // Start is called before the first frame update
    void Start()
    {
        Init();   
    }

    // Update is called once per frame
    public override void Init()
    {
        Bind<GameObject>(typeof(GameObjects));
        Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<Text>().text = _name;

        Get<GameObject>((int)GameObjects.Item_Icon).BindUIEvent((PointerEventData) => { Debug.Log($"아이템 클릭! {_name}"); });
    }
    public void setinfo(string name)
    {
        _name = name;
    }
}
