using System;
using System.Drawing;
using UnrealEngine.Framework;

namespace PKSIW {
	public static class Main {
		private static ISystem runningSystem;
		private static Rewind r;

		public static void OnWorldPostBegin() {
			Debug.Log(LogLevel.Display, "Hello, from Prince of Persia System!"); 
			r = new Rewind();
			


		}

		//public static void OnWorldPrePhysicsTick(float deltaTime) => runningSystem.OnTick(deltaTime);

		public static void OnWorldEnd() {
			r.OnEndPlay();

			Debug.Log(LogLevel.Display, "Prince of Persia System Shutdown!");
		}

		
	}

	public interface ISystem {
		void OnBeginPlay() { }
		void OnTick(float deltaTime) { }
		void OnEndPlay() { }
	}
}