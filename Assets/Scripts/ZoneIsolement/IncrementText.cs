using UnityEngine.UI;
using System.Collections;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
public class IncrementText : MonoBehaviour
{
    public Text m_Text;

    public Text text
    {
        get => m_Text;
        set => m_Text = value;
    }

    int m_Count;

    protected void Awake()
    {
        if (m_Text == null)
            Debug.LogWarning("Missing required Text component reference. Use the Inspector window to assign which Text component to increment.", this);
    }

    public void IncrementUIText()
    {

        if (AnomalyManager.Instance.hasAnomaly)
        {
            m_Count += 1;
            if (m_Text != null)
                m_Text.text = m_Count.ToString();
        }
    }
}
}
