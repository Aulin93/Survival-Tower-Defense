using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Resource.ResourceType[] resourceTypes;
    [SerializeField] private float[] initialResourceAmounts;
    [SerializeField] private float initialPowerCapacity;
    [SerializeField] private State[] states;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private static Dictionary<Collider, ISelectable> selectableColliders = new Dictionary<Collider, ISelectable>();
    [SerializeField] private LayerMask layerMask;
    private StateMachine stateMachine;

    public ObjectPool<Enemy> enemyPool { get; private set; }

    private void Awake()
    {
        Inventory.Initialize(resourceTypes, initialResourceAmounts);
        PowerGrid.Initialize(initialPowerCapacity);
        stateMachine = new StateMachine(this, states);
        enemyPool = new ObjectPool<Enemy>(enemyPrefab);
    }

    private void Update()
    {
        stateMachine.Run();
        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            if(Physics.Raycast(CameraController.MainCamera.ScreenPointToRay(Input.mousePosition), out hit, float.MaxValue, layerMask))
            {
                if (selectableColliders.ContainsKey(hit.collider)) { selectableColliders[hit.collider].BecomeSelected(); }
            }
            else
            {
                if(UserInterfaceController.SelectedCanvas != false)
                {
                    UserInterfaceController.SelectedCanvas.SetActive(false);
                    UserInterfaceController.SelectedCanvas = null;
                }
            }
        }
    }

    public static void AddSelectableCollider(Collider collider, ISelectable selectable) { selectableColliders.Add(collider, selectable); }

    public static ISelectable GetSelectableFromCollider(Collider collider)
    {
        if (selectableColliders.ContainsKey(collider))
        {
            return selectableColliders[collider];
        }
        else
        {
            return null;
        }
    }
}
