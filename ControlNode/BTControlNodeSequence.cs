using System.Runtime.InteropServices;
/// <summary>
/// class:   序列节点
/// Check:   若是从头开始的，则调用第一个子节点的Check方法，将其返回值作为自身的返回值返回。否则，调用当前运行节点的Evaluate方法，将其返回值作为自身的返回值返回。
/// Tick:    调用可以运行的子节点的Tick方法，若返回运行结束，则将下一个子节点作为当前运行节点，若当前已是最后一个子节点，表示该序列已经运行结束，则自身返回运行结束。
///          若子节点返回运行中，则用它所返回的运行状态作为自身的运行状态返回
/// </summary>

public class BTControlNodeSequence : BTControlNodeBase {
    protected int _currentNodeIndex;

    protected override bool DoCheck (BTInput input) {
        base.DoCheck (input);

        int testNode = _currentNodeIndex == -1 ? 0 : _currentNodeIndex;

        var node = _childNodes[testNode];
        if (node.Check (input)) return true;

        return false;
    }

    protected override void DoTransition (BTInput input) {
        base.DoTransition (input);

        if (_currentNodeIndex != -1) {
            var node = _childNodes[_currentNodeIndex];
            node.Transition (input);
        }

        _currentNodeIndex = -1;
    }

    protected override BTRunningStatus DoTick(BTInput input, ref BTOutput output){
        base.DoTick(input, ref output);
        var runningStatus = BTRunningStatus.Finish;

        if(_currentNodeIndex == -1) _currentNodeIndex = 0;

        var node = _childNodes[_currentNodeIndex];
        runningStatus = node.Tick(input, ref output);
        if(runningStatus == BTRunningStatus.Finish){
            _currentNodeIndex++;
            if(_currentNodeIndex == _childNodes.Count){
                _currentNodeIndex = -1;
            }else{
                runningStatus = BTRunningStatus.Executing;
            }
        }else if(runningStatus == BTRunningStatus.Error){
            _currentNodeIndex = -1;
        }

        return runningStatus;
    }
}