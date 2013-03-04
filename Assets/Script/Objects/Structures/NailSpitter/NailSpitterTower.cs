public class NailSpitterTower : Tower {
	string 	_name 	= "Nail Spitter";
	string  _description = "Throws a single nail at a time.";
	string	_prefab = "Prefabs/NailSpitter";
	float	_damage	= 8f;
	float	_reach	= 3f;
	int		_price	= 20;
	int		_jumps	= 0;
	float	_attackSpeed	= 1.5f;
	
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
	
	public override float attackSpeed {
		get {
			return _attackSpeed;
		}
	}

	#endregion
}
