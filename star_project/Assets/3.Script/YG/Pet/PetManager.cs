using UnityEngine;

public class PetManager : MonoBehaviour
{
    /*
     특정 item 지속시간 증가
     n초 마다 아이템 지급
     */

    private pet_ID pet_ID;

    public void Select_pet(int pet_id)
    {
        pet_ID = (pet_ID)pet_id;
        
        switch (pet_ID)
        {
            case pet_ID.Yellow:
                break;
            case pet_ID.Red:
                break;
            case pet_ID.Blue:
                break;
            case pet_ID.Purple:
                break;
            case pet_ID.Green:
                break;
            default:
                break;
        }
    }
}


