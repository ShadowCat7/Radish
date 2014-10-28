using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Radish
{
	public class RadishContainer
	{
		public RadishContainer(RadishCollection collection)
		{
			m_radishes = ConvertRadishes(collection);
		}

		public RadishContainer(Action<RadishCollection> collectionFunc)
		{
			RadishCollection collection = new RadishCollection();
			collectionFunc(collection);
			m_radishes = ConvertRadishes(collection);
		}

		private static Dictionary<Type, Dictionary<string, IRadishImplementation>> ConvertRadishes(RadishCollection collection)
		{
			return collection.Interfaces
				.GroupBy(r => r.Type.Type)
				.ToDictionary(radishGroup => radishGroup.Key,
					radishGroup => radishGroup.ToDictionary(radishInterface => radishInterface.Type.Name ?? string.Empty,
						radishInterface => radishInterface.Implementation));
		}

		public void SetUp()
		{
			foreach (IRadishImplementation implementation in m_radishes.Values.SelectMany(i => i.Values).Where(i => i.ShouldSetFuncGetInstance()))
			{
				RadishConstructor defaultConstructor = null;
				RadishConstructor bestConstructor = null;

				foreach (ConstructorInfo constructor in implementation.GetImplementationType().GetConstructors())
				{
					ParameterInfo[] parameters = constructor.GetParameters();
					RadishConstructor radishConstructor = new RadishConstructor(constructor);

					if (parameters.Length == 0)
					{
						defaultConstructor = radishConstructor;
					}
					else
					{
						bool haveAllParameters = true;

						for (int i = 0; haveAllParameters && i < parameters.Length; i++)
						{
							Dictionary<string, IRadishImplementation> radishImplementations;
							haveAllParameters = m_radishes.TryGetValue(parameters[i].ParameterType, out radishImplementations);

							if (haveAllParameters)
								radishConstructor.AddParameter(radishImplementations.Last().Value);
						}

						if (haveAllParameters && (bestConstructor == null || radishConstructor.ParametersCount < bestConstructor.ParametersCount))
							bestConstructor = radishConstructor;
					}
				}

				if (bestConstructor == null && defaultConstructor == null)
					throw new Exception();

				implementation.SetFuncGetInstance((bestConstructor ?? defaultConstructor).GetInvoke());
			}
		}

		public TInterface GetInstance<TInterface>(string name = null)
		{
			RadishType type = RadishType.Create<TInterface>(name);

			Dictionary<string, IRadishImplementation> implementations;
			if (!m_radishes.TryGetValue(type.Type, out implementations))
				throw new Exception();

			IRadishImplementation implementation;

			if (name == null)
				implementation = implementations.Last().Value;
			else if (!implementations.TryGetValue(name, out implementation))
				throw new Exception();

			return (implementation as IGenericRadishImplementation<TInterface>).GetInstance();
		}

		public IEnumerable<TInterface> GetAllInstances<TInterface>()
		{
			Type type = typeof(TInterface);

			Dictionary<string, IRadishImplementation> implementations;
			if (!m_radishes.TryGetValue(type, out implementations))
				throw new Exception();

			return implementations.Values
				.Cast<IGenericRadishImplementation<TInterface>>()
				.Select(r => r.GetInstance());
		}

		readonly Dictionary<Type, Dictionary<string, IRadishImplementation>> m_radishes;
	}
}
