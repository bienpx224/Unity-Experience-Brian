using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using System;
using System.IO;
public class AssetManager
{
	public GameObject LoadPrefab(string assetName, string path)
	{
		Debug.Log(string.Format("{0}/{1}", path, assetName));
		return Resources.Load<GameObject>(string.Format("{0}/{1}", path, assetName));
	}

    	public Sprite GetSprite(string assetName, string path)
	{
		return Resources.Load<Sprite>(string.Format("{0}/{1}", path, assetName));
	}

    public Sprite[] GetSprites(string path)
	{
		return Resources.LoadAll<Sprite>(path);
	}

}