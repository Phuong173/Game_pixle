using System;
using System.Collections.Generic;
using GameTool;
using Pixel_Adventure_1.Scripts;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace Pixel_Adventure_1.DataSTO
{
    [CreateAssetMenu(fileName = "ListFruitData", menuName = "Pixel Adventure 1/STO/ListFruitsData", order = 0)]
    public class STOFruitsData : ScriptableObject
    {
        public SerializableDictionaryBase<int, FruitItemData> FruitItemData => m_FruitItemData;
        [SerializeField] private SerializableDictionaryBase<int, FruitItemData> m_FruitItemData =
            new SerializableDictionaryBase<int, FruitItemData>();

        public FruitItemData FruitItemDataBase;
    }

    // Class khác tham gia
    /// Class chứa các dữ liệu thiết đặt sẵn để tham chiếu (Không runtime)
    [Serializable]
    public class FruitItemData
    {
        public EFruitType FruitType => m_FruitType;
        public ePooling PoolingName => m_PoolingName;
        public ePooling PoolingNameFruit => m_PoolingNameFruit;
        public float Cooldown => m_Cooldown;
        public Sprite Icon => icon;


        [SerializeField] private EFruitType m_FruitType;
        [SerializeField] private ePooling m_PoolingName;
        [SerializeField] private ePooling m_PoolingNameFruit;
        [SerializeField] private float m_Cooldown;
        [SerializeField] private Sprite icon;
    }

    /// 8 loại hoa quả như Apple, Bananas, ...
    public enum EFruitType
    {
        Apple,
        Bananas,
        Cherries,
        Kiwi,
        Melon,
        Orange,
        PineApple,
        Strawberry,
        Base
    }
}