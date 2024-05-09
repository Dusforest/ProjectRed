using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Elvenwood
{

	public class ObjectPool
	{
		Transform mObjectPoolParent;
		GameObject mPooledObject;
		List<GameObject> mObjectList;

		public ObjectPool(GameObject obj, int maxSize, Transform objectPoolParent)
		{
			mPooledObject = obj;
			mObjectPoolParent = objectPoolParent;
			mObjectList = new List<GameObject>();
			for (int i = 0; i < maxSize; i++)
			{
				GameObject tObj = GameObject.Instantiate(mPooledObject, mObjectPoolParent);
				tObj.SetActive(false);
				mObjectList.Add(tObj);
			}
		}

		public GameObject GetObject()
		{
			for (int i = 0; i < mObjectList.Count; i++)
			{
				if (mObjectList[i].activeSelf == false)
				{
					return mObjectList[i];
				}
			}

			Debug.LogWarning(mPooledObject.name + "的对象池超限，已扩容。");
			GameObject tObj = GameObject.Instantiate(mPooledObject, mObjectPoolParent);
			tObj.SetActive(false);
			mObjectList.Add(tObj);

			return tObj;
		}
	}
}


