using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    private void Awake()
    {
        Init();
    }

    protected override void Init()
    {
        base.Init();

        SceneType = Define.Scene.Game;

        //Managers.UI.ShowSceneUI<UI_Inven>();

        Dictionary<int, Data.Stat> Dict = Managers.Data.StatDict;
    }

    public override void Clear()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
