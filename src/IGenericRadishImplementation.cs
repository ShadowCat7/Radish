namespace Radish
{
	public interface IGenericRadishImplementation<out TImplementation> : IRadishImplementation
	{
		TImplementation GetInstance();
	}
}
