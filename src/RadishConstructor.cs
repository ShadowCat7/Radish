using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Radish
{
	public class RadishConstructor
	{
		public RadishConstructor(ConstructorInfo constructor)
		{
			m_constructor = constructor;
			m_parameters = new List<IRadishImplementation>();
		}

		public void AddParameter(IRadishImplementation parameter)
		{
			m_parameters.Add(parameter);
		}

		public Func<object> GetInvoke()
		{
			return () => m_constructor.Invoke(m_parameters.Select(p => p.GetInstanceAsObject()).ToArray());
		}

		public int ParametersCount { get { return m_parameters.Count; } }

		readonly ConstructorInfo m_constructor;
		readonly List<IRadishImplementation> m_parameters;
	}
}
