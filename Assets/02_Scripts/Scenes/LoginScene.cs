using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoginScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Login;

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < 5; i++)
        {
            //Managers.Resource.Instantiate("unitychan");
            list.Add(Managers.Resource.Instantiate("unitychan"));
        }

        foreach(GameObject obj in list)
        {
            Managers.Resource.Destroy(obj);
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            //SceneManager.LoadScene("Game");
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }
}
