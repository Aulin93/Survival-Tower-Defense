using UnityEngine;

[CreateAssetMenu(menuName = "BuildingState/BlueprintState")]
public class BuildingBlueprintState : BuildingBaseState
{
    [SerializeField] private LayerMask layerMask;

    public override void Enter()
    {
        UserInterfaceController.BlueprintIsSelected = true;
        Building.BecomeSelected();
    }

    public override void Run()
    {
        RaycastHit hit;
        Ray ray = CameraController.MainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, float.MaxValue, layerMask))
        {
            Building.transform.position = hit.point + hit.normal;
        }
        if (Input.GetMouseButtonDown(0) && (Inventory.CanAfford(Building.GetPrice()) || Inventory.BuildingIsInInventory(Building)) && (Base.Position - Building.transform.position).sqrMagnitude <= Base.Range * Base.Range && Base.CanCommand(Building.GetCommandCost()))
        {
            if (!Inventory.BuildingIsInInventory(Building))
            {
                Inventory.PayPrice(Building.GetPrice());
                Inventory.AddBuildingToInventory(Building);
            }
            Base.TakeCommand(Building.GetCommandCost());
            Building.Construct();
        }
        else if (Input.GetMouseButton(1))
        {
            UserInterfaceController.BlueprintIsSelected = false;
            Building.ReturnToObjectPool();
        }
    }

    public override void Exit()
    {
        UserInterfaceController.BlueprintIsSelected = false;
    }
}
