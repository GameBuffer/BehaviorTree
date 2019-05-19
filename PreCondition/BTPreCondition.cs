public abstract class BTPreCondition {
    public abstract bool Check (BTInput input);
}

public class BTPreConditionTRUE : BTPreCondition {
    public override bool Check (BTInput input) {
        return true;
    }
}

public class BTPreConditionFALSE : BTPreCondition {
    public override bool Check (BTInput input) {
        return false;
    }
}