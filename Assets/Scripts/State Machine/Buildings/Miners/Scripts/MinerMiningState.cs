using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MinerState/MiningState")]
public class MinerMiningState : MinerBaseState
{
    [SerializeField] private float range;

    public override void Enter()
    {
        Miner.ScanForResources(range);
    }

    public override void Run()
    {
        if(PowerGrid.PowerIsAvailable(Miner.GetPowerDraw() * Time.deltaTime))
        Miner.Mine();
    }
}
