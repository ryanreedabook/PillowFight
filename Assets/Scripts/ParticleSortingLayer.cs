using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour {

	public string sortLayer;

	void Start () 
	{
		particleSystem.renderer.sortingLayerName = sortLayer;
	}

	void UpdateSortingLayer(string layer)
	{
		sortLayer = layer;
		particleSystem.renderer.sortingLayerName = sortLayer;
	}
}
