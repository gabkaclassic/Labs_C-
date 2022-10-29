namespace Lab_2;

public interface IRateAndCopy
{
    double Rating { get;  }

    object DeepCopy();
}