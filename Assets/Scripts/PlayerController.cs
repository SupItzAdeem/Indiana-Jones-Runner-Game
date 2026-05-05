using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Player movement and physics variables

    [SerializeField] private Vector3 _axis;
    [SerializeField] private float _transSpeed = 4f;
    [SerializeField] private float maxTransSpeed = 11f;
    [SerializeField] private float transSpeedIncreaseRate = 0.1f;
    [SerializeField] private float jumpHeight = 2f; 
    [SerializeField] private LayerMask groundLayers;
    [SerializeField] private KeyCode _slide;
    [SerializeField] CameraController cameraController;
    [SerializeField] SegmentGenerator segmentGenerator;
    
    private float gravity = -10;
    private Rigidbody rb;
    private CapsuleCollider capsuleCollider;
    private Animator animator;
    private bool isGrounded;
    private bool isSliding = false;
    private float horizontalInput = 1f;
    private float raycastDistance = 0.6f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        CheckGroundStatus();

        if (isGrounded && _axis.y < 0)
        {
            _axis.y = 0f;
        } else
        {
            // Apply gravity
            _axis.y += gravity * Time.deltaTime;
        }

        // Gradually increase movement speed
        if (_transSpeed < maxTransSpeed)
        {
            _transSpeed += transSpeedIncreaseRate * Time.deltaTime;

        }

        // Update camera speed based on _transSpeed
        if (cameraController != null)
        {
            cameraController.SetSpeed(_transSpeed);
        }

        // Move the player forward
        rb.linearVelocity = new Vector3(horizontalInput * _transSpeed, rb.linearVelocity.y, 0);

        // Handle jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Sqrt(jumpHeight * -2 * gravity), rb.linearVelocity.z);
            SoundManager.PlaySound(SoundType.JUMP);
        }

        // Handle sliding
        if (isGrounded && Input.GetKeyDown(_slide) && !isSliding)
        {
            StartSliding();
        }

        // Update animations
        animator.SetFloat("Speed", horizontalInput);
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("VerticalSpeed", rb.linearVelocity.y);
    }

    private void CheckGroundStatus()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + Vector3.down * (capsuleCollider.height / 2 - capsuleCollider.radius);

        isGrounded = Physics.Raycast(raycastOrigin, Vector3.down, out hit, raycastDistance, groundLayers);
    }

    private void StartSliding()
    {
        animator.SetTrigger("Slide");
        isSliding = true;
        capsuleCollider.height = 1.0f;
        capsuleCollider.center = new Vector3(capsuleCollider.center.x, -0.35f, capsuleCollider.center.z);
        SoundManager.PlaySound(SoundType.SLIDE);
        Invoke(nameof(StopSliding), 1.0f);
    }

    private void StopSliding()
    {
        capsuleCollider.height = 2.0f;
        capsuleCollider.center = new Vector3(capsuleCollider.center.x, -0.1f, capsuleCollider.center.z);
        isSliding = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        _transSpeed = 0;
        this.GetComponent<PlayerController>().enabled = false;
        animator.GetComponent<Animator>().Play("Falling Backwards");
        GameManager.totalTime = GameManager.currentTime;
        SoundManager.PlaySound(SoundType.HIT);
        StartCoroutine(WaitSeconds());
    }

    IEnumerator WaitSeconds() {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 2);
        GameManager.Instance.GameOver();
    }
    
} 
