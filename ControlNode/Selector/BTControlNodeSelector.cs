/// <summary>
/// class:   不带优先级的选择节点
/// Check:   先调用上一个运行的子节点（若存在）的Check方法，如果可以运行，则继续运保存该节点的索引，返回True，如果不能运行，则重新选择（同带优先级的选择节点的选择方式）
/// Tick:    调用可以运行的子节点的Tick方法，用它所返回的运行状态作为自身的运行状态返回
/// </summary>

public class BTControlNodeSelector : BTControlNodeBase {
    protected int _currentSelectIndex;
    protected int _lastSelectIndex;

    protected override bool DoCheck (BTInput input) {
        base.DoCheck (input);

        BTNodeBase node = _childNodes[_index];
        if (node.Check (input)) return true;

        return base.DoCheck (input);
    }

    protected override void DoTransition (BTInput input) {
        base.DoTransition (input);
        var node = _childNodes[_currentSelectIndex];
        node.Transition (input);
        _lastSelectIndex = -1;
    }

    protected override BTRunningStatus DoTick (BTInput input, ref BTOutput output) {
        base.DoTick (input, ref output);

        var runningStatus = BTRunningStatus.Finish;
        if (_lastSelectIndex != -1 && _lastSelectIndex != _currentSelectIndex) {
            var node = _childNodes[_lastSelectIndex];
            node.Transition (input);
            _lastSelectIndex = _currentSelectIndex;
        }

        if (_lastSelectIndex != -1) {
            var node = _childNodes[_lastSelectIndex];
            runningStatus = node.Tick (input, ref output);
            if (runningStatus == BTRunningStatus.Finish) {
                _lastSelectIndex = -1;
            }
        }

        return runningStatus;
    }
}