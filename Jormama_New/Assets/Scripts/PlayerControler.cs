using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private Transform _model;
    [SerializeField] private float speed = 5;
    [SerializeField] private float turnSpeed = 700;
    [SerializeField] private PlayerInputActions playerControls;
    private Vector3 input;
    private InputAction move;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        move = playerControls.Player.Move;
        move.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
    }

    private void Update()
    {
        GatherInput();
        Look();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void GatherInput()
    {
        input = new Vector3(move.ReadValue<Vector2>().x, 0, move.ReadValue<Vector2>().y);
    }

    

    private void Move()
    {
        _rigidBody.MovePosition(transform.position + input.ToIso() * input.normalized.magnitude * speed * Time.deltaTime);
    }
    private void Look()
    {
        if (input == Vector3.zero) return;

        Quaternion rot = Quaternion.LookRotation(input.ToIso(), Vector3.up);
        _model.rotation = Quaternion.RotateTowards(_model.rotation, rot, turnSpeed * Time.deltaTime);
    }
}

public static class Helpers
{
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}
