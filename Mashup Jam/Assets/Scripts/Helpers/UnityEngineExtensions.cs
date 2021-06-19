using UnityEngine;
 
static public class UnityEngineExtensions
{
	/// <summary>
	/// Returns the component of Type type. If one doesn't already exist on the GameObject it will be added.
	/// </summary>
	/// <typeparam name="T">The type of Component to return.</typeparam>
	/// <param name="gameObject">The GameObject this Component is attached to.</param>
	/// <returns>Component</returns>
	static public T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
	{
		return gameObject.GetComponent<T>() ?? gameObject.AddComponent<T>();
	}
	
	/// <summary>
	/// clamps a vector2 by each value
	/// </summary>
	/// <param name="original">vector2 to clamp</param>
	/// <param name="min">min values for x and y</param>
	/// <param name="max">max value for x and y</param>
	/// <returns>clamped original</returns>
	public static Vector2 Clamp (this Vector2 original, Vector2 min, Vector2 max)
	{
		return new Vector2(Mathf.Clamp(original.x, min.x, max.x), Mathf.Clamp(original.y, min.y, max.y));
	}
}