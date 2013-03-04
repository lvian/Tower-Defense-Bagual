public class LightningTowerFactory : TowerFactory {
	
	#region implemented abstract members of TowerFactory
	public override Tower GetTower ()
	{
		return new LightningTower();
	}
	#endregion
	
}
