using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effectCollisionsWithBorder;
    [SerializeField] private ParticleSystem _effectCollisionsWithRacket;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Racket racket))
        {
            GenerateParticle(_effectCollisionsWithRacket, collision.transform.position);
        }
        else if(collision.TryGetComponent(out Border border))
        {
            GenerateParticle(_effectCollisionsWithBorder, new Vector2(transform.position.x, collision.transform.position.y));
        }
    }

    private void GenerateParticle(ParticleSystem effect, Vector2 point)
    {
        Instantiate(effect, transform.position, Quaternion.identity);
    }
}
