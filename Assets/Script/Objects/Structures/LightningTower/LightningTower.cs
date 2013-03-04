public class LightningTower : Tower {
	string 	_name 	= "Lightning Tower";
	string	_description = "Throws lighning bolt that hit up to 3 invading units.";
	string	_prefab = "Prefabs/LightningTower";
	float	_damage	= 6f;
	float	_reach	= 3f;
	int		_price	= 30;
	int		_jumps	= 2;
	float	_attackSpeed = 2.0f;
	
	#region implemented abstract members of Tower
	public override string Name {
		get {
			return _name;
		}
	}
	
	public override string Description {
		get {
			return _description;
		}
	}

	public override string Prefab {
		get {
			return _prefab;
		}
	}

	public override float damage {
		get {
			return _damage;
		}
	}

	public override float reach {
		get {
			return _reach;
		}
	}
	
	public override float attackSpeed {
		get {
			return _attackSpeed;
		}
	}

	public override int price {
		get {
			return _price;
		}
	}

	public override int jumps {
		get {
			return _jumps;
		}
	}
	#endregion
}
