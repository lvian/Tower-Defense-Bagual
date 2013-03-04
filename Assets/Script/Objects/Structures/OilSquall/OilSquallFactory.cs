public class OilSquallFactory : TowerFactory {
	
	#region implemented abstract members of TowerFactory
	public override Tower GetTower ()
	{
		return new OilSquallTower();
	}
	#endregion
	
}
