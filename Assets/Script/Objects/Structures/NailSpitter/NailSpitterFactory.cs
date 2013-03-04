public class NailSpitterFactory : TowerFactory {
	#region implemented abstract members of TowerFactory
	public override Tower GetTower ()
	{
		return new NailSpitterTower();
	}
	#endregion
}
