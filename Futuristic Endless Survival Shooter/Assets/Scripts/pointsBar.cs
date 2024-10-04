using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class pointsBar : MonoBehaviour
{

    public float maxPoints = 100;
    public float points;
    public Slider pointsSlider;
    public Slider easePointsSlider;
    public float lerpSpeed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        points = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (easePointsSlider.value != points)
        {
            easePointsSlider.value = points;
        }

        if (pointsSlider.value != easePointsSlider.value)
        {
            pointsSlider.value = Mathf.Lerp(pointsSlider.value, points, lerpSpeed);
        }
    }
}
