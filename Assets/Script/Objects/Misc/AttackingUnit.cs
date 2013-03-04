public class AttackingUnit{
	private IStructureBehavior 	_structureBehavior;
	private TowerFactory 		_towerFactory;
	
	public AttackingUnit(IStructureBehavior behavior, TowerFactory factory){
		_structureBehavior = behavior;
		_towerFactory = factory;
	}

	public IStructureBehavior StructureBehavior {
		get {
			return this._structureBehavior;
		}
		set {
			_structureBehavior = value;
		}
	}

	public TowerFactory TowerFactory {
		get {
			return this._towerFactory;
		}
		set {
			_towerFactory = value;
		}
	}
}
