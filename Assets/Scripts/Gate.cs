using UnityEngine;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    [SerializeField] private int _numberPlayer;

    public event UnityAction<int> UpdatedCounter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Ball ball))
        {
            UpdatedCounter?.Invoke(_numberPlayer);
        }
    }
}
