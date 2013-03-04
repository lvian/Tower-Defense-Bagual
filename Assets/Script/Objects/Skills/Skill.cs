using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour{
	private string 		_name;
	private string		_description;
	private GameObject	_user;
	private SkillType	_type;
	
	#region Getters and Setters
	public string Description {
		get {
			return this._description;
		}
		set {
			_description = value;
		}
	}

	public string Name {
		get {
			return this._name;
		}
		set {
			_name = value;
		}
	}

	public UnityEngine.GameObject User {
		get {
			return this._user;
		}
		set {
			_user = value;
		}
	}

	public SkillType Type {
		get {
			return this._type;
		}
		set {
			_type = value;
		}
	}
	#endregion
}

public enum SkillType {
	Passive,
	Instant,
	Active
}
