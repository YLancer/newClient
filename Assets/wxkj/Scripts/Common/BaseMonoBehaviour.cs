using UnityEngine;
using System.Collections;

public class BaseMonoBehaviour : MonoBehaviour
{
	public new Transform transform
	{
		get
		{
			if(_transform == null)
			{
				_transform = base.transform;
			}

			return _transform;
		}
	}
	private Transform _transform;


	public new GameObject gameObject
	{
		get
		{
			if(_gameObject == null)
			{
				_gameObject = base.gameObject;
			}

			return _gameObject;
		}
	}
	private GameObject _gameObject;

	public string tag
	{
		get
		{
			if(_tag == null)
			{
				_tag = base.tag;
			}

			return _tag;
		}
	}
	private string _tag;
}
