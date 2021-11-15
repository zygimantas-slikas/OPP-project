using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client
{
    public class GameSettings
    {
        private static GameSettings instance = null;
        public int Player_direction { get; set; }
        public Key Move_up_key     ;
        public Key Move_down_key   ;
        public Key Move_left_key   ;
        public Key Move_right_key  ;
        public Key Take_item_key   ;
        public Key Drop_item_key   ;
        public Key Switch_left_key ;
        public Key Switch_right_key;
        public Key Use_item_key    ;
        public Key Put_bomb_key    ;

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
        public GameSettings()
        {
            this.Player_direction = 0;
            this.PlayerDelaySpeed = 100;
            Delay = Task.Delay(this.PlayerDelaySpeed);
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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
