using UnityEngine;

public class ClickEffect : MonoBehaviour
{
    public ParticleSystem particleSystem2D;
    public Camera mainCamera;
    public AudioSource audioSource;
    public float minPitch = 0.9f;
    public float maxPitch = 1.1f;

    void Start()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnParticlesAtMouse();
        }
    }

    void SpawnParticlesAtMouse()
    {
        if (particleSystem2D == null)
            return;

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f;

        particleSystem2D.transform.position = mousePos;

        particleSystem2D.Stop();
        particleSystem2D.Play();

        audioSource.pitch = Random.Range(minPitch, maxPitch);
        audioSource.Play();
    }
}
