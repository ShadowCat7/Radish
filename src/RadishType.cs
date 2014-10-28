using System;

namespace Radish
{
	public class RadishType
	{
		private RadishType(Type type, string name)
		{
			m_type = type;
			m_name = name;
		}

		public static RadishType Create<T>(string name = null)
		{
			return new RadishType(typeof(T), name);
		}

		public Type Type { get { return m_type; } }
		public string Name { get { return m_name; } }

		readonly Type m_type;
		readonly string m_name;
	}
}
