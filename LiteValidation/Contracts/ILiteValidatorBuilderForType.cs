namespace LiteValidation.Contracts;

public interface ILiteValidatorBuilderForType<T>
{
    void Check(T value);
}
