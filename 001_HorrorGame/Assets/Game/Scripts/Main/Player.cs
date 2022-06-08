using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private float m_Speed = 2f;
    private float m_Gravity = 20f;
    private float m_SensityvityX = 1f;
    private float m_SensityvityY = 1f;

    [SerializeField] private Camera m_Camera = null;
    [SerializeField] PlayerInput m_PlayerInput = null;
    private CharacterController controller = null;
    private Vector3 m_MoveDirection = Vector3.zero;
    private bool m_IsRuning = false;
    private Vector2 m_PreMousePosition = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        m_PreMousePosition = Input.mousePosition;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {        
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_IsRuning = true;
        }
        else
        {
            m_IsRuning = false;
        }

        float x = Input.GetAxis("Mouse X") * m_SensityvityX;
        float y = Input.GetAxis("Mouse Y") * m_SensityvityY;
    
        m_Camera.transform.Rotate(new Vector3(-y, 0f, 0f));
        transform.Rotate(new Vector3(0f, x, 0f));
    }
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        m_MoveDirection = new Vector3(v, 0f, -h);
        m_MoveDirection = transform.TransformDirection(m_MoveDirection);

        if (m_IsRuning)
        {
            m_MoveDirection *= m_Speed * 1.5f;
        }
        else
        {
            m_MoveDirection *= m_Speed;
        }
        m_MoveDirection.y -= m_Gravity * Time.deltaTime;
        controller.Move(m_MoveDirection * Time.deltaTime);
    }

    private Quaternion ClampRotation(Quaternion q)
    {
        // q.x /= q.w;
        // q.y /= q.w;
        // q.z /= q.w;
        // q.w = 1f;

        // float angleX = Mathf.Atan(q.x) * Mathf.Rad2Deg * 2f;
        // angleX = Mathf.Clamp(angleX, MIN_X, MAX_X);
        // q.x = Mathf.Tan(angleX * Mathf.Deg2Rad * 0.5f);

        return q;
    }
}
