﻿using System;
using System.Collections.Generic;

namespace Escape
{
	class Program
	{
		#region Declarations
		private static int Width = 100;
		private static int Height = 30;
		
		public enum GameStates { Start, Playing, Battle, Quit, GameOver };
		public static GameStates GameState = GameStates.Start;
		private static bool run = true;
		
		private static bool isError = false;
		private static List<string> errors = new List<string>();
		
		private static bool isNotification = false;
		private static List<string> notifications = new List<string>();
		#endregion
		
		public static void Main(string[] args)
		{
			Console.WindowWidth = Width;
			Console.WindowHeight = Height;

			World.Initialize();
			
			while(run)
			{
				if (!isError)
				{
					switch (GameState)
					{
						case GameStates.Start:
							Text.WriteLine("Hello adventurer! What is your name?");
							Player.Name = Text.SetPrompt("> ");
							Text.Clear();
							GameState = GameStates.Playing;
							break;
							
						case GameStates.Playing:
							if (isNotification)
							{
								DisplayNotification();
							}
							
							World.LocationDescription();
							World.LocationExits();
							World.LocationItems();
							
							string temp = Text.SetPrompt("[" + World.Map[Player.Location].Name + "] > ");
							Text.Clear();
							Player.Do(temp);
							break;
						
						case GameStates.Battle:
							break;
							
						case GameStates.Quit:
							Console.Clear();
							Text.WriteLine("Are you sure you want to quit? (y/n)");
							
							ConsoleKeyInfo quitKey = Console.ReadKey();
							if (quitKey.KeyChar == 'y')
							{
								run = false;
							}
							else
							{
								Text.Clear();
								GameState = GameStates.Playing;
							}
							break;
							
						case GameStates.GameOver:
							break;
					}
				}
				else
				{
					DisplayError();
				}
			}
		}
		
		#region Notification Handling
		private static void DisplayNotification()
		{
			foreach (string notification in notifications)
			{
				Text.WriteColor("`g`Alert: `w`" + notification);
			}
			
			Text.BlankLines(2);
			UnsetNotification();
		}
		
		public static void SetNotification(string message)
		{
			notifications.Add(message);
			isNotification = true;
		}
		
		private static void UnsetNotification()
		{
			notifications.Clear();
			isNotification = false;
		}
		#endregion
		
		#region Error Handling
		private static void DisplayError()
		{
			foreach (string error in errors)
			{
				Text.WriteColor("`r`Error: `w`" + error);
			}
			
			Text.BlankLines(2);
			UnsetError();
		}
		
		public static void SetError(string message)
		{
			errors.Add(message);
			isError = true;
		}
		
		private static void UnsetError()
		{
			errors.Clear();
			isError = false;
		}
		#endregion
	}
}