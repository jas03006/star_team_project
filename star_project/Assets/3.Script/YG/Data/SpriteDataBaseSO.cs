using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 게임 내에서 필요한 sprite를 저장하는 스크럽터블 오브젝트.
/// SpriteManager에서 데이터를 불러와서 사용함.
/// 만들어진 에셋 참조.
/// </summary>
[CreateAssetMenu]
public class SpriteDataBaseSO : ScriptableObject
{
    public List<Image_data> ImageData;
    public List<HousingImage_data> HousingImageData;
    public List<Image_data> emoziData;
    public List<Image_data> backgroundData;
    public List<Material> character_material_Data;
    public List<MoneyImage_data> moneyData;
}

[Serializable]
public class Image_data //기타 이미지 데이터
{
    [field: SerializeField]
    public int id { get; private set; }

    [field: SerializeField]
    public Sprite sprite { get; private set; }
}
[Serializable]
public class HousingImage_data //하우징 관련 이미지 데이터
{
    [field: SerializeField]
    public housing_itemID id { get; private set; }

    [field: SerializeField]
    public Sprite sprite { get; private set; }
}

[Serializable]
public class MoneyImage_data //재화 관련 이미지 데이터
{
    [field: SerializeField]
    public Money id { get; private set; }

    [field: SerializeField]
    public Sprite sprite { get; private set; }
}
