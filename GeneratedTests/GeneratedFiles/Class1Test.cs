using System;
using NUnit.Framework;
using MainPart.Files;
using Moq;
using System.Collections.Generic;
[TestFixture]
class Class1Test
{
    private Class1 _class1;
    [SetUp]
    public void SetUp()
    {
        var str = default(string);
        _class1 = new Class1(str);
    }

    [Test]
    public void kj()
    {
        var actual = _class1.kj();
        var expected = default(int);
        Assert.That(actual, Is.EqualTo(expected));
        Assert.Fail("autogenerated");
    }
}