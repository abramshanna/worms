using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraHealth : PickupObject
{
    public override bool OnPickup()
    {
        print("You got extra health!");
        return true;
    }

}
