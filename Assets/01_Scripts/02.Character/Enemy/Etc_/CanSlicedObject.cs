using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySpriteCutter;
using DG.Tweening;

public class CanSlicedObject : MonoBehaviour, ICuttable
{
    [SerializeField]
    int _headX, _headY, _headWidth, _headHeight, _textureWidth, _textureHeight;
    Vector2 _scale;
    Transform _parent;
    Renderer _renderer;
    Material enemyMat = null;
    
    public bool isFirstCutting = false;
    private bool isSpriteRender;
    
    public void SetValues(bool isSprite)
    {
        isSpriteRender = isSprite;
    }

    public void SetValues(int headX, int headY, int headWidth, int headHeight, int textureWidth, int textureHegiht, Vector2 scale, Transform parent, Material mat)
    {
        _renderer = GetComponent<Renderer>();
        enemyMat = mat;

        if (_renderer.GetType() == typeof(SpriteRenderer))
        {
            _renderer.material = enemyMat;
            if (_renderer.material.HasProperty("_Dissolve"))
            {
                _renderer.material.SetFloat("_Dissolve", 1);
            }
            isSpriteRender = true;
        }
        else
        {
            isSpriteRender = false;
        }
        //if (_renderer.GetType() == typeof(MeshRenderer))
        //{
        //    Material myCurrentMat = _renderer.material;
        //    List<Material> materials = new List<Material>();
        //    //materials.Add(myCurrentMat);
        //    if (enemyMat) materials.Add(enemyMat);
        //    _renderer.materials = materials.ToArray();

        //    if (_renderer.materials[0].HasProperty("_Dissolve"))//DeadMaterial 위치
        //    {
        //        _renderer.materials[0].SetFloat("_Dissolve", 1);
        //    }
        //    isMeshRenderer = true;
        //}

        this._headX = headX;
        this._headY = headY;
        this._headWidth = headWidth;
        this._headHeight = headHeight;
        this._textureWidth = textureWidth;
        this._textureHeight = textureHegiht;
        this._scale = scale;
        this._parent = parent;
        transform.localScale = scale;
        isFirstCutting = true; 
        
        DistroyThisObj(1.5f);
    }

    public void DistroyThisObj(float time)
    {
        StartCoroutine(SpriteDissolve(time));
    }

    IEnumerator SpriteDissolve(float time)
    {
        yield return new WaitForSeconds(time);
        if (isSpriteRender)
        {
            Sequence seq = DOTween.Sequence();
            Tween dissolve = DOTween.To(
                () => _renderer.material.GetFloat("_Dissolve"),
                x => _renderer.material.SetFloat("_Dissolve", x),
                0f,
                2.5f);

            seq.Append(dissolve);
            seq.OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
        else
            Destroy(gameObject);
    }
    
    public void SpriteCutting(Vector2 InputVec, Vector2 OutputVec, int layerMask = -1)
    {
        SpriteCutterOutput output = SpriteCutter.Cut(new SpriteCutterInput()
        {
            lineStart = InputVec,
            lineEnd = OutputVec,
            headX = _headX,
            headY = _headY,
            headWidth = _headWidth,
            headHeight = _headHeight,
            textureWidth = _textureWidth,
            textureHeight = _textureHeight,
            scale = _scale,
            isFirstCutting = isFirstCutting,
            isboneCutting = !isSpriteRender,
            gameObject = this.gameObject,
            gameObjectCreationMode = SpriteCutterInput.GameObjectCreationMode.CUT_OFF_ONE,
        });

        if (output != null && output.secondSideGameObject != null)
        {
            isFirstCutting = false;
            //3초 후 사라지게
            output.firstSideGameObject.AddComponent<PartsDissapear>();
            output.secondSideGameObject.AddComponent<PartsDissapear>();

            //CanSlicedObject canSlice =  output.secondSideGameObject.AddComponent<CanSlicedObject>();
           
            //canSlice.SetValues(_headX, _headY, _headWidth, _headHeight, _textureWidth, _textureHeight, _scale, _parent, output.secondSideGameObject.GetComponent<Renderer>().material);

            Rigidbody2D newRigidbody = output.firstSideGameObject.GetComponent<Rigidbody2D>();
            Debug.Log(output.firstSideGameObject.name);
            output.secondSideGameObject.AddComponent<Rigidbody2D>();
            Rigidbody2D newRigidbody2 = output.secondSideGameObject.GetComponent<Rigidbody2D>();

            newRigidbody.angularDrag = 0.5f;
            newRigidbody2.angularDrag = 0.5f;
            newRigidbody.AddForceAtPosition((newRigidbody.position - InputVec) * 3, newRigidbody.position, ForceMode2D.Impulse);
            newRigidbody2.AddForceAtPosition((newRigidbody2.position - InputVec) * 3, newRigidbody2.position, ForceMode2D.Impulse);
        }
    }
}
