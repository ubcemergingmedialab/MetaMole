using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Meta;
using Meta.HandInput;

public class EEGChangeColour : Interaction {

    private HandFeature _handFeature;
    public GameObject obj;

    protected override bool CanEngage(Hand handProxy)
    {
        return GrabbingHands.Count == 1;
    }

    protected override void Engage()
    {
        _handFeature = GrabbingHands[0];

        //rigidbody should be kinematic as to not interfere with grab translation
        SetIsKinematic(true);

        SetGrabOffset(_handFeature.Position);
    }

    protected override bool CanDisengage(Hand handProxy)
    {
        if (_handFeature != null && handProxy.Palm == _handFeature)
        {
            foreach (var hand in GrabbingHands)
            {
                if (hand != _handFeature)
                {
                    _handFeature = hand;
                    SetGrabOffset(_handFeature.Position);
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    protected override void Disengage()
    {
        SetIsKinematic(false);
        _handFeature = null;
    }

    protected override void Manipulate()
    {
        Move(_handFeature.Position);
        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in rends)
        {
            Color oldColour = rend.material.color;
            float relAlpha = EEGData.GetRelativeAlpha();
            Color newColour = new Color(relAlpha, 0.1f, (1.0f - relAlpha));
            rend.material.color = Color.Lerp(oldColour, newColour, Time.deltaTime);
        }
    }
}
