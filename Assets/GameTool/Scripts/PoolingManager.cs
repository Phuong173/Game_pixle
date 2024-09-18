using System.Collections;
using System.Collections.Generic;
using GameTool;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

namespace GameTool
{
    public class PoolingManager : SingletonMonoBehavior<PoolingManager>
    {
        [SerializeField] private SerializableDictionaryBase<ePooling, MonoBehaviour> PoolingBehaviors;
        private Dictionary<ePooling, List<MonoBehaviour>> Pooler = new Dictionary<ePooling, List<MonoBehaviour>>();
        private GameObject parentPooler;

        protected override void Awake()
        {
            base.Awake();
            this.RegistEvent(eEventType.SceneLoaded, OnSceneLoaded);
        }

        public void OnSceneLoaded()
        {
            Pooler = new Dictionary<ePooling, List<MonoBehaviour>>();
            foreach (KeyValuePair<ePooling,MonoBehaviour> poolingBehavior in PoolingBehaviors)
            {
                Pooler.Add(poolingBehavior.Key, new List<MonoBehaviour>());
            }
            parentPooler = new GameObject("PoolerInstance");
        }

        public MonoBehaviour GetObject(ePooling objectName, Transform parent = null)
        {
            MonoBehaviour monoBehaviour = Pooler[objectName].Find(behaviour => !behaviour.gameObject.activeSelf);
            if (!monoBehaviour)
            {
                monoBehaviour = Instantiate(PoolingBehaviors[objectName]);
                Pooler[objectName].Add(monoBehaviour);
            }
            if (!parent)
            {
                parent = parentPooler.transform;
            }
            monoBehaviour.transform.parent = parent;
            monoBehaviour.gameObject.SetActive(true);
            return monoBehaviour;
        }

        public MonoBehaviour GetObject(ePooling objectName, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            MonoBehaviour monoBehaviour = Pooler[objectName].Find(behaviour => !behaviour.gameObject.activeSelf);
            if (!monoBehaviour)
            {
                monoBehaviour = Instantiate(PoolingBehaviors[objectName], parent);
                Pooler[objectName].Add(monoBehaviour);
            }
            if (!parent)
            {
                parent = parentPooler.transform;
            }
            monoBehaviour.transform.parent = parent;
            monoBehaviour.transform.position = position;
            monoBehaviour.transform.rotation = rotation;
            monoBehaviour.gameObject.SetActive(true);
            return monoBehaviour;
        }
    }

    public enum ePooling
    {
        EnemySon,
        AmmoBase,
        AppleAmmo,
        BananasAmmo,
        CherriesAmmo,
        KiwiAmmo,
        MelonAmmo,
        OrangeAmmo,
        PineAppleAmmo,
        StrawberryAmmo,
        FruitCollected,
        AppleFruit,
        BananaFruit,
        CherriesFruit,
        KiwiFruit,
        MelonFruit,
        OrangeFruit,
        PineAppleFruit,
        StrawberryFruit,
        StrawberryChild,
        EnemyRocket,
    }
}