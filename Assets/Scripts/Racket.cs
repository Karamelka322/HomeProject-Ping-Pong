using UnityEngine;
using Mirror;

public class Racket : NetworkBehaviour
{
    [SerializeField] private float _speed = 0.1f;

    private float _frameMap;

    private void Start()
    {
        _frameMap = Camera.main.orthographicSize - transform.localScale.y / 2;
    }

    private void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        if (Input.GetKey(KeyCode.W) && _frameMap > transform.position.y)
            transform.Translate(0, _speed, 0);
        else if (Input.GetKey(KeyCode.S) && -_frameMap < transform.position.y)
            transform.Translate(0, -_speed, 0);
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
    }
}
