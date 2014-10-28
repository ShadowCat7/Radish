namespace Radish
{
	public interface IRadishInterface
	{
		RadishType Type { get; }
		IRadishImplementation Implementation { get; }
	}
}
