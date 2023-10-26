namespace LiteValidation;

public interface ILiteValidatorBuilderForType<T>
{
    void Check(T value);
}
