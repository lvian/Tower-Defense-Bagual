public class OilSquallTower : Tower {
	string 	_name 	= "Oil Squall";
	string  _description = "Slow enemies pouring oil on the track.";
	string	_prefab = "Prefabs/OilSquall";
	float	_damage	= 0f;
	float	_reach	= 3.5f;
	int		_price	= 30;
	int		_jumps	= 2;
	float	_attackSpeed	= 0.5f;
	
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
