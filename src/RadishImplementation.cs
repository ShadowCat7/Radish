using System;

namespace Radish
{
	public class RadishImplementation<TImplementation> : IGenericRadishImplementation<TImplementation>
	{
		public RadishImplementation(RadishScope scope)
		{
			m_scope = scope;
			m_getInstanceFunc = null;
			m_isInitialized = false;
		}

		public RadishImplementation(RadishScope scope, TImplementation implementation)
		{
			m_scope = scope;
			m_getInstanceFunc = () => implementation;
			m_isInitialized = true;
		}

		public RadishImplementation(RadishScope scope, Func<TImplementation> implementationFunc)
		{
			m_scope = scope;
			m_getInstanceFunc = implementationFunc;
			m_isInitialized = true;
		}

		public Type GetImplementationType()
		{
			return typeof(TImplementation);
		}

		public bool ShouldSetFuncGetInstance()
		{
			return m_getInstanceFunc == null;
		}

		public void SetFuncGetInstance(Func<object> getInstanceAsObject)
		{
			if (m_getInstanceFunc == null)
				m_getInstanceFunc = () => (TImplementation) getInstanceAsObject();
		}

		public object GetInstanceAsObject()
		{
			return GetInstance();
		}

		public TImplementation GetInstance()
		{
			TImplementation implementation = m_getInstanceFunc();

			if (m_scope == RadishScope.Singleton && !m_isInitialized)
			{
				m_getInstanceFunc = () => implementation;
				m_isInitialized = true;
			}

			return implementation;
		}

		public RadishScope Scope { get { return m_scope; } }

		readonly RadishScope m_scope;
		Func<TImplementation> m_getInstanceFunc;
		bool m_isInitialized;
	}
}
