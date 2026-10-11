using System;
using UnityEngine;

public interface IDamageable {
    public event EventHandler<OnDamageTakenEventArgs> OnDamageTaken;
    public event EventHandler<EventArgs> OnDie;

    public class OnDamageTakenEventArgs : EventArgs {
        public int Health;
        public int Damage;
    }

    public void TakeDamage(int amount);
    public int GetHealth();
}