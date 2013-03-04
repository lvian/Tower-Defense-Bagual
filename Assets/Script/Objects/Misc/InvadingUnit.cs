public class InvadingUnit {
	
	private IDefendBehavior _defendBehavior;
	private IBotBehavior _botBehavior;
	private IBotStats _botStat;
	private IMoveBehavior _moveBehavior;
	
	public InvadingUnit(IDefendBehavior behavior, IBotStats stats, IBotBehavior botBehavior, IMoveBehavior moveBehavior){
		this._defendBehavior = behavior;
		this._botBehavior = botBehavior;
		this._botStat = stats;
		this._moveBehavior = moveBehavior;
	}
	
	public IDefendBehavior DefendBehavior {
		get {
			return this._defendBehavior;
		}
		set {
			_defendBehavior = value;
		}
	}

	public IBotBehavior BotBehavior {
		get {
			return this._botBehavior;
		}
		set {
			_botBehavior = value;
		}
	}
	
	public IBotStats Stat {
		get {
			return this._botStat;
		}
		set {
			_botStat = value;
		}
	}

	public IMoveBehavior MoveBehavior {
		get {
			return this._moveBehavior;
		}
		set {
			_moveBehavior = value;
		}
	}
}
