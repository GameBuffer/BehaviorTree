using System.ComponentModel;
using System.Collections.Generic;
/// <summary>
/// class:  并行节点
/// Check:  依次调用所有的子节点的Check方法，若所有的子节点都返回True，则自身也返回True，否则，返回False
/// Tick:   调用所有子节点的Tick方法，若并行节点是“或者”的关系，则只要有一个子节点返回运行结束，那自身就返回运行结束。
///         若并行节点是“并且”的关系，则只有所有的子节点返回结束，自身才返回运行结束
/// </summary>

public class BTControlNodeParallel : BTControlNodeBase {
    protected BTParallelFinishCondition _finishCondition;
    protected List<BTRunningStatus> _childNodesStatus;

    public BTControlNodeParallel () : base () {
        _childNodesStatus = new List<BTRunningStatus> ();
    }

    public BTControlNodeParallel (BTNodeBase parentNode, BTPreCondition preCondition = null) : base (parentNode, preCondition) {
        _childNodesStatus = new List<BTRunningStatus> ();
    }

    protected override bool DoCheck (BTInput input) {
        base.Check (input);
        var childCount = _childNodes.Count;
        for (int i = 0; i < childCount; i++) {
            var node = _childNodes[i];
            if (_childNodesStatus[i] == BTRunningStatus.Executing) {
                if (!node.Check (input)) {
                    return false;
                }
            }
        }
        return true;
    }

    protected override void DoTransition (BTInput input) {
        base.DoTransition (input);
        var childCount = _childNodes.Count;
        for (int i = 0; i < childCount; i++) {
            _childNodesStatus[i] = BTRunningStatus.Executing;
            var node = _childNodes[i];
            node.Transition (input);
        }
    }

    protected override BTRunningStatus DoTick (BTInput input, ref BTOutput output) {
        base.DoTick (input, ref output);

        var finishChildCount = 0;
        var childCount = _childNodes.Count;
        for (int i = 0; i < childCount; i++) {
            var node = _childNodes[i];
            if (_finishCondition == BTParallelFinishCondition.OR) {
                if (_childNodesStatus[i] == BTRunningStatus.Executing) {
                    _childNodesStatus[i] = node.Tick (input, ref output);
                } else {
                    RestChildNodeStatus();
                    return BTRunningStatus.Finish;
                }
            } else if (_finishCondition == BTParallelFinishCondition.AND) {
                if(_childNodesStatus[i] == BTRunningStatus.Executing){
                    _childNodesStatus[i] = node.Tick(input, ref output);
                }else{
                    finishChildCount++;
                }
            } else {
                // Debug.LogError("BTParallelFinishCondition Error");
            }
        }

        if(finishChildCount == _childNodes.Count){
            RestChildNodeStatus();
            return BTRunningStatus.Finish;
        }

        return BTRunningStatus.Executing;
    }

    private void RestChildNodeStatus () {
        var childCount = _childNodes.Count;
        for (int i = 0; i < childCount; i++) {
            _childNodesStatus[i] = BTRunningStatus.Executing;
        }
    }
}