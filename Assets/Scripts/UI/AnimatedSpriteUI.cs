using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimatedSpriteUI : MonoBehaviour
{
    public float _speed = 0.3f;
    [SerializeField] private int _frameRate = 30;
    public Image mImage = null; //el reloj
    public GameObject canvas;

    public enum SpriteToLoad { REVERSETIME, BLUEFLAME }

    public SpriteToLoad sprite;

    public Sprite[] mSprites = null;
    [SerializeField] private float mTimeperFrame = 0f;
    [SerializeField] private float mElapsedTime = 0f;
    public int mCurrentFrame = 0;
    public float _CurrentFrame = 0;
    public bool ended = false;
    public float valorOfOpacity = 155;
    [SerializeField] private bool loop = false;
    // Start is called before the first frame update
    void Start()
    {
        mImage = GetComponent<Image>();
        //enabled = false;
        if (sprite == SpriteToLoad.REVERSETIME)
            LoadReverseTimeSpriteSheet();
        else if (sprite == SpriteToLoad.BLUEFLAME)
            LoadBlueFlameSpriteSheet();
    }

    public void LoadReverseTimeSpriteSheet()
    {
        mSprites = Resources.LoadAll<Sprite>("UI/480x480/reverseTime");
        if (mSprites != null && mSprites.Length > 0)
        {
            mTimeperFrame = 1f / _frameRate;
        }
        else
            Debug.Log("No se encontró el Sprite");
    }
    public void LoadBlueFlameSpriteSheet()
    {
        mSprites = Resources.LoadAll<Sprite>("UI/Fires/BlueFlame");
        if (mSprites != null && mSprites.Length > 0)
        {
            mTimeperFrame = 1f / _frameRate;
        }
        else
            Debug.Log("No se encontró el Sprite");
    }
    public void Play()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(sprite != SpriteToLoad.BLUEFLAME)
        {
            canvas.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            canvas.GetComponent<Canvas>().sortingLayerName = "Player";
            canvas.GetComponent<Canvas>().sortingOrder = 3;
        }
        mElapsedTime += Time.deltaTime * _speed;

        if (mElapsedTime >= mTimeperFrame)
        {
            //++mCurrentFrame;
            _CurrentFrame += 1f * _speed;
            mCurrentFrame = (int)_CurrentFrame;
            SetSprite();
            if (mCurrentFrame >= mSprites.Length)
            {
                if (loop)
                    _CurrentFrame = 0;
                else
                {
                    ended = true;
                    enabled = false;
                    if (sprite != SpriteToLoad.BLUEFLAME)
                        canvas.GetComponent<Canvas>().sortingOrder = 1;
                    var newA = mImage.color;
                     newA.a = (valorOfOpacity / 255f);
                    mImage.color = newA;

                }

            }
        }
    }

    private void SetSprite()
    {
        if(mCurrentFrame >= 0 && mCurrentFrame < mSprites.Length)
        {
            mImage.sprite = mSprites[mCurrentFrame];
        }
    }

}
