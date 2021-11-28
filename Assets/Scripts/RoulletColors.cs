using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulletColors : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        LayerMask layer = collision.gameObject.layer;
        
        if (HandleUI.checkColors)
        {
            if (layer == LayerMask.NameToLayer("Pointer"))
            {
                FindObjectOfType<HandleUI>().CallColorReward(gameObject.name);
            }

            HandleUI.checkColors = false;
        }
    }
}
