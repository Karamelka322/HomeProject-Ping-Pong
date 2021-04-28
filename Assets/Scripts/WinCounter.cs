using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class WinCounter : NetworkBehaviour
{
    [SerializeField] private Gate _gatePlayerOne;
    [SerializeField] private Gate _gatePlayerTwo;
    [Space]
    [SerializeField] private Text _counterPlayerOne;
    [SerializeField] private Text _counterPlayerTwo;
    
    [SyncVar(hook = nameof(SyncCounterPlayerOne))] private int _counterOne;
    [SyncVar(hook = nameof(SyncCounterPlayerTwo))] private int _counterTwo;

    private void OnEnable()
    {
        _gatePlayerOne.UpdatedCounter += OnUpdatedCounter;
        _gatePlayerTwo.UpdatedCounter += OnUpdatedCounter;        
    }

    private void OnDisable()
    {
        _gatePlayerOne.UpdatedCounter -= OnUpdatedCounter;
        _gatePlayerTwo.UpdatedCounter -= OnUpdatedCounter;        
    }

    private void OnUpdatedCounter(int player)
    {
        if (isServer)
        {
            ChangedCounters(player);
        }
        else
        {
            CmdChangeCounters(player);
        }
    }

    [Server]
    private void ChangedCounters(int player)
    {
        if (player == 1)
        {
            _counterOne++;
            _counterPlayerOne.text = _counterOne.ToString();
        }
        else if (player == 2)
        {
            _counterTwo++;
            _counterPlayerTwo.text = _counterTwo.ToString();
        }
    }

    [Command]
    private void CmdChangeCounters(int player)
    {
        ChangedCounters(player);
    }

    private void SyncCounterPlayerOne(int oldValue, int newValue)
    {
        _counterOne = newValue;
        _counterPlayerOne.text = _counterOne.ToString();
    }    
    
    private void SyncCounterPlayerTwo(int oldValue, int newValue)
    {
        _counterTwo = newValue;
        _counterPlayerTwo.text = _counterTwo.ToString();
    }
}
