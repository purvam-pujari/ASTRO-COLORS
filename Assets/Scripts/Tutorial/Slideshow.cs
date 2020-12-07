using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slideshow : MonoBehaviour
{
    public Sprite[] slides = new Sprite[8];
    // public Sprite temp;
    // public RectTransform rectTransform;
    public float changeTime = 10.0f;
    private int currentSlide = 0;
    private float timeSinceLast = 1.0f;
    public Image image;
    public Text SlideNumber;
    // Start is called before the first frame update
    void Start()
    {
        image= GameObject.FindGameObjectWithTag("Tutorial").GetComponent<Image>();
        image.sprite = slides[currentSlide];
        // SlideNumber.text = currentSlide+1 + "/"  + slides.Length;
    }

    void Update() {
        
    }

    public void NextSlide() {
        currentSlide = (currentSlide + 1) % 8;
        image.sprite = slides[currentSlide];
        SlideNumber.text = currentSlide+1 + "/"  + slides.Length;
    }

    public void PrevSlde() {
        if(currentSlide == 0) {
            currentSlide = slides.Length - 1;
        } else{
            currentSlide -= 1;
        }
        image.sprite = slides[currentSlide];
        SlideNumber.text = currentSlide+1 + "/"  + slides.Length;
    }
}
