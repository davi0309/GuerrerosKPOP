using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// UI persistente de la demo: selector de efectos (una escena por efecto) + Play/Pause global.
/// Adjuntar al GameObject RAÍZ del Canvas (con el EventSystem como hijo).
/// </summary>
[DisallowMultipleComponent]
public class PersistentUI : MonoBehaviour
{
    [Serializable]
    public struct EffectEntry
    {
        public string label;      // Texto que se muestra en el botón
        public string sceneName;  // Nombre EXACTO de la escena (debe estar en Build Settings)
    }

    // ───────────── Singleton / estado global ─────────────
    public static PersistentUI Instance { get; private set; }
    public static bool IsPaused { get; private set; }
    public static string CurrentEffect { get; private set; }
    public static event Action<bool> PauseChanged;

    [Header("Canvas")]
    [Tooltip("Orden de dibujo del Canvas. Debe ser mayor que el de cualquier otro Canvas de tus escenas.")]
    [SerializeField] private int sortingOrder = 100;

    [Header("Efectos")]
    [SerializeField] private EffectEntry[] effects;
    [Tooltip("Si está activo, al iniciar desde una escena que no es un efecto (ej. el menú) se carga solo el efecto indicado abajo. Déjalo apagado para que el menú se quede en el menú.")]
    [SerializeField] private bool loadDefaultEffectOnStart = false;
    [SerializeField] private int defaultEffectIndex = 0;
    [SerializeField] private RectTransform effectButtonsContainer;
    [SerializeField] private Button effectButtonPrefab;

    [Header("Reproducción")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button pauseButton;
    [Tooltip("Vuelve a reproducir el efecto actual desde el inicio (recarga su escena).")]
    [SerializeField] private Button restartButton;
    [Tooltip("Si está activo, al cambiar de efecto la demo se reanuda automáticamente.")]
    [SerializeField] private bool resumeOnEffectChange = true;

    private Button[] _effectButtons;
    private bool _isLoading;

    // Limpia los estáticos si "Enter Play Mode Options" tiene desactivado el Domain Reload.
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics()
    {
        Instance = null;
        IsPaused = false;
        CurrentEffect = null;
        PauseChanged = null;
    }

    // ───────────── Ciclo de vida ─────────────
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log($"[PersistentUI] Duplicado destruido en la escena '{gameObject.scene.name}'.", this);
            Destroy(gameObject); // Ya existe una UI persistente: este duplicado sobra.
            return;
        }

        Canvas canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("[PersistentUI] Este script debe estar en el mismo GameObject que el componente Canvas.", this);
            return;
        }

        Instance = this;
        transform.SetParent(null);          // DontDestroyOnLoad solo funciona con objetos raíz.
        DontDestroyOnLoad(gameObject);

        // Overlay no depende de ninguna cámara (con otros modos el Canvas se pierde al cambiar de escena).
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = sortingOrder;

        if (GetComponentInChildren<EventSystem>(true) == null)
            Debug.LogWarning("[PersistentUI] Falta un EventSystem como hijo del Canvas.", this);

        BuildEffectButtons();
        playButton.onClick.AddListener(Play);
        pauseButton.onClick.AddListener(Pause);
        if (restartButton != null) restartButton.onClick.AddListener(RestartEffect);
        SceneManager.sceneLoaded += OnSceneLoaded;
        RefreshUI();
    }

    private void Start()
    {
        if (Instance != this) return;

        string active = SceneManager.GetActiveScene().name;
        CurrentEffect = IndexOfEffect(active) >= 0 ? active : null;
        RefreshUI();

        // Por defecto el menú se queda en el menú. Solo se carga un efecto si se activa la opción.
        if (loadDefaultEffectOnStart && CurrentEffect == null && effects.Length > 0)
            LoadEffect(effects[Mathf.Clamp(defaultEffectIndex, 0, effects.Length - 1)].sceneName);
    }

    private void Update()
    {
        if (Instance != this) return;
        ShaderClock.Tick(IsPaused);
    }

    private void OnDestroy()
    {
        if (Instance != this) return;

        SceneManager.sceneLoaded -= OnSceneLoaded;
        Instance = null;
        IsPaused = false;
        Time.timeScale = 1f;
        AudioListener.pause = false;
    }

    // ───────────── API pública ─────────────
    public void Play() => SetPaused(false);
    public void Pause() => SetPaused(true);
    public void TogglePause() => SetPaused(!IsPaused);

    public void LoadEffect(string sceneName)
    {
        if (_isLoading || sceneName == CurrentEffect) return;

        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError($"[PersistentUI] La escena '{sceneName}' no existe o no está en Build Settings.", this);
            return;
        }

        StartCoroutine(LoadEffectRoutine(sceneName, false));
    }

    /// <summary>Vuelve a reproducir el efecto actual desde el inicio (recarga su escena).</summary>
    public void RestartEffect()
    {
        if (_isLoading || CurrentEffect == null) return;
        StartCoroutine(LoadEffectRoutine(CurrentEffect, true));
    }

    // ───────────── Reproducción ─────────────
    private void SetPaused(bool paused)
    {
        if (IsPaused == paused) return;

        IsPaused = paused;
        Time.timeScale = paused ? 0f : 1f;
        AudioListener.pause = paused;

        RefreshUI();
        PauseChanged?.Invoke(paused);
    }

    // ───────────── Carga de escenas ─────────────
    private IEnumerator LoadEffectRoutine(string sceneName, bool forceResume)
    {
        _isLoading = true;
        RefreshUI();

        // Reanudar ANTES de cargar: así Awake/Start de la nueva escena corren con el tiempo en marcha.
        // Reiniciar siempre reanuda; cambiar de efecto depende de la casilla Resume On Effect Change.
        if (forceResume || resumeOnEffectChange) SetPaused(false);

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (op != null && !op.isDone)
            yield return null; // Espera por frames (no por segundos): funciona aunque timeScale sea 0.

        _isLoading = false;
        RefreshUI();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (mode != LoadSceneMode.Single) return;

        CurrentEffect = IndexOfEffect(scene.name) >= 0 ? scene.name : null;
        if (CurrentEffect == null) SetPaused(false); // En el menú el tiempo nunca se queda congelado.
        ShaderClock.Reset(); // Cada efecto arranca con su reloj en 0.
        RemoveDuplicateEventSystems();
        RefreshUI();
    }

    // Si la escena nueva trae su propio EventSystem, Unity avisa con un error: se elimina.
    // (Asume que ese EventSystem está en su propio GameObject, como lo crea Unity por defecto.)
    private void RemoveDuplicateEventSystems()
    {
        EventSystem own = GetComponentInChildren<EventSystem>(true);
        if (own == null) return;

        foreach (EventSystem es in FindObjectsByType<EventSystem>(FindObjectsSortMode.None))
            if (es != own)
                Destroy(es.gameObject);
    }

    // ───────────── UI ─────────────
    private void BuildEffectButtons()
    {
        _effectButtons = new Button[effects.Length];

        for (int i = 0; i < effects.Length; i++)
        {
            string sceneName = effects[i].sceneName; // Copia local para el lambda.
            Button button = Instantiate(effectButtonPrefab, effectButtonsContainer);
            button.name = $"Btn_{effects[i].label}";

            TMP_Text text = button.GetComponentInChildren<TMP_Text>();
            if (text != null) text.text = effects[i].label;

            button.onClick.AddListener(() => LoadEffect(sceneName));
            _effectButtons[i] = button;
        }
    }

    private void RefreshUI()
    {
        // Play/Pause solo tienen sentido dentro de un efecto (no en el menú).
        bool inEffect = CurrentEffect != null;
        playButton.interactable = inEffect && IsPaused;
        pauseButton.interactable = inEffect && !IsPaused;
        if (restartButton != null) restartButton.interactable = inEffect && !_isLoading;

        // El botón del efecto activo se desactiva; todos se bloquean mientras se carga una escena.
        for (int i = 0; i < _effectButtons.Length; i++)
            _effectButtons[i].interactable = !_isLoading && effects[i].sceneName != CurrentEffect;
    }

    private int IndexOfEffect(string sceneName)
    {
        for (int i = 0; i < effects.Length; i++)
            if (effects[i].sceneName == sceneName) return i;
        return -1;
    }
}

/// <summary>
/// Reloj propio para shaders (reemplaza a _Time). Solo avanza en Play y se publica
/// como variable global de shader "_EffectTime".
/// </summary>
public static class ShaderClock
{
    private static readonly int EffectTimeId = Shader.PropertyToID("_EffectTime");

    public static float Seconds { get; private set; }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetStatics() => Seconds = 0f;

    public static void Tick(bool paused)
    {
        // Si más adelante quieres cámara lenta, cambia unscaledDeltaTime por deltaTime.
        if (!paused) Seconds += Time.unscaledDeltaTime;
        Shader.SetGlobalFloat(EffectTimeId, Seconds);
    }

    public static void Reset()
    {
        Seconds = 0f;
        Shader.SetGlobalFloat(EffectTimeId, 0f);
    }
}