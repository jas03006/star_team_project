using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class SpriteDataBaseSO : ScriptableObject
{
    public List<Image_data> ImageData;
    public List<HousingImage_data> HousingImageData;
}

[Serializable]
public class Image_data
{
    [field: SerializeField]
    public int id { get; private set; }

    [field: SerializeField]
    public Sprite sprite { get; private set; }
}
[Serializable]
public class HousingImage_data
{
    [field: SerializeField]
    public housing_itemID id { get; private set; }

    [field: SerializeField]
    public Sprite sprite { get; private set; }
}
