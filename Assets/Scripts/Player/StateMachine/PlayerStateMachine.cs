using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
	public PlayerState CurrentState { get; private set; }
	private PhysicsMovement _player;
	private Dictionary<Type, PlayerState> _playerStates;

	public PlayerStateMachine(PhysicsMovement player)
	{
		_player = player;
		_playerStates = new Dictionary<Type, PlayerState>()
		{
			{ typeof(Sliding), new Sliding(_player, this) },
			{ typeof(Invulnerable), new Invulnerable(_player, this) }
		};
	}

	public void Initialize<TState>() where TState : PlayerState
	{
		if(_playerStates.TryGetValue(typeof(TState), out PlayerState value))
		{
			CurrentState = value;
			CurrentState.Enter();
		}
	}

	public void ChangeState<TState>() where TState : PlayerState
	{
		if (_playerStates.TryGetValue(typeof(TState), out PlayerState value))
		{
			CurrentState.Exit();
			CurrentState = value;
			CurrentState.Enter();
		}
	}
}
