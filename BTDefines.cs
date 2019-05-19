public enum BTRunningStatus {
    Error = 0,
    Executing = 1,
    Finish = 2,
}

public enum BTNodeStatus {
    Ready = 1,
    Running = 2,
    Finish = 3,
}

public enum BTParallelFinishCondition {
    OR = 1,
    AND = 2,
}