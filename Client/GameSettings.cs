﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class GameSettings
    {
        private static GameSettings instance = null;
        public int Player_direction { get; set; }
        public int PlayerDelaySpeed;
        public Task Delay;
        public void moved()
        {
            Delay = Task.Delay(this.PlayerDelaySpeed);
        }
        public void SetSpeed(int newSpeed)
        {
            this.PlayerDelaySpeed = newSpeed;
        }
        private GameSettings()
        {
            this.Player_direction = 0;
            this.PlayerDelaySpeed = 100;
            Delay = Task.Delay(this.PlayerDelaySpeed);
        }
        public static GameSettings GetInstance()
        {
            {
                if (instance == null)
                {
                    instance = new GameSettings();
                }
                return instance;
            }
        }
    }
}
