using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigid;

    [SerializeField]
    private float _speed = 10;
    [SerializeField]
    private float jumpPower = 5;
    public LayerMask layer;
    public bool isGround = false;

    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector3 moveInput = new Vector3(Input.GetAxis("Horizontal"), 0);
        transform.position += moveInput * Time.deltaTime * _speed;
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            _rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isGround = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Ground"))
            isGround = true;
    }
}
