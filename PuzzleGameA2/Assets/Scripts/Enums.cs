using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enums
{
    public enum ShapePower
    {
        None,
        Jump,
        ChangeDirection,
        SideJump,
        Acceleration,
        InverseGravity
    }
    public enum PhaseType
    {
        PlateformePlacement = 0,
        PlayersMoving = 1,
        ChoicePhase = 2,
        GameEndPhase = 3
    }
}
