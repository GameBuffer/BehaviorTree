public class BTControlNodeSelector : BTControlNodeBase {

    public void Enter () {
        throw new System.NotImplementedException ();
    }

    public override void Execute () {
        for (int i = 0; i < _childNodes.Count; i++) {
            if (_childNodes[i].PreCondition ()) {
                _childNodes[i].Enter ();
                return;
            }
        }
    }

    public void Exit () {
        throw new System.NotImplementedException ();
    }

    public bool PreCondition () {
        return false;
    }
}