using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem Instance { get; private set; }

    public Vector2 Movement => movement;
    public Vector2 Look => look;
    public bool IsLeftShift => isLeftShift;

    private Vector2 movement;
    private Vector2 look;
    private bool isLeftShift;

    private bool isShowCursor = false;

    public System.Action OnClickSpace;
    public System.Action OnClickLeftMouseButton;
    public System.Action OnClickInteract;
    public System.Action<float> OnMouseScrollWheel;
    public System.Action OnClickThrowButton;

    private void Awake()
    {
        Instance = this;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    private void Update()
    {
        // 입력값 설정
        if(Input.GetKeyDown(KeyCode.G))
        {
            OnClickThrowButton?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            OnClickInteract?.Invoke();
        }

        if (Input.GetMouseButtonDown(0))
        {
            OnClickLeftMouseButton?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnClickSpace?.Invoke();
        }

        // 커서 컨트롤
        SetShowCursor();
        
        // 이동 벡터 설정
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        movement = new Vector2(inputX, inputY);

        // 시야 위치 설정
        float lookX = Input.GetAxis("Mouse X");
        float lookY = Input.GetAxis("Mouse Y");

        look = isShowCursor ? Vector2.zero : new Vector2(lookX, lookY);

        // 달리기 설정
        isLeftShift = Input.GetKey(KeyCode.LeftShift);

        // 상호작용 전용 마우스 휠 값 설정
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if(mouseScroll > 0 || mouseScroll <0)
        {
            OnMouseScrollWheel?.Invoke(mouseScroll);
        }
    }

    // 커서 컨트롤
    void SetShowCursor()
    {
        isShowCursor = Input.GetKey(KeyCode.LeftAlt);

        // 변수가 참이면 커서를 보이고 잠금 해제
        if (isShowCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        // 변수가 거짓이면 커서를 안보이고 잠금 설정
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}