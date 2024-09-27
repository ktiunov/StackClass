namespace StackClass;

internal class BaseClass
{
    private readonly char _1 = 'H';
    private readonly char _2 = 'e';
    private readonly char _3 = 'a';
    private readonly char _4 = 'p';
    private readonly char _5 = '!';

    protected string Source => $"{_1}{_2}{_3}{_4}{_5}";

    public virtual void Test()
    {
        Console.WriteLine($"Base class. Hello from {Source}");
    }
}

internal class DerivedClass : BaseClass
{
    public override void Test()
    {
        Console.WriteLine($"Child class. Hello from {Source}");
    }
}

internal class TestClassContainer
{
    private readonly BaseClass _class;

    public TestClassContainer(BaseClass @class)
    {
        _class = @class;
    }

    public BaseClass Reference => _class;
}
