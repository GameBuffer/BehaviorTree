using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTControlNodeBase : BTNodeBase {
    public BTControlNodeBase () : base () { }

    public BTControlNodeBase (BTNodeBase parentNode, BTPreCondition preCondition = null) : base (parentNode, preCondition) { }
}