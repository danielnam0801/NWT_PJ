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
        effect = Instantiate(_hitParticle) as EffectPlayer;
        effect.transform.parent = transform;
        //effect = PoolManager.Instance.Pop(_hitParticle.name) as EffectPlayer;
        effect.transform.position = _aiACtionData.CreatePoint;
        effect.StartPlay(_effectPlayTime);
    }

    public override void FinishFeedBack()
    {
        
    }

    public void StopFeedback() => Destroy(effect);
    //public void StopFeedback() => effect.StopPlay();
}
