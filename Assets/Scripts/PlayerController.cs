using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
  public float MoveSpeed = 1.0f;
  public TextMeshProUGUI CountText;
  public GameObject WinText;

  private Rigidbody body;
  private float movementX;
  private float movementY;
  private int count;

  private void Start()
  {
    body = GetComponent<Rigidbody>();
    count = 0;
    SetCountText();
    WinText.SetActive(false);
  }

  private void FixedUpdate()
  {
    var movement = new Vector3(movementX, 0.0f, movementY);
    body.AddForce(movement * MoveSpeed);
  }

  private void OnMove(InputValue inputValue)
  {
    var movement = inputValue.Get<Vector2>();
    movementX = movement.x;
    movementY = movement.y;
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
    CountText.text = count.ToString();

    if (count >= 13)
    {
      WinText.SetActive(true);
    }
  }
}
