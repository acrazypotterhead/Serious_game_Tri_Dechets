using UnityEngine.UI;
using System.Collections;

namespace UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets
{
public class IncrementText : MonoBehaviour
{
    public Text m_Text;
    private int m_Count = 0;

    public void Increment()
    {
        m_Count++;
        if (m_Text != null)
            m_Text.text = m_Count.ToString();
    }
}
}
