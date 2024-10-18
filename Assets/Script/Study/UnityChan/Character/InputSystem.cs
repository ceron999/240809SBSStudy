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
        // �Է°� ����
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

        // Ŀ�� ��Ʈ��
        SetShowCursor();
        
        // �̵� ���� ����
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");
        movement = new Vector2(inputX, inputY);

        // �þ� ��ġ ����
        float lookX = Input.GetAxis("Mouse X");
        float lookY = Input.GetAxis("Mouse Y");

        look = isShowCursor ? Vector2.zero : new Vector2(lookX, lookY);

        // �޸��� ����
        isLeftShift = Input.GetKey(KeyCode.LeftShift);

        // ��ȣ�ۿ� ���� ���콺 �� �� ����
        float mouseScroll = Input.GetAxis("Mouse ScrollWheel");
        if(mouseScroll > 0 || mouseScroll <0)
        {
            OnMouseScrollWheel?.Invoke(mouseScroll);
        }
    }

    // Ŀ�� ��Ʈ��
    void SetShowCursor()
    {
        isShowCursor = Input.GetKey(KeyCode.LeftAlt);

        // ������ ���̸� Ŀ���� ���̰� ��� ����
        if (isShowCursor)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        // ������ �����̸� Ŀ���� �Ⱥ��̰� ��� ����
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}