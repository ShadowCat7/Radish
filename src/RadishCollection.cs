using System.Collections.Generic;

namespace Radish
{
	public class RadishCollection
	{
		public RadishCollection()
		{
			m_interfaces = new List<IRadishInterface>();
		}

		public RadishInterface<TInterface> For<TInterface>(string name = null)
		{
			var addInterface = new RadishInterface<TInterface>(name);
			m_interfaces.Add(addInterface);
			return addInterface;
		}

		public List<IRadishInterface> Interfaces { get { return m_interfaces; } }

		readonly List<IRadishInterface> m_interfaces;
	}
}
