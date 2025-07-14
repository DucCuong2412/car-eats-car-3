public interface ICarControlls
{
	void Accelerate(float direction, float multipler = 1f);

	void Rotate(float direction);

	void Turbo(bool Use);

	void Jump(bool USe);

	void Fire();
}
