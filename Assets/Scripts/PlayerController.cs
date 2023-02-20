using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  public float MoveSpeed = 10.0f;
  public float JumpForce = 5.0f;
  public TextMeshProUGUI CountText;
  public GameObject WinText;
  public TextMeshProUGUI TimeText;

  private Rigidbody body;
  private SphereCollider sphereCollider;
  private float movementX;
  private float movementZ;
  private int count;
  private int totalCount = 13;
  private bool inputsEnabled;
  private Stopwatch stopwatch;

  private void Start()
  {
    body = GetComponent<Rigidbody>();
    sphereCollider = GetComponent<SphereCollider>();
    count = 0;
    SetCountText();
    WinText.SetActive(false);
    inputsEnabled = true;
    stopwatch = Stopwatch.StartNew();
  }

  private void FixedUpdate()
  {
    if (inputsEnabled)
    {
      var movement = new Vector3(movementX, 0.0f, movementZ);
      body.AddForce(movement * MoveSpeed);
    }
  }

  private void OnMove(InputValue inputValue)
  {
    if (inputsEnabled)
    {
      var movement = inputValue.Get<Vector2>();
      movementX = movement.x;
      movementZ = movement.y;
    }
  }

  private void OnJump()
  {
    if (inputsEnabled && IsGrounded())
    {
      body.velocity = new Vector3(body.velocity.x, 1.0f * JumpForce, body.velocity.z);
    }
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("PickUp"))
    {
      other.gameObject.SetActive(false);
      count++;
      SetCountText();
    }
  }

  private void SetCountText()
  {
    CountText.text = $"{count}/{totalCount}";

    if (count >= totalCount)
    {
      stopwatch.Stop();
      TimeText.SetText($"Time spent: {stopwatch.Elapsed}");
      WinText.SetActive(true);
      inputsEnabled = false;
    }
  }

  private bool IsGrounded()
  {
    var extraHeight = 0.3f;
    return Physics.Raycast(sphereCollider.bounds.center, Vector3.down, sphereCollider.bounds.extents.y + extraHeight);
  }
}
