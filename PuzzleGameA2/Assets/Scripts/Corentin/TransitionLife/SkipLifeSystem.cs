using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipLifeSystem : MonoBehaviour
{
    public void SkipLife()
    {
        if (PlayerManager.Instance != null)
        {
            if (PlayerManager.Instance.CurrentPlayer != null)
            {
                PlayerManager.Instance.CurrentPlayer.KillPlayerByLaser();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
