using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 0;
    public float jumpSpeed = 5;
    private bool onGround = true;
    private int count;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;

    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>();
        SetCountText();
        winTextObject.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown ("space") && onGround) {
            rb.AddForce(Vector3.up * jumpSpeed, ForceMode.Impulse);
            onGround = false;
        }
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    void OnCollisionEnter(Collision collision)
    {
        onGround = true;
    }

    private float movementX;
    private float movementY;

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            count = count + 1;
            other.gameObject.SetActive(false);
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Skor : " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }
}
