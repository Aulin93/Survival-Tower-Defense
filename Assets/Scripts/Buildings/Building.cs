using UnityEngine.UI;
using UnityEngine;

public abstract class Building : MonoBehaviour, ISelectable
{
    [SerializeField] protected State[] states;
    [SerializeField] protected Price price;
    [SerializeField] protected GameObject selectionCanvas;
    [SerializeField] protected Text selectionText;
    protected int commandCost;
    protected bool constructed;
    protected StateMachine stateMachine;

    public virtual void InitializeStateMachine()
    {
        stateMachine = new StateMachine(this, states);
        stateMachine.TransitionTo<BuildingBlueprintState>();
    }

    public virtual void InitializePrices() {
        price = Instantiate(price);
        price.InitializeDictionary();
    }

    public virtual void RegisterListeners()
    {
        EventCoordinator.RegisterListener<WaveClearedEvent>(OnWaveCleared);
    }

    public abstract void ReturnToObjectPool();
    public abstract void Construct();

    public virtual void BecomeSelected() {
        selectionCanvas.SetActive(true);
        if(UserInterfaceController.SelectedCanvas != null)
        {
            UserInterfaceController.SelectedCanvas.SetActive(false);
        }
        UserInterfaceController.SelectedCanvas = selectionCanvas;
    }

    public virtual void Relocate() { stateMachine.TransitionTo<BuildingBlueprintState>(); }
    public virtual void OnWaveCleared(WaveClearedEvent waveCleared) { ReturnToObjectPool(); }

    public Price GetPrice() { return price; }
    public int GetCommandCost() { return commandCost; }
}
