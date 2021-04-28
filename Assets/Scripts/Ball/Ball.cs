using UnityEngine;
using Mirror;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : NetworkBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;
    private float _startSpeed;
    private float _frameMap;
    private Vector2 _direction;

    private void Start()
    {
        _frameMap = Camera.main.orthographicSize - transform.localScale.y / 2;
        _rigidbody = GetComponent<Rigidbody2D>();

        _startSpeed = _speed;

        RespawnBall();
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_direction * _speed);

        if (Mathf.Abs(transform.position.x) > _frameMap * 2)
            RespawnBall();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Racket racket))
        {
            _speed += 0.1f;
            _direction = new Vector2(-_direction.x, _direction.y);
        }
        else if(collision.TryGetComponent(out Border border))
        {
            _direction = new Vector2(_direction.x, -_direction.y);
        }

        Boost();
    }

    private void RespawnBall()
    {
        transform.position = Vector2.zero;
        _speed = _startSpeed;

        var randomX = Random.Range(0, 2) == 0 ? Random.Range(-0.30f, -0.70f) : Random.Range(0.30f, 0.70f);
        var randomY = Mathf.Sqrt(1 - randomX * randomX);

        _direction = new Vector2(randomX, randomY);

        Boost();
    }

    private void Boost()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.AddForce(_direction * _speed * 10, ForceMode2D.Impulse);
    }
}
