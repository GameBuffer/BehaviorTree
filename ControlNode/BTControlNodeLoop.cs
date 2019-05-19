/// <summary>
/// class:   循环节点
/// Check:   预设的循环次数到了就返回False，否则，只调用第一个子节点的Check方法，用它所返回的值作为自身的值返回
/// Tick:    只调用第一个节点的Tick方法，若返回运行结束，则看是否需要重复运行，若循环次数没到，则自身返回运行中，若循环次数已到，则返回运行结束
/// </summary>

public class BTControlNodeLoop : BTControlNodeBase {
    private int _loopCount;
    private int _currentCount;

    public BTControlNodeLoop (BTNodeBase parentNode, BTPreCondition preCondition = null, int loopCount = -1) : base (parentNode, preCondition) {
        _loopCount = loopCount;
        _currentCount = 0;
    }

    public int LoopCount { get => _loopCount;}

    protected override bool DoCheck (BTInput input) {
        base.DoCheck (input);

        var checkLoopCount = _loopCount == -1 || _currentCount < _loopCount;
        if (checkLoopCount) return false;

        var node = _childNodes[0];
        if (node.Check (input)) return true;

        return false;
    }

    protected override void DoTransition(BTInput input){
        base.DoTransition(input);
        
        var node = _childNodes[0];
        node.Transition(input);
        _currentCount = 0;
    }

    protected override BTRunningStatus DoTick(BTInput input, ref BTOutput output){
        var runningStatus = base.DoTick(input, ref output);

        var node = _childNodes[0];
        runningStatus = node.Tick(input, ref output);

        if(runningStatus == BTRunningStatus.Finish){
            if(_loopCount != -1){
                _currentCount++;
                if(_currentCount == _loopCount){
                    runningStatus = BTRunningStatus.Executing;
                }
            }else{
                runningStatus = BTRunningStatus.Executing;
            }
        }

        return runningStatus;
    }
}