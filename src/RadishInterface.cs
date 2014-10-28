namespace Radish
{
	public class RadishInterface<TInterface> : IRadishInterface
	{
		public RadishInterface(string name)
		{
			m_type = RadishType.Create<TInterface>(name);
		}

		public void Use<TImplementation>(RadishScope scope = RadishScope.None)
			where TImplementation : TInterface
		{
			m_implementation = new RadishImplementation<TImplementation>(scope);
		}

		public void Use(TInterface implementation, RadishScope scope = RadishScope.None)
		{
			m_implementation = new RadishImplementation<TInterface>(scope, implementation);
		}

		public RadishType Type { get { return m_type; } }
		public IRadishImplementation Implementation { get { return m_implementation; } }

		readonly RadishType m_type;
		IRadishImplementation m_implementation;
	}
}
