using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Responsible for updating a progress bar with a given value
/// </summary>
public class ProgressBar : MonoBehaviour
{
    /// <summary>
    /// Reference to the foreground of the progress bar
    /// </summary>
    [SerializeField] private Image m_ForegroundFill;

    /// <summary>
    /// Sets the progress bar to a given value
    /// </summary>
    public void SetProgress(float progress)
    {
        m_ForegroundFill.fillAmount = Mathf.Clamp01(progress);
    }
}
