using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float maxHitPoints { get; private set; }
    public float hitPoints { get; private set; }
    public float ballisticResistance { get; private set; }
    public float explosiveResistance { get; private set; }
    public float energyResistance { get; private set; }

    [SerializeField] private Slider healthBar;
    private Dictionary<DamageType, float> resistanceDictionary = new Dictionary<DamageType, float>();

    public enum DamageType
    {
        Ballistic, Explosive, Energy
    }

    /// <summary>
    /// Initializes the health component, always do this only once
    /// </summary>
    /// <param name="initalHitPoints">The initial amount of hit points</param>
    /// <param name="initialResistances">The initial resistances, ballistic resistance in x, explosive resistance in y and energy resistance in z</param>
    public void Initialize(float initalHitPoints, Vector3 initialResistances)
    {
        maxHitPoints = initalHitPoints;
        hitPoints = initalHitPoints;
        healthBar.maxValue = initalHitPoints;
        healthBar.value = hitPoints;
        ballisticResistance = initialResistances.x;
        explosiveResistance = initialResistances.y;
        energyResistance = initialResistances.z;
        resistanceDictionary.Add(DamageType.Ballistic, ballisticResistance);
        resistanceDictionary.Add(DamageType.Energy, energyResistance);
        resistanceDictionary.Add(DamageType.Explosive, explosiveResistance);
    }

    private void Update()
    {
        healthBar.transform.LookAt(CameraController.MainCamera.transform);
    }

    /// <summary>
    /// Reduces hit points and returns the damage taken after resistance mitigation.
    /// </summary>
    /// <param name="damageValue">Amount of unmitigated damage.</param>
    /// <param name="damageType">The attacks damage type, damage will be mitigated depending on damage type.</param>
    /// <returns></returns>
    public float TakeDamage(float damageValue, DamageType damageType)
    {
        float damageTaken = damageValue * resistanceDictionary[damageType];
        hitPoints -= damageTaken;
        healthBar.value = hitPoints;
        if(hitPoints <= 0)
        {
            hitPoints = 0;
            healthBar.gameObject.SetActive(false);
        }
        return damageTaken;
    }
}
