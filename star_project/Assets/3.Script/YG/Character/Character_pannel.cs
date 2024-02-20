using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Character_pannel : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text character_name;
    [SerializeField] private TMP_Text level;

    [SerializeField] private TMP_Text basic; //기본 능력
    [SerializeField] private TMP_Text special; //특수 능력
    [SerializeField] private TMP_Text unique; //고유 능력

    private void Awake()
    {
        //UI에 요소 할당
        image = transform.GetChild(0).GetComponent<Image>();
        character_name = transform.GetChild(1).GetChild(1).GetComponent<TMP_Text>();
        level = transform.GetChild(2).GetChild(1).GetComponent<TMP_Text>();
        basic = transform.GetChild(3).GetChild(1).GetComponent<TMP_Text>();
        special = transform.GetChild(4).GetChild(1).GetComponent<TMP_Text>();
        unique = transform.GetChild(5).GetChild(1).GetComponent<TMP_Text>();
    }

    public void UI_update(Character character)
    {
        image.sprite = SpriteManager.instance.Num2Sprite(character.sprite);
        character_name.text = character.character_name;
        level.text = $"{character.curlevel}";

        basic.text = $"{character.basic}";
        special.text = $"{character.special}";
        unique.text = $"{character.unique}";
    }

    public void Level_update(Character character)
    {
        level.text = $"{character.curlevel}";
    }
}
