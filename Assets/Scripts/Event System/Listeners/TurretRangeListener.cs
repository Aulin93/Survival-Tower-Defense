using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRangeListener : MonoBehaviour
{
    protected static Dictionary<Collider, Turret> cachedTurrets = new Dictionary<Collider, Turret>();
}
