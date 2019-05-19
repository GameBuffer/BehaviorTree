public class BTActionNodeBase : BTNodeBase {
    private BTNodeStatus _status;
    private bool _needExit;

    public BTActionNodeBase () : base () {
        _status = BTNodeStatus.Ready;
        _isActiveNode = true;
        _needExit = false;
    }

    public BTActionNodeBase (BTNodeBase parentNode, BTPreCondition preCondition = null) : base (parentNode, preCondition) {
        _isActiveNode = true;
    }

    protected virtual void DoEnter (BTInput input) { }

    protected virtual BTRunningStatus DoExecute (BTInput _input, ref BTOutput _output) {
        return BTRunningStatus.Finish;
    }

    protected virtual void DoExit (BTInput _input, BTRunningStatus _status) { }

    protected override void DoTransition (BTInput _input) {
        base.DoTransition (_input);
        if (_needExit) {
            DoExit (_input, BTRunningStatus.Error);
        }

        SetActiveNode (null);
        _status = BTNodeStatus.Ready;
        _needExit = false;
    }

    protected override BTRunningStatus DoTick (BTInput input, ref BTOutput output) {
        BTRunningStatus runningStatus = base.DoTick (input, ref output);

        switch (_status) {
            case BTNodeStatus.Ready:
                DoEnter (input);
                _needExit = true;
                _status = BTNodeStatus.Running;
                SetActiveNode (this);
                break;
            case BTNodeStatus.Running:
                runningStatus = DoExecute (input, ref output);
                SetActiveNode (this);
                if (runningStatus == BTRunningStatus.Finish || runningStatus == BTRunningStatus.Error) {
                    _status = BTNodeStatus.Finish;
                }
                break;
            case BTNodeStatus.Finish:
                if (_needExit) DoExit (input, runningStatus);
                _status = BTNodeStatus.Ready;
                _needExit = false;
                SetActiveNode (null);
                break;
        }

        return runningStatus;
    }
}