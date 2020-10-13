using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour, ISelectable
{
    [SerializeField] private ResourceType resourceType;
    [SerializeField] private GameObject selectionCanvas;

    private void Awake()
    {
        GameController.AddSelectableCollider(GetComponent<BoxCollider>(), this);
    }

    public enum ResourceType
    {
        //Metals
        Iron,
        Aluminum,
        Copper,
        Lead
    }

    public ResourceType GetResourceType() { return resourceType; }

    public void BecomeSelected()
    {
        selectionCanvas.SetActive(true);
        if (UserInterfaceController.SelectedCanvas != null)
        {
            UserInterfaceController.SelectedCanvas.SetActive(false);
        }
        UserInterfaceController.SelectedCanvas = selectionCanvas;
    }
}
