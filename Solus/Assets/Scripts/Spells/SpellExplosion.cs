using UnityEngine;

public class SpellExplosion : MonoBehaviour {

	void Awake ()
	{
	    gameObject.transform.parent = null;

        Destroy(gameObject, gameObject.GetComponent<ParticleSystem>().main.duration);
	}
}
