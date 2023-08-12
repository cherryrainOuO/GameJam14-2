using System;

public class Kyun_BehaviourInfo
{
}

public interface Kyun_IBehaviourInfo
{
    Kyun_BehaviourInfoType BehaviourInfoType { get; }
    Type ObjectType { get; }
    object Object { get; }
}

public enum Kyun_BehaviourInfoType { Attack,  }