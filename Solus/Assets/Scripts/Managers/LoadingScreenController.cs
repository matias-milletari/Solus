using UnityEngine;
using UnityEngine.UI;
public class LoadingScreenController : MonoBehaviour
{
    public static LoadingScreenController Instance;

    private const float MIN_TIME_TO_SHOW = 0f;
    private AsyncOperation currentLoadingOperation;
    private Vector3 barFillLocalScale;
    private bool isLoading;
    private float timeElapsed;
    
    [SerializeField] private RectTransform barFillRectTransform;
    [SerializeField] private Text percentLoadedText;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

            return;
        }

        barFillLocalScale = barFillRectTransform.localScale;

        Hide();
    }

    private void Update()
    {
        if (isLoading)
        {
            SetProgress(currentLoadingOperation.progress);
            
            if (currentLoadingOperation.isDone)
            {
                Hide();
            }
            else
            {
                timeElapsed += Time.deltaTime;

                if (timeElapsed >= MIN_TIME_TO_SHOW)
                {
                    currentLoadingOperation.allowSceneActivation = true;
                }
            }
        }
    }

    private void SetProgress(float progress)
    {
        
        barFillLocalScale.x = progress;
        
        barFillRectTransform.localScale = barFillLocalScale;
        
        percentLoadedText.text = Mathf.CeilToInt(progress * 100) + "%";
    }

    public void Show(AsyncOperation loadingOperation)
    {
        
        gameObject.SetActive(true);
        
        currentLoadingOperation = loadingOperation;
        
        currentLoadingOperation.allowSceneActivation = false;
        
        SetProgress(0f);
        
        timeElapsed = 0f;

        isLoading = true;
    }

    public void Hide()
    {
        gameObject.SetActive(false);

        currentLoadingOperation = null;

        isLoading = false;
    }
}
