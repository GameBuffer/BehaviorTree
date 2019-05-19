public class BTControlNodeParallel : BTControlNodeBase {
    public override void Enter () {
        throw new System.NotImplementedException ();
    }

    public override void Execute () {
        for (int i = 0; i < _childNodes.Count; i++) {
            if (_childNodes[i].PreCondition ()) {
                _childNodes[i].Enter ();
            }
        }
    }

    public override void Exit () {
        throw new System.NotImplementedException ();
    }

    public override bool PreCondition () {
        return false;
    }
}