using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathDetection : MonoBehaviour
{
    private List<Vector3> _positions;

    private PlayerBehaviour _player;

    private int sameCount;

    private void CheckIfStuck()
    {
        sameCount = 0;

        for (int i = 0; i < _positions.Count; i++)
        {
            if (i != 0)
            {
                if ((_positions[i] - _positions[i - 1]).magnitude < 0.05f)
                {
                    sameCount++;
                }
                else
                {
                    sameCount = 0;
                }
            }

            if (sameCount > 50)
            {
                _player.KillPlayerByLaser();

                break;
            }
        }
    }

    private void Start()
    {
        _positions = new List<Vector3>();

        _player = GetComponent<PlayerBehaviour>();
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentPhase == Enums.PhaseType.PlayersMoving && _player == PlayerManager.Instance.CurrentPlayer)
        {
            _positions.Add(transform.position);

            if (_positions.Count > 100)
            {
                _positions.RemoveAt(0);
            }

            CheckIfStuck();
        }
    }
}
