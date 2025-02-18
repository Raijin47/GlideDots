using UnityEngine;

namespace UnityChan
{
	public class RandomWind : MonoBehaviour
	{
		private SpringBone[] springBones;
        private const float windPower = 0.005f;

        private void Awake()
		{
			springBones = GetComponent<SpringManager>().springBones;
		}

		private void Update()
		{
			Vector3 force = new(Mathf.PerlinNoise(Time.time, 0.0f) * windPower, 0, 0);

			for (int i = 0; i < springBones.Length; i++)		
				springBones [i].springForce = force;			
		}
	}
}