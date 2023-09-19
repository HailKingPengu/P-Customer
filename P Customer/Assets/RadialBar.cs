using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class RadialBar : MonoBehaviour
{

    [SerializeField] private Gradient gradient;
    [SerializeField] private Image sprite;
    public float progress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sprite.color = gradient.Evaluate(progress);
        sprite.fillAmount = progress;
    }
}
