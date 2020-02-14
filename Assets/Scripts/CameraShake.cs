using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //variable to control the shake of the camera.  Mult in the PerlinNoise function.
    [SerializeField]
    float frequency = 25;

    [SerializeField]
    float recoverySpeed = 1.5f;

    private float _seed;

    private float _trauma = 0;

    private void Awake()
    {
        _seed = Random.value;
    }

    [SerializeField]
    Vector3 maximumTranslationShake = Vector3.one * 0.5f;

    [SerializeField]
    Vector3 maximumAngularShake = Vector3.one * 0.5f;
    private void Update()
    {
        //the camera is inside of a gameobject, so we're using transform."localPosition"
        //because it sets the position relative to the parent game object that it's housed
        //inside of.  If we used transform.position, the camera will be set at a position
        //near 0, 0, 0 in world space.  Multiply by 0.3f to reduce shake

        //PerlinNoise takes a Vector2 as a parameter.  We use PerlinNoise instead of random
        //.range because the transition from one random number to the next is more smooth with
        //Perlin.  This function returns a value of 0-1, but we're changing the range to -1 to 1
        //by mult by 2, subtracting by 1.  I saw no significant change removing the "*2-1"

        //we can comment this out because it effects the transform of the camera (shaking side to side).
        //since this game is a top-down 2D space ship game, we just need the camera to shake/rotate
        //violently.
        /*transform.localPosition = new Vector3(
            maximumTranslationShake.x * Mathf.PerlinNoise(_seed, Time.time * frequency) *2 - 1,
            maximumTranslationShake.y * Mathf.PerlinNoise(_seed + 1, Time.time * frequency) * 2 - 1,
            maximumTranslationShake.z * Mathf.PerlinNoise(_seed + 2, Time.time * frequency) * 2 - 1) * _trauma; */

        transform.localRotation = Quaternion.Euler(new Vector3(
            maximumAngularShake.x * Mathf.PerlinNoise(_seed, Time.time * frequency) * 2 - 1,
            maximumAngularShake.y * Mathf.PerlinNoise(_seed + 1, Time.time * frequency) * 2 - 1,
            maximumAngularShake.z * Mathf.PerlinNoise(_seed + 2, Time.time * frequency) * 2 - 1) * _trauma);

        _trauma = Mathf.Clamp01(_trauma - recoverySpeed * Time.deltaTime);
    }
    public void InduceStress(float stress)
    {
        _trauma = Mathf.Clamp01(_trauma + stress);
    }

}


//On the player, we will need to add a blank gameobject container that we can click and drag the camera into.

