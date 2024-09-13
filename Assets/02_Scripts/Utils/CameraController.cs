using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Define.CameraMode _mode = Define.CameraMode.Quaterview;
    [SerializeField]
    Vector3 _delta = new Vector3(0.0f, 6.0f, -5.0f);
    //타켓
    [SerializeField]
    GameObject _player = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(_mode == Define.CameraMode.Quaterview)
        {
            RaycastHit hit;
            if(Physics.Raycast(_player.transform.position,_delta,out hit, _delta.magnitude, LayerMask.GetMask("Wall")))
            {
                float dist = (hit.point - _player.transform.position).magnitude*0.8f;
                transform.position = _player.transform.position + _delta.normalized*dist;
            }
            else
            {
                transform.position = _player.transform.position + _delta;
                transform.LookAt(_player.transform);
            }
        }
        
    }

    //나중에 QuaterView를 코드로 구현하고 싶다면 이런식으로 함수를 만들면 된다.
    //(무기 모드 변경하듯이, 또는 VR에서 이동 방법 변경하듯이 처리하면 된다.)
    public void SetQuarterView(Vector3 delta)
    {
        _mode = Define.CameraMode.Quaterview;
        _delta = delta;
    }
}
