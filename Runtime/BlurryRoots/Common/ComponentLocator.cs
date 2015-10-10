using UnityEngine;
using System.Collections;

/// <summary>
/// Helper class used to locate any type of component.
/// </summary>
/// <typeparam name="TComponentType">Type of component to locate.</typeparam>
public sealed class ComponentLocator<TComponentType> : MonoBehaviour 
where TComponentType : MonoBehaviour {

	/// <summary>
	/// Locates exactly one component.
	/// </summary>
	/// <exception cref="UnityException">Raised if more than one component of type TComponent exists.</exception>
	/// <returns>The located component.</returns>
	public static TComponentType LocateSingle () {
		var components = ComponentLocator<TComponentType>.LocateAll ();
		if (1 < components.Length) {
			throw new UnityException ("More than one " + typeof (TComponentType) + " registered!");
		}

		return components[0];
	}

	/// <summary>
	/// Locates all components of given type.
	/// </summary>
	/// <returns>Array of all found components.</returns>
	public static TComponentType[] LocateAll () {
		var components = FindObjectsOfType<TComponentType> ();

		if (null != components && 0 == components.Length) {
			throw new UnityException ("No components of type " + typeof (TComponentType) + " registered!");
		}

		return components;
	}

}
