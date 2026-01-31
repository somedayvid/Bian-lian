using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMask : Card
{
    public override void UseCard()
    {
        base.UseCard();
        Debug.Log("Red Mask effect activated: Increase attack power by 20% for 3 turns.");
        // Implement Red Mask specific effect logic here
    }

  
}
