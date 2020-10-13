using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Inventory 
{
    private static Dictionary<Resource.ResourceType, float> resourcesInInventory = new Dictionary<Resource.ResourceType, float>();
    private static HashSet<Building> buildings = new HashSet<Building>();

    public static void Initialize(Resource.ResourceType[] resourceTypes, float[] amounts)
    {
        for (int i = 0; i < resourceTypes.Length; i++)
        {
            resourcesInInventory.Add(resourceTypes[i], amounts[i]);
        }
    }

    public static void AddBuildingToInventory(Building building) { buildings.Add(building); }

    public static bool BuildingIsInInventory(Building building) { return buildings.Contains(building); }

    public static float GetResourceAmount(Resource.ResourceType resourceType) {
        if (!resourcesInInventory.ContainsKey(resourceType))
        {
            resourcesInInventory.Add(resourceType, 0);
        }
        return resourcesInInventory[resourceType];
    }

    public static void AddResourceToInventory(Resource.ResourceType resourceType, float amount) {
        if(resourcesInInventory.ContainsKey(resourceType))
        resourcesInInventory[resourceType] += amount;
        else { resourcesInInventory.Add(resourceType, amount); }
    }

    public static void RemoveResourceFromInventory(Resource.ResourceType resourceType, float amount) {
        if (!resourcesInInventory.ContainsKey(resourceType))
        {
            Debug.LogError("Inventory does not contain " + resourceType);
            return;
        }
        float amountOfResourceInInventory = resourcesInInventory[resourceType];
        amountOfResourceInInventory -= amount;
        if(amountOfResourceInInventory < 0) {
            amountOfResourceInInventory = 0;
            Debug.LogError("Removed more resources from inventory than was there.");
        }
        resourcesInInventory[resourceType] = amountOfResourceInInventory;
    }

    public static bool CanAfford(Price price)
    {
        if(price.resourceTypes.Length == 0) { return true; }
        foreach (Resource.ResourceType resourceType in price.typeValuePairs.Keys)
        {
            if(GetResourceAmount(resourceType) < price.typeValuePairs[resourceType])
            {
                return false;
            }
        }
        return true;
    }

    public static void PayPrice(Price price)
    {
        if (CanAfford(price))
        {
            foreach (Resource.ResourceType type in price.typeValuePairs.Keys)
            {
                RemoveResourceFromInventory(type, price.typeValuePairs[type]);
            }
        }
    }
}
