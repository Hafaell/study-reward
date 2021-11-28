using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulletColors : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask layer = collision.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Pointer"))
        {
            if(HandleUI.sound)
                SoundsManager.instance.PlaySound(SoundsManager.instance.roullet, 1, 0.1f);
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        LayerMask layer = collision.gameObject.layer;
        
        if (HandleUI.instance.checkColors)
        {
            if (layer == LayerMask.NameToLayer("Pointer"))
            {
                FindObjectOfType<HandleUI>().CallColorReward(gameObject.name);

                if (gameObject.name == "laranjaCLaro")
                {
                    HandleUI.instance.ActiveSpin(true);
                }
            }

            HandleUI.instance.checkColors = false;
        }
    }
}
