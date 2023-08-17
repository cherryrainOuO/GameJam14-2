using System;

public class BehaviourInfo
{
}

public interface IBehaviourInfo
{
    BehaviourInfoType BehaviourInfoType { get; }
    Type ObjectType { get; }
    object Object { get; }
}

public enum BehaviourInfoType { Attack,  }