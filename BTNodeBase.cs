using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTNodeBase : IBTNodeInterface {
    protected List<BTNodeBase> _childNodes;

    public BTNodeBase () {
        _childNodes = new List<BTNodeBase> ();
    }

    public virtual void Enter () {
        throw new System.NotImplementedException ();
    }

    public virtual void Execute () {
        throw new System.NotImplementedException ();
    }

    public virtual void Exit () {
        throw new System.NotImplementedException ();
    }

    public virtual bool PreCondition () {
        return false;
    }
}