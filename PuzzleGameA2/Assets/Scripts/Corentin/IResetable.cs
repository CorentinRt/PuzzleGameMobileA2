using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IResetable
{
    public Vector3 StartPosition { get; set; }

    void InitReset();
    void ResetActive();
    void Desactive();
}
