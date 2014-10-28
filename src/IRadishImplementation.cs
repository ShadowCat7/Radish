using System;

namespace Radish
{
	public interface IRadishImplementation
	{
		Type GetImplementationType();
		bool ShouldSetFuncGetInstance();
		void SetFuncGetInstance(Func<object> getInstanceAsObject);
		object GetInstanceAsObject();
		RadishScope Scope { get; }
	}
}
