using UnityEngine;

public class PetManager : MonoBehaviour
{
    /*
     Ư�� item ���ӽð� ����
     n�� ���� ������ ����
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


