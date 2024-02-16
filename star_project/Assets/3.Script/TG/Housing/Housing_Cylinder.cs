using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Housing_Cylinder : Net_Housing_Object
{
    
    public override void interact(string player_id, int interaction_id, int param)
    {
        base.interact(player_id);
        request_interact(interaction_id, param);
        transform.Rotate(new Vector3(90f,0f,0f));
    }

}
