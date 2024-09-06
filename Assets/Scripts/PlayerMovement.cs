using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerMovement : MonoSingleton<PlayerMovement>
{
    enum MoveDirectionEnum
    {
        Stop, Up, Down, Left, Right
    }

    [SerializeField] private float _movementSpeed = 1;
    [SerializeField] private float _spriteChangeTime = 0.3f;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    [Header("Sprites")]
    [SerializeField] private Sprite _standingSprite;
    [SerializeField] private Sprite[] _leftRightSprites;
    [SerializeField] private Sprite[] _upSprites;
    [SerializeField] private Sprite[] _downSprites;

    private Rigidbody _rigidBody;

    private Vector2 _moveInputVector;
    private MoveDirectionEnum _moveDirection = MoveDirectionEnum.Stop;

    private int _spriteIterator = 0;
    private float _spriteChangeTimer = 0;

    protected override void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        _rigidBody.MovePosition(transform.position + new Vector3(_moveInputVector.x, 0, _moveInputVector.y) * _movementSpeed * Time.deltaTime);

        _spriteChangeTimer += Time.deltaTime;
        if(_spriteChangeTimer >= _spriteChangeTime)
        {
            _spriteChangeTimer = 0;
            SetSprite(_moveDirection);
        }
    }

    public void Move(CallbackContext ctx)
    {
        _moveInputVector = ctx.ReadValue<Vector2>();

        if(_moveInputVector == Vector2.zero)
        {
            SetMoveDirection(MoveDirectionEnum.Stop);
            return;
        }

        Vector2 up = new Vector2(0, 1);

        float angle = Vector2.Angle(up, _moveInputVector);

        if (angle >= 140)
            SetMoveDirection(MoveDirectionEnum.Down);
        else if (angle <= 40)
            SetMoveDirection(MoveDirectionEnum.Up);
        else
        {
            if (_moveInputVector.x >= 0)
                SetMoveDirection(MoveDirectionEnum.Right);
            else
                SetMoveDirection(MoveDirectionEnum.Left);
        }
    }

    private void SetMoveDirection(MoveDirectionEnum nMoveDirection)
    {
        if (nMoveDirection == _moveDirection)
            return;

        if (nMoveDirection == MoveDirectionEnum.Left || _moveDirection == MoveDirectionEnum.Left)
        {
            Vector3 scale = _spriteRenderer.transform.localScale;
            scale.x *= -1;
            _spriteRenderer.transform.localScale = scale;
        }

        _spriteIterator = 0;
        _spriteChangeTimer = 0;

        _moveDirection = nMoveDirection;

        SetSprite(_moveDirection, true);
    }

    private void SetSprite(MoveDirectionEnum direction, bool first = false)
    {
        if (!first)
        {
            _spriteIterator++;
            if (_spriteIterator >= 4)
                _spriteIterator = 0;
        }

        int iterator = first ? 0 : _spriteIterator;

        switch (direction)
        {
            case MoveDirectionEnum.Stop:
                _spriteRenderer.sprite = _standingSprite;
                break;
            case MoveDirectionEnum.Up:
                _spriteRenderer.sprite = _upSprites[iterator];
                break;
            case MoveDirectionEnum.Down:
                _spriteRenderer.sprite = _downSprites[iterator];
                break;
            case MoveDirectionEnum.Left:
                _spriteRenderer.sprite = _leftRightSprites[iterator];
                break;
            case MoveDirectionEnum.Right:
                _spriteRenderer.sprite = _leftRightSprites[iterator];
                break;
            default:
                break;
        }
    }
}
