using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using System.ComponentModel;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using TMPro;

public class UI_Button : UI_Popup
{
    /*[SerializeField]
    TextMeshProUGUI _text;

    int _score = 0;*/
    enum Buttons
    {
        PointButton
    }
    enum Texts
    {
        PointText,
        ScoreText,
    }
    enum GameObjects
    {
        TestObject,
    }
    enum Images
    {
        image
    }
    // Start is called before the first frame update
    void Start()
    { 
        Init();
    }
    //컴퍼넌트에 연결해줄 함수 형태

    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        //Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));
        //test
        //Get<Text>((int)Texts.ScoreText).text = "Test";
        GetTextMeshProUGUI((int)Texts.ScoreText).text = "Test";
        //GetText((int)Texts.ScoreText).text = "Test";
        GetButton((int)Buttons.PointButton).gameObject.BindUIEvent(OnButtonClicked);
        GameObject go = GetImage((int)Images.image).gameObject;
        //UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        //evt.OnBeginDragHandler += ((PointerEventData data) => { go.transform.position = data.position; });
        BindUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        //_score++;
        //_text.text = $"점수 : { _score}";
        _score++;
        GetTextMeshProUGUI((int)Texts.PointText).text = $"점수 : {_score}";
    }
  
}
