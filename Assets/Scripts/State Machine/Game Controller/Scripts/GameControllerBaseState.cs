using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameControllerBaseState : State
{
    private GameController gameController;

    public GameController GameController => gameController = gameController ?? (GameController)stateMachine.owner;
}
