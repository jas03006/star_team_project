using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Housing_Cylinder : Net_Housing_Object
{
    
    public override void interact()
    {
        int interaction_id = 0;
        int param=0;
        base.interact();
        request_interact(interaction_id, param);
        transform.Rotate(new Vector3(90f,0f,0f));
    }

}
