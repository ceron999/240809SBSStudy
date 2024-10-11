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
    public System.Action OnClickLeftMouseBtn;

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
        if (Input.GetMouseButtonDown(0))
        {
            OnClickLeftMouseBtn?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnClickSpace?.Invoke();
        }

        // 커서 컨트롤
        SetShowCursor();

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        movement = new Vector2(inputX, inputY);

        float lookX = Input.GetAxis("Mouse X");
        float lookY = Input.GetAxis("Mouse Y");

        look = isShowCursor ? Vector2.zero : new Vector2(lookX, lookY);

        isLeftShift = Input.GetKey(KeyCode.LeftShift);
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