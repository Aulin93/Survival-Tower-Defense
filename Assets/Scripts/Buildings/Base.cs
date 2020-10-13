using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : Building
{
    public static Vector3 Position { get; private set; }
    public static float Range { get; private set; }

    [SerializeField] private UserInterfaceController uiController;
    [SerializeField] private GameObject uiMainButtonsGroup;
    private static int lives = 40;
    private static int commandLimit = 50;
    private static int currentCommand;

    private void Awake()
    {
        Range = 40;
        GameController.AddSelectableCollider(GetComponent<SphereCollider>(), this);
        stateMachine = new StateMachine(this, states);
    }

    void Update()
    {
        Position = transform.position;
        stateMachine.Run();
    }

    public override void Construct()
    {
        uiController.SwitchButtonGroup(uiMainButtonsGroup);
        stateMachine.TransitionTo<BuildingIdleState>();
        EventCoordinator.FireEvent(new BaseConstructedEvent());
    }

    public static void LoseLives(int damage)
    {
        lives -= damage;
        if(lives <= 0)
        {
            EventCoordinator.FireEvent(new PlayerLostEvent());
        }
        EventCoordinator.FireEvent(new LivesLostEvent());
    }

    public static int GetLives() { return lives; }
    public static int GetCommandLimit() { return commandLimit; }
    public static bool CanCommand(int requestedCommand) { return currentCommand + requestedCommand <= commandLimit; }
    public static void TakeCommand(int requestedCommand) {
        currentCommand += requestedCommand;
        if(currentCommand > commandLimit) { Debug.LogError("We have exceeded our command limit."); }
        EventCoordinator.FireEvent(new CommandForceChangedEvent(requestedCommand, currentCommand));
    }
    public static void RelieveCommand(int relievedCommand) {
        currentCommand -= relievedCommand;
        EventCoordinator.FireEvent(new CommandForceChangedEvent(-relievedCommand, currentCommand));
    }
    public static void IncreaseCommandLimit(int commandIncrease) { commandLimit += commandIncrease; }

    public override void ReturnToObjectPool()
    {
        Relocate();
    }
}
