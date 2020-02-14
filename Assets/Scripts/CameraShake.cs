using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //variable to control the shake of the camera.  Mult in the PerlinNoise function.
    [SerializeField]
    float shakeForce = 25;

    //variable controlling camera shake stabilizing over time.
    [SerializeField]
    float recovery = 1.5f;

    private float _seed;

    //variable that triggers when (and how much) the camera will shake.  zero is the starting point but
    //when we can call on the InduceStress method in player script and pass a _trauma number greater than
    //zero to trigger the shaking.

    private float _trauma = 0;

    private void Awake()
    {
        _seed = Random.value;
    }

    //max angle in degrees that the camera will shake. Vector3.one is another way to type (1, 1, 1).
    [SerializeField]
    Vector3 maxAngle = Vector3.one * 15f;
    private void Update()
    {
        //the camera is inside of a gameobject, so we're using transform."localRotation"
        //because it sets the position relative to the parent game object that it's housed
        //inside of.  If we used transform.rotation, the camera will be set at a rotation
        //near 0, 0, 0 in world space.  

        //PerlinNoise takes a Vector2 as a parameter.  We use PerlinNoise instead of random
        //.range because the transition from one random number to the next is more smooth with
        //Perlin.  This function returns a value of 0-1, but we're changing the range to -1 to 1
        //by mult by 2, subtracting by 1.  I saw no significant change removing the "*2-1"

          
        transform.localRotation = Quaternion.Euler(new Vector3(
            maxAngle.x * Mathf.PerlinNoise(_seed, Time.time * shakeForce),
            maxAngle.y * Mathf.PerlinNoise(_seed + 1, Time.time * shakeForce),
            maxAngle.z * Mathf.PerlinNoise(_seed + 2, Time.time * shakeForce)) * _trauma);

        _trauma = Mathf.Clamp01(_trauma - recovery * Time.deltaTime);
    }
    public void CreateShake(float shake)
    {
        _trauma = Mathf.Clamp01(_trauma + shake);
    }

}


//On the player, we will need to add a blank CameraShake container that we can click and drag the camera/script into.

