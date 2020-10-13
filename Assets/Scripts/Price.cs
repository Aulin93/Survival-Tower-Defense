using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Price")]
public class Price : ScriptableObject
{
    public Resource.ResourceType[] resourceTypes;
    public float[] resourceAmounts;
    public Dictionary<Resource.ResourceType, float> typeValuePairs = new Dictionary<Resource.ResourceType, float>();

    public Price(Price other)
    {
        resourceTypes = new Resource.ResourceType[other.resourceTypes.Length];
        resourceAmounts = new float[other.resourceAmounts.Length];
        for (int i = 0; i < resourceTypes.Length; i++)
        {
            resourceTypes[i] = other.resourceTypes[i];
            resourceAmounts[i] = other.resourceAmounts[i];
            typeValuePairs.Add(resourceTypes[i], resourceAmounts[i]);
        }
    }

    public Price() { }

    public void InitializeDictionary()
    {
        for (int i = 0; i < resourceTypes.Length; i++)
        {
            typeValuePairs.Add(resourceTypes[i], resourceAmounts[i]);
        }
    }

    private void IncreasePrice(Dictionary<Resource.ResourceType, float> pairs)
    {
        foreach (Resource.ResourceType type in pairs.Keys)
        {
            if (typeValuePairs.ContainsKey(type))
            {
                typeValuePairs[type] += pairs[type];
            }
            else
            {
                typeValuePairs.Add(type, pairs[type]);
            }
        }
    }

    public static Price operator +(Price a, Price b)
    {
        List<Resource.ResourceType> types = new List<Resource.ResourceType>();
        List<float> amounts = new List<float>();

        Dictionary<Resource.ResourceType, float> pairs = new Dictionary<Resource.ResourceType, float>();
        for (int i = 0; i < a.resourceTypes.Length; i++)
        {
            pairs.Add(a.resourceTypes[i], a.resourceAmounts[i]);
        }
        for (int i = 0; i < b.resourceTypes.Length; i++)
        {
            if (pairs.ContainsKey(b.resourceTypes[i]))
            {
                pairs[b.resourceTypes[i]] += b.resourceAmounts[i];
            }
            else
            {
                pairs.Add(b.resourceTypes[i], b.resourceAmounts[i]);
            }
        }

        Price result = new Price();
        result.IncreasePrice(pairs);
        return result;
    }

    public static Price operator *(Price a, float b)
    {
        Price result = new Price(a);
        foreach (Resource.ResourceType type in a.typeValuePairs.Keys)
        {
            result.typeValuePairs[type] *= b;
        }
        return result;
    }
}
