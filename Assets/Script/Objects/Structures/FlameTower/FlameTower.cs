public class FlameTower : Tower {
	string 	_name 	= "Flame Tower";
	string  _description = "Melt your enemies and anything close to them.";
	string	_prefab = "Prefabs/FlameTower";
	float	_damage	= 4f;
	float	_reach	= 2f;
	int		_price	= 20;
	int		_jumps	= 0;
	float	_attackSpeed	= 0.25f;
	
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
