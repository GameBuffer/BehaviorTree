public class BehaviorTree {
    private BTNodeBase _root;

    public BehaviorTree () { }

    public void AddNodes (params BTNodeBase[] nodes) {
        _root = nodes[0];
        if (nodes.Length > 1) {
            for (int i = 1; i < nodes.Length; i++) {
                _root.AddChild (nodes[i]);
            }
        }
    }

    public void Tick (BTInput input, ref BTOutput output) {
        if(_root.Check(input)){
            _root.Tick (input, ref output);
        }
    }
}