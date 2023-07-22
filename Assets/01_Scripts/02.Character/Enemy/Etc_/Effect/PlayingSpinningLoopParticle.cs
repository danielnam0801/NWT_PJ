using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingSpinningLoopParticle : Feedback
{
    [SerializeField]
    private EffectPlayer _hitParticle;
    [SerializeField]
    private float _effectPlayTime;

    private AIActionData _aiACtionData;
    private void Awake()
    {
        _aiACtionData = transform.parent.Find("AI").GetComponent<AIActionData>();
    }

    EffectPlayer effect;
    public override void CreateFeedBack()
    {
        effect = Instantiate(_hitParticle, Vector3.zero, Quaternion.identity) as EffectPlayer;
        //effect = PoolManager.Instance.Pop(_hitParticle.name) as EffectPlayer;
        effect.transform.SetParent(_aiACtionData.transform.parent);
        effect.transform.localPosition = _aiACtionData.CreatePoint + Vector3.down;
        effect.transform.localRotation = Quaternion.identity;
        effect.StartPlay(_effectPlayTime);
    }

    public override void FinishFeedBack()
    {
        
    }

    public void StopFeedback()
    {
        if(effect != null)
            Destroy(effect.gameObject);
    }
    //public void StopFeedback() => effect.StopPlay();
}
