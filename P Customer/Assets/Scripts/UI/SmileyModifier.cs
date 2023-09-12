using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmileyModifier : MonoBehaviour
{
    RawImage m_RawImage;

    [SerializeField] private Texture[] m_Textures;

    private enum followingVariable {
        power,
        happiness
    };
    [SerializeField] private followingVariable followingVariableChosen;
    [SerializeField] private int CorrectSpriteIndex;

    int listCount = 0;
    int TextNow = 0;

    GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        m_RawImage = GetComponent<RawImage>();
        listCount = m_Textures.Length;
        m_RawImage.texture = m_Textures[TextNow];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSprite();
    }

    void UpdateSprite () {
    float calc = 0;
    switch(followingVariableChosen) {
        case followingVariable.power :
            calc =  (gameManager.power / gameManager.powerNeeded) * (CorrectSpriteIndex) ;
        break;
        case followingVariable.happiness :
            calc =  (gameManager.happiness / gameManager.happinessNeeded) * (CorrectSpriteIndex) ;
        break;
    }
        calc = Mathf.Clamp(calc,0,listCount-1);
    ChangeTextureIndex(Mathf.RoundToInt(calc));

    }

    void ChangeTextureIndex (int newIndex) {
        if (listCount < 1) { return; }
        TextNow = newIndex;
        Mathf.Clamp(TextNow, 0, listCount - 1);
        m_RawImage.texture = m_Textures[TextNow];
    }

}
