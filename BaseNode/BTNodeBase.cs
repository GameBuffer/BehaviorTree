using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BTNodeBase {
    private string _name;

    protected BTNodeBase _parentNode;
    protected BTPreCondition _preCondition;
    protected List<BTNodeBase> _childNodes;
    protected int _index;

    protected BTNodeBase _lastActiveNode;
    protected BTNodeBase _activeNode;

    protected bool _isActiveNode;

    public BTNodeBase () {
        _childNodes = new List<BTNodeBase> ();
        _parentNode = null;
        _isActiveNode = false;
    }

    protected BTNodeBase (BTNodeBase parentNode, BTPreCondition preCondition = null) {
        _parentNode = parentNode;
        _preCondition = preCondition;
        _isActiveNode = false;
    }

    protected BTPreCondition PreCondition {
        get { return _preCondition; }
    }

    public string Name { get => _name; set => _name = value; }

    public BTRunningStatus Tick (BTInput input, ref BTOutput output) {
        return DoTick (input, ref output);
    }

    public bool Check (BTInput input) {
        return (_preCondition == null || _preCondition.Check (input)) && DoCheck (input);
    }

    public void Transition (BTInput _input) {
        DoTransition (_input);
    }

    public void AddChild (BTNodeBase childNode) {
        _childNodes.Add (childNode);
    }

    public void RemoveChild (BTNodeBase childNode) {
        int count = _childNodes.Count;
        for (int i = 0; i < count; i++) {
            if (_childNodes[i].Equals (childNode)) {
                _childNodes.RemoveAt (i);
                break;
            }
        }
    }

    public void SetActiveNode (BTNodeBase node) {
        _lastActiveNode = _activeNode;
        _activeNode = node;
        if (_parentNode != null) _parentNode.SetActiveNode (node);
    }

    ////////// ---> viturals below

    protected virtual bool DoCheck (BTInput input) {
        return true;
    }

    protected virtual BTRunningStatus DoTick (BTInput input, ref BTOutput output) {
        Debug.LogError ("DoTick:" + _name);
        return BTRunningStatus.Finish;
    }

    protected virtual void DoTransition (BTInput input) { }
}