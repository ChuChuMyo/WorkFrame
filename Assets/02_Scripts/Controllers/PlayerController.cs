using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    //[SerializeField]
    //float _speed = 10.0f;  // _ 를 사용하면 전역변수로 사용한다는 뜻
    PlayerStat _stat;

    bool _moveToDest = false;
    Vector3 _destPos;
    //float wait_run_ratio = 0;
    
    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
    }
    PlayerState _state = PlayerState.Idle;
    void Start()
    {
        _stat = gameObject.GetComponent<PlayerStat>();

        Managers.Input.KeyAction -= Onkeyboard;
        Managers.Input.KeyAction += Onkeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;
        //Managers.Resource.Instantiate("UI/UI_Button"); // 프리팹폴더에 UI폴더를 만든 후 관리 // 프리팹폴더가 베이스라는걸 기억
        //Managers.UI.ShowPopupUI<UI_Button>("UI_Button");
        //Debug.Log(5656);
        //uiPopup = Managers.UI.ShowPopupUI<UI_Button>(); // Managers.UI.ShowPopupUI<UI_Button>("팝업UI명");
        /*
        for (int i =0; i<1; i++)
        {
            uiPopup = Managers.UI.ShowPopupUI<UI_Button>();//예제 코드(프레임워크아님)
        }
        */
        //Managers.UI.ShowSceneUI<UI_Inven>();//예제 코드(프레임워크아님)
    }
    UI_Button uiPopup;//예제 코드(프레임워크아님)

    void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case
                PlayerState.Idle:
                UpdateIdle();
                break;
                
        }
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
        {
            Managers.UI.ClosePopupUI();
            
            
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (uiPopup != null)
            {
                Managers.UI.ClosePopupUI(uiPopup);
            }
        }
       
        
        /*if (_moveToDest)
        {
            Vector3 dir = _destPos - transform.position;//방향
            if (dir.magnitude < 0.0001f)//거리
            {
                _moveToDest = false;
            }
            else
            {
                
                 float moveDist = _speed * Time.deltaTime
                if (moveDist >= dir.magnitude)
                    moveDist = dir.magnitude;
                
                //위의 코드를 이런식으로 표현 가능
                float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude); // dir.magnitude는 찍은곳 까지의 최대거리 // 이 이상은 이동하지 못하게

                transform.position += dir.normalized * moveDist;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            }
        }*/
        /*if(_moveToDest)
        {
            Animator anim = GetComponent<Animator>();
            anim.Play("RUN");
        }
        else
        {
            Animator anim = GetComponent<Animator>();
            anim.Play("WAIT");
        }*/
        /*if (_moveToDest)
        {
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("wait_run_ratio", 1);
            anim.Play("RUN");
        }
        else
        {
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("wait_run_ratio", 0);
            anim.Play("WAIT");
        }*/
        /*if (_moveToDest)
        {
            wait_run_ratio = Mathf.Lerp(wait_run_ratio,1,10.0f*Time.deltaTime);
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("wait_run_ratio", wait_run_ratio);
            anim.Play("RUN");
        }
        else
        {
            wait_run_ratio = Mathf.Lerp(wait_run_ratio, 0, 10.0f * Time.deltaTime);
            Animator anim = GetComponent<Animator>();
            anim.SetFloat("wait_run_ratio", wait_run_ratio);
            anim.Play("WAIT");
        }*/
    }
    void UpdateDie()
    {

    }
    void UpdateMoving()
    {
        Vector3 dir = _destPos - transform.position;
        if(dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();

            float moveDist = Mathf.Clamp(_stat.MoveSpeed*Time.deltaTime,0, dir.magnitude);
            nma.Move(dir.normalized * moveDist);

            Debug.DrawRay(transform.position, dir.normalized, Color.green);
            if(Physics.Raycast(transform.position,dir,1.0f,LayerMask.GetMask("Block")))
            {
                _state = PlayerState.Idle;
                return;
            }

            transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(dir), 20*Time.deltaTime);

            Animator anim = GetComponent<Animator>();

            anim.SetFloat("speed", _stat.MoveSpeed);
        }
    }
    void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }
    void Onkeyboard()
    {
        
        
        
        // 좌, 우 전, 후 이동
        if (Input.GetKey(KeyCode.W))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            // 회전 보간
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.2f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);   // 로컬 좌표라서 바라보는 방향으로 가면 됨
            transform.position += Vector3.forward * Time.deltaTime * _stat.MoveSpeed;    // 월드 좌표라서 각 이동마다 방향을 정해줘야 함
        }

        if (Input.GetKey(KeyCode.S))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.2f);
            //transform.Translate(Vector3.back * Time.deltaTime * _speed);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.back * Time.deltaTime * _stat.MoveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.2f);
            //transform.Translate(Vector3.left * Time.deltaTime * _speed);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.left * Time.deltaTime * _stat.MoveSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.2f);
            //transform.Translate(Vector3.right * Time.deltaTime * _speed);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.right * Time.deltaTime * _stat.MoveSpeed;
        }
        _moveToDest = false; //클릭방식으로 이동 불가
    }

    
    void OnMouseClicked(Define.MouseEvent evt)
    {
        int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

        if (_state == PlayerState.Die)
            return;
        //Press일경우는 작동 안되게 (임시로 처리될 수 있게)
        //프레스 기능을 사용하고 싶다면 삭제, 주석처리
        if (evt != Define.MouseEvent.Click)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit,100.0f, _mask)) // LayerMask.GetMask는 땅만 클릭될수있게 해주는 코드
        {
            _destPos = hit.point;
            //_moveToDest = true;
            _state = PlayerState.Moving;
        }
        if(hit.collider.gameObject.layer == (int)Define.Layer.Monster)
        {
            Debug.Log("Monster Click!");
        }
        else
        {
            Debug.Log("Monster Click!");
        }
    }
}
