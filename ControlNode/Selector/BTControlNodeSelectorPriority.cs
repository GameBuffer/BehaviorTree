/// <summary>
/// class:   带优先级的选择节点
/// Check:   从第一个子节点开始依次遍历所有的子节点，调用其Check方法，当发现存在可以运行的子节点时，记录子节点索引，停止遍历，返回True。
/// Tick:    调用可以运行的子节点的Tick方法，用它所返回的运行状态作为自身的运行状态返回
/// </summary>

public class BTControlNodeSelectorPriority : BTControlNodeSelector {
    protected override bool DoCheck (BTInput input) {
        base.DoCheck (input);

        var childCount = _childNodes.Count;
        for (int i = 0; i < childCount; i++) {
            var node = _childNodes[i];
            if (node.Check (input)) {
                _currentSelectIndex = i;
                return true;
            }
        }

        return false;
    }
}