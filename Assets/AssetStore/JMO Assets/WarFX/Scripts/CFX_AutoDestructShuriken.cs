using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]
public class CFX_AutoDestructShuriken : MonoBehaviour
{
	public bool OnlyDeactivate;

	public float colliderInterval;
	private float colliderTimer = 0.0f;

	public AudioClip audioClip;           //"audioClip"
	private SphereCollider sphereCollider;
	private AudioSource audioSource;      //"AudioSource"

	// Start is called before the first frame update
	void Start()
	{
		audioSource = this.gameObject.GetComponent<AudioSource>();
		audioSource.PlayOneShot(audioClip);

		//"CapsuleCollider"Ç™ë∂ç›ÇµÇƒÇ¢ÇÈèÍçá
		if (TryGetComponent<SphereCollider>(out sphereCollider))
		{
			sphereCollider = this.gameObject.GetComponent<SphereCollider>();
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(sphereCollider != null)
        {
			colliderTimer += Time.deltaTime;

			if (colliderTimer >= colliderInterval)
			{
				sphereCollider.enabled = false;
			}
		}
	}

	void OnEnable()
	{
		StartCoroutine("CheckIfAlive");
	}
	
	IEnumerator CheckIfAlive ()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			if(!GetComponent<ParticleSystem>().IsAlive(true))
			{
				if(OnlyDeactivate)
				{
					#if UNITY_3_5
						this.gameObject.SetActiveRecursively(false);
					#else
						this.gameObject.SetActive(false);
					#endif
				}
				else
					GameObject.Destroy(this.gameObject);
				break;
			}
		}
	}
}
