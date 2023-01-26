using System.Collections.Generic;
using UnityEngine;

namespace ObjectPooling
{
    /// <summary>
    /// Main object pooling manager
    /// </summary>
    public class ObjectPooler : MonoBehaviour
    {
        private static ObjectPooler instance;

        public static ObjectPooler Get
        {
            get
            {
                if (instance == null)
                {
                    instance = (ObjectPooler)FindObjectOfType(typeof(ObjectPooler));

                    if (instance == null)
                    {
                        Debug.LogError("An instance of " + typeof(ObjectPooler) +
                           " is needed in the scene, but there is none.");
                    }
                }

                return instance;
            }
        }

        /// <summary> List of poolable objects presets to create at start </summary>
        internal List<ObjectToPoolPreset> ObjectsToPool = new List<ObjectToPoolPreset>();

        public List<ObjectPoolerSettings> Settings = new List<ObjectPoolerSettings>();

        /// <summary> List of created poolable objects </summary>
        public List<PoolableObject> PooledObjects = new List<PoolableObject>();

        /// <summary> Objects that were created when pool was expanded will be destroyed if they won't be used for that amount of time </summary>
        public float CreatedObjectExpirationTime = 10;

        // Use this for initialization
        void Start()
        {
            CollectObjectPresetsFromSettings();

            /*Create all the instances of poolable objects we want, they will be used later in game*/
            foreach (ObjectToPoolPreset objectToPool in ObjectsToPool)
            {
                if (objectToPool == null)
                    continue;

                if (objectToPool.ObjectToPool == null)
                    continue;

                for (int i = 0; i < objectToPool.AmountToPool; i++)
                {
                    CreateObjectFromPreset(objectToPool, false);
                }
            }

            InvokeRepeating("ClearObjectPool", 10, 10);
        }

        private void CollectObjectPresetsFromSettings()
        {
            foreach (ObjectPoolerSettings settings in Settings)
            {
                ObjectsToPool.AddRange(settings.ObjectsToPool);
            }
        }
        /// <summary>
        /// Returns poolable object with given tag, it returns one from the pool or creates another one if there are no free objects in pool and it can expand the pool
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public GameObject GetPooledObject(EPoolables poolable)
        {
            /*Search for free pooled object with this tag*/
            for (int i = 0; i < PooledObjects.Count; i++)
            {
                if (PooledObjects[i] == null)
                {
                    Debug.Log("Some poolable got destroyed instead of deactivating? Index: " + i);
                    continue;
                }

                /*If object has matching tag and it's deactivated, it means it's free to use so return it*/
                if (!PooledObjects[i].gameObject.activeInHierarchy && PooledObjects[i].Poolable == poolable)
                {
                    return PooledObjects[i].gameObject;
                }
            }

            /*If there are no free objects in pool, check if pool of objects with this tag should be expanded and if yes, create another one and add to pool*/
            foreach (ObjectToPoolPreset objectPreset in ObjectsToPool)
            {
                if (objectPreset == null)
                    continue;

                if (objectPreset.ObjectToPool == null)
                    continue;

                if (objectPreset.ObjectToPool.Poolable == poolable)
                {
                    if (objectPreset.ShouldExpandIfNeeded)
                    {
                        GameObject poolableObject = CreateObjectFromPreset(objectPreset, true);
                        return poolableObject;
                    }
                }
            }
            return null;
        }

        public List<GameObject> GetAllPoolablesWithTag(EPoolables poolable)
        {
            List<GameObject> poolables = new List<GameObject>();

            /*Search for free pooled object with this tag*/
            for (int i = 0; i < PooledObjects.Count; i++)
            {
                if (PooledObjects[i] == null)
                    continue;

                /*If object has matching tag and it's deactivated, it means it's free to use so return it*/
                if (PooledObjects[i].Poolable == poolable)
                {
                    poolables.Add(PooledObjects[i].gameObject);
                }
            }

            return poolables;
        }

        public bool IsPoolableObjectAvailable(EPoolables poolable)
        {
            /*Search for free pooled object with this tag*/
            for (int i = 0; i < PooledObjects.Count; i++)
            {
                if (PooledObjects[i] == null)
                {
                    continue;
                }

                /*If object has matching tag and it's deactivated, it means it's free to use so return it*/
                if (!PooledObjects[i].gameObject.activeInHierarchy && PooledObjects[i].Poolable == poolable)
                {
                    return true;
                }
            }

            return false;
        }


        private GameObject CreateObjectFromPreset(ObjectToPoolPreset preset, bool wasPoolExpanded)
        {
            GameObject obj = (GameObject)Instantiate(preset.ObjectToPool.gameObject);
            obj.SetActive(false);
            PoolableObject poolable = obj.GetComponent<PoolableObject>();
            PooledObjects.Add(poolable);
            poolable.WasCreatedByObjectPooler = true;
            poolable.WasAddedAsExpansion = wasPoolExpanded;
            obj.transform.SetParent(transform);

            if (wasPoolExpanded)
                obj.name += " - additional";

            return obj;
        }

        private void ClearObjectPool()
        {
            PooledObjects.RemoveAll(GameObject => GameObject == null);
            List<PoolableObject> poolablesToRemove = new List<PoolableObject>();

            foreach (PoolableObject poolable in PooledObjects)
            {
                if (poolable == null)
                {
                    poolablesToRemove.Add(poolable);
                    continue;
                }
                if (!poolable.WasAddedAsExpansion)
                    continue;

                if (poolable.gameObject.activeSelf)
                    continue;

                if (Time.time - poolable.LastUsed >= CreatedObjectExpirationTime)
                {
                    poolablesToRemove.Add(poolable);
                }
            }

            foreach (PoolableObject poolableToRemove in poolablesToRemove)
            {
                PooledObjects.Remove(poolableToRemove);
                Destroy(poolableToRemove.gameObject);
            }
        }


        public void ResetActiveObjects()
        {
            foreach (PoolableObject poolable in PooledObjects)
            {
                if (poolable.gameObject.activeInHierarchy)
                {
                    poolable.gameObject.SetActive(false);
                }
            }
        }
    }
}