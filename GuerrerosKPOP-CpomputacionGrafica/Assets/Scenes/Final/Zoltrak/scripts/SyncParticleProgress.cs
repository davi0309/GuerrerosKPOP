using UnityEngine;

[ExecuteAlways]
public class SyncParticleProgress : MonoBehaviour
{
    private ParticleSystem ps;
    private Renderer rend;
    private Material mat;

    [Header("Configuración de Tiempos")]
    [Tooltip("Tiempo de espera (delay) antes de que la runa comience a llenarse")]
    public float startDelay = 1.23f;

    [Tooltip("Activa esto si quieres definir una duración manual en lugar de usar la del Particle System")]
    public bool overrideDuration = false;
    public float customDuration = 1f;

    void OnEnable()
    {
        InitializeComponents();
    }

    void Update()
    {
        InitializeComponents();

        if (ps != null && mat != null)
        {
            float duration = overrideDuration ? customDuration : ps.main.duration;
            float currentTime = ps.time;

            float normalizedProgress = 1f; // Por defecto empieza en 1 (vacío)

            // Si todavía estamos en el delay, se mantiene en 1 (vacío)
            if (currentTime < startDelay)
            {
                normalizedProgress = 1f;
            }
            else
            {
                // Si ya pasó el delay, va de 1 a 0 para que se llene correctamente
                float activeTime = currentTime - startDelay;
                float activeDuration = duration - startDelay;

                if (activeDuration <= 0f) activeDuration = 0.001f;

                float t = Mathf.Clamp01(activeTime / activeDuration);
                normalizedProgress = 1f - t; // Invertido: de 1 (vacío) a 0 (lleno)
            }

            // Actualiza la propiedad en el shader
            mat.SetFloat("_Progress", normalizedProgress);
        }
    }

    void InitializeComponents()
    {
        if (ps == null)
            ps = GetComponent<ParticleSystem>();

        if (rend == null)
            rend = GetComponent<Renderer>();

        if (rend != null)
        {
            if (Application.isPlaying)
            {
                if (mat == null) mat = rend.material;
            }
            else
            {
                mat = rend.sharedMaterial;
            }
        }
    }
}