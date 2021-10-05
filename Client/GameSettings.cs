﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class GameSettings
    {
        private static GameSettings instance = null;
        public int PlayerDelaySpeed;
        public Task Delay;
        public void moved()
        {
            Delay = Task.Delay(this.PlayerDelaySpeed);
        }

        private GameSettings()
        {
            PlayerDelaySpeed = 100;
            Delay = Task.Delay(this.PlayerDelaySpeed);
        }
        public static GameSettings Instance
        {
            get
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
