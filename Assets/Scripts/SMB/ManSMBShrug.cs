using UnityEngine;

public class ManSMBShrug : SceneLinkedSMB<HumanBehavior>
{
    public override void OnSLStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnSLStateExit(animator, stateInfo, layerIndex);

        m_MonoBehaviour.ShrugHandling();
    }
}
