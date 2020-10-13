using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLimitUpgrade : Upgrade, IUpgrade<Base>
{
    [SerializeField] private int commandLimitModifier;
    [SerializeField] private float priceMultiplier;
    private Price actualPrice;

    public void Upgrade(Base @base)
    {
        if(actualPrice == null)
        {
            actualPrice = Instantiate(InitialPrice);
            actualPrice.InitializeDictionary();
        }
        Base.IncreaseCommandLimit(commandLimitModifier);
        actualPrice *= priceMultiplier;
    }
}
