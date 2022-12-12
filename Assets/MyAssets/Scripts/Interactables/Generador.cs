using UnityEngine;
using System.Collections;

public class Generador : MonoBehaviour {

	[SerializeField]
	private GameObject[] obj;
	private GameObject createdObj;
	
	[SerializeField]
	private float tiempoMin = 1.25f;
	[SerializeField]
	private float tiempoMax = 2.5f;

	// Use this for initialization
	void Start () {
		Generar();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void Generar(){
		if (!createdObj)
        {
			createdObj = Instantiate(obj[0], transform.position, Quaternion.identity);
        }
	}

	private void OnTriggerEnter(Collider collider)
	{
		Destroy(createdObj);
		Invoke("Generar", Random.Range(tiempoMin, tiempoMax));
	}
}
