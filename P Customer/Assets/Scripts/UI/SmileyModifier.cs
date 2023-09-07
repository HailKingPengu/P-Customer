using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmileyModifier : MonoBehaviour
{
    RawImage m_RawImage;

    [SerializeField] private Texture[] m_Textures;

    int listCount;
    int TextNow = 0;

    // Start is called before the first frame update
    void Start()
    {
        m_RawImage = GetComponent<RawImage>();
        listCount = m_Textures.Length;
        m_RawImage.texture = m_Textures[TextNow];
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void TaskOnClick()
    {
        Debug.Log("You have clicked the button!");
        TextNow++;
        if (TextNow >= listCount) { TextNow = 0; }
        m_RawImage.texture = m_Textures[TextNow];
    }
}
