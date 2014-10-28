using System;
using Radish;

namespace RadishTests
{
	public static class RadishTests
	{
		static void Assert(bool assertion)
		{
			if (!assertion)
				throw new Exception();
		}

		public static void GetsConstructedInstanceForSimpleInterface()
		{
			SimpleObj1 constructedObj = new SimpleObj1();

			RadishContainer container = new RadishContainer(r =>
			{
				r.For<ISimpleObj1>().Use(constructedObj);
			});

			container.SetUp();

			ISimpleObj1 instance = container.GetInstance<ISimpleObj1>();

			Assert(instance != null);
			Assert(constructedObj == container.GetInstance<ISimpleObj1>());
		}

		public static void GetsCreatedInstanceForSimpleInterface()
		{
			RadishContainer container = new RadishContainer(r =>
			{
				r.For<ISimpleObj1>().Use<SimpleObj1>();
			});

			container.SetUp();

			ISimpleObj1 constructedObj = new SimpleObj1();
			ISimpleObj1 instance = container.GetInstance<ISimpleObj1>();

			Assert(instance != null);
			Assert(instance != constructedObj);
		}

		public static void GetsCreatedInstanceForComplexInterface()
		{
			RadishContainer container = new RadishContainer(r =>
			{
				r.For<ISimpleObj1>().Use<SimpleObj1>();
				r.For<ISimpleObj2>().Use<SimpleObj2>();
				r.For<IComplexObj1>().Use<ComplexObj1>();
			});

			container.SetUp();

			IComplexObj1 instance = container.GetInstance<IComplexObj1>();

			Assert(instance != null);
			Assert(instance.Obj1 != null);
			Assert(instance.Obj2 != null);
		}

		public static void RunAllTests()
		{
			GetsConstructedInstanceForSimpleInterface();
			GetsCreatedInstanceForSimpleInterface();
		}
	}

	public interface ISimpleObj1
	{
	}

	public class SimpleObj1 : ISimpleObj1
	{
		public SimpleObj1()
		{
		}
	}

	public interface ISimpleObj2
	{
	}

	public class SimpleObj2 : ISimpleObj2
	{
		public SimpleObj2()
		{
		}
	}

	public interface IComplexObj1
	{
		ISimpleObj1 Obj1 { get; }
		ISimpleObj2 Obj2 { get; }
	}

	public class ComplexObj1 : IComplexObj1
	{
		public ComplexObj1(ISimpleObj1 obj1, ISimpleObj2 obj2)
		{
			Obj1 = obj1;
			Obj2 = obj2;
		}

		public ISimpleObj1 Obj1 { get; private set; }
		public ISimpleObj2 Obj2 { get; private set; }
	}
}
