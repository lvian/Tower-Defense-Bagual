public class  FlameTowerFactory : TowerFactory {
	#region implemented abstract members of TowerFactory
	public override Tower GetTower ()
	{
		return new FlameTower();
	}
	#endregion
}
