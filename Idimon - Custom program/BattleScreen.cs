using System;
using SplashKitSDK;

namespace Idimon
{
    public class BattleScreen : Screens
    {
        public bool InBattle { get; private set; }
        List<Idimons> _player;
        List<Idimons> _opponent;
        Inventory _inventory;
        Window _window;
        GameScreen _preGameScreen;
        private int _currentIdimonIndex, _selectedIndex;
        List<MenuItem> _actionButtons;
        private enum Action
        {
            Skills,
            Swich,
            Items,
            Run
        }
        private enum Turn
        {
            Player,
            Opponent
        }

        private Turn _currentTurn;
        private Action _currentAction;
        private string _type;

        private List<Items> _items;

        private int _selectedIdimonIndex;
        private int _selectedSkillIndex;
        private int _selectedItemIndex;

        public BattleScreen(Window window, List<Idimons> player, List<Idimons> opponents, Inventory inventory, GameScreen preGameScreen) : base(window)
        {
            InBattle = true;
            _window = window;
            _player = player;
            _opponent = opponents;
            _inventory = inventory;
            _preGameScreen = preGameScreen;
            _currentIdimonIndex = 0;
            _selectedIndex = 0;
            _currentAction = (Action)0;
            _type = "";
            _actionButtons = new List<MenuItem>
            {
                new MenuItem("Skills", 50, SplashKit.ScreenHeight() / 3 * 2 + 30),
                new MenuItem("Switch", 50, SplashKit.ScreenHeight() / 3 * 2 + 80),
                new MenuItem("Items", 50, SplashKit.ScreenHeight() / 3 * 2 + 130),
                new MenuItem("Run", 50, SplashKit.ScreenHeight() / 3 * 2 + 180)
            };
            _actionButtons[_selectedIndex].IsSelected = true;
            _currentTurn = DetermineFirstTurn();

            _selectedSkillIndex = 0;
            _selectedIdimonIndex = 0;
            _selectedItemIndex = 0;
        }

        public void ExitBattle()
        {
            Game.CurrentScreen = _preGameScreen;
            InBattle = false;
        }

        public void Select()
        {
            switch (_currentAction)
            {
                case Action.Skills:
                    // Skills logic
                    _type = "Skills";
                    break;
                case Action.Swich:
                    // Items logic
                    _type = "Switch";
                    break;
                case Action.Items:
                    // Equipment logic
                    _type = "Items";
                    _items = _inventory.GetAllItems("Items");
                    break;
                case Action.Run:
                    // Run logic
                    ExitBattle();
                    break;
            }
        }

        public override void Update()
        {
            // throw new NotImplementedException();
            // ExecuteTurn();
        }
        
        public override void Draw()
        {
            // Initialize the window
            _window.Clear(Color.Black);
            SplashKit.FillRectangle(Color.White, 0, SplashKit.ScreenHeight() / 3 * 2, SplashKit.ScreenWidth(), 2);
            SplashKit.FillRectangle(Color.White, SplashKit.ScreenWidth() / 4, SplashKit.ScreenHeight() / 3 * 2, 2, SplashKit.ScreenHeight());
            
            foreach (MenuItem button in _actionButtons)
            {
                button.Draw();
            }

            // Player
            _player[_currentIdimonIndex].Draw(SplashKit.ScreenWidth() / 2 - 300, 200);
            _player[_currentIdimonIndex].DrawHPBar(20, 20, SplashKit.ScreenWidth() / 3, 20);

            // Opponent
            Idimons opponent = _opponent[0];
            opponent.Draw(SplashKit.ScreenWidth() / 2 + 300, 200);
            opponent.DrawHPBar(SplashKit.ScreenWidth() - (SplashKit.ScreenWidth() / 3 + 20) , 20, SplashKit.ScreenWidth() / 3, 20);

            if(_type == "Skills")
            {
                // Draw skills
                double x = 300;
                double y = 555;
                int i = 0;

                foreach (var skill in _player[_currentIdimonIndex].Skills)
                {
                    if (i == _selectedSkillIndex)
                    {
                        SplashKit.FillRectangle(Color.RGBAColor(255, 255, 0, 150), x - 5, y - 5, 250, 30);
                    }

                    SplashKit.DrawText(skill.Name, Color.White, "Arial", 20, x, y);
                    SplashKit.DrawText($"Power: {skill.Damage}", Color.White, "Arial", 20, x + 150, y);

                    i++;
                    // y += SplashKit.ScreenHeight() / 6;
                    if(i % 2 == 0)
                    {
                        y += 50;
                        x = 300;
                    }
                    else
                    {
                        x += 300;
                    }
                }
            }

            if(_type == "Switch")
            {
                // Draw Idimons
                double x = 265;
                double y = 540;
                int i = 0;

                foreach (var idimon in _player)
                {    
                    if (i == _selectedIdimonIndex)
                    {
                        SplashKit.FillRectangle(Color.RGBAColor(255, 255, 0, 150), x - 5, y - 5, 260, 64);
                    }

                    // Details
                    SplashKit.DrawText(idimon.Name, Color.White, "Arial", 20, x, y);
                    SplashKit.DrawText($"Level: {idimon.Level}", Color.White, "Arial", 20, x + 150, y);
                    idimon.DrawHPBar(x, y + 34, 250, 10);

                    i++;
                    if(i % 2 == 0)
                    {
                        y += 70;
                        x -= 300;
                    }
                    else
                    {
                        x += 300;
                    }
                }
            }

            if(_type == "Items")
            {
                // Draw items

                _inventory.DisplayInventory(_window, 260, 525, 370, "Items");
            }

            _window.Refresh(60);
        }

        public void Navigate(KeyCode key)
        {
            if (_type == "Skills")
            {
                if (key == KeyCode.DownKey)
                {
                    _selectedSkillIndex = (_selectedSkillIndex + 2) % _actionButtons.Count;
                }
                else if (key == KeyCode.UpKey)
                {
                    _selectedSkillIndex = (_selectedSkillIndex - 2 + _actionButtons.Count) % _actionButtons.Count;
                }
                else if (key == KeyCode.RightKey)
                {
                    _selectedSkillIndex = (_selectedSkillIndex + 1) % _actionButtons.Count;
                }
                else if (key == KeyCode.LeftKey)
                {
                    _selectedSkillIndex = (_selectedSkillIndex - 1 + _actionButtons.Count) % _actionButtons.Count;
                }
                return;
            }
            if (_type == "Switch")
            {
                if (key == KeyCode.DownKey)
                {
                    if(_player.Count %2 != 0 && _selectedIdimonIndex == _player.Count - 1)
                    {
                        _selectedIdimonIndex = 0;
                        return;
                    }
                    _selectedIdimonIndex = (_selectedIdimonIndex + 2) % _player.Count;
                }
                else if (key == KeyCode.UpKey)
                {
                    if(_player.Count %2 != 0 && (_selectedIdimonIndex == 0 || _selectedIdimonIndex == 1))
                    {
                        _selectedIdimonIndex = _player.Count - 1;
                        return;
                    }
                    _selectedIdimonIndex = (_selectedIdimonIndex - 2 + _player.Count) % _player.Count;
                }
                else if (key == KeyCode.RightKey)
                {
                    _selectedIdimonIndex = (_selectedIdimonIndex + 1) % _player.Count;
                }
                else if (key == KeyCode.LeftKey)
                {
                    _selectedIdimonIndex = (_selectedIdimonIndex - 1 + _player.Count) % _player.Count;
                }
                return;
            }

            if (_type == "Items")
            {
                _inventory.Navigate("Items");
                return;
            }

            _actionButtons[_selectedIndex].IsSelected = false;

            if (key == KeyCode.DownKey)
            {
                _selectedIndex = (_selectedIndex + 1) % _actionButtons.Count;
            }
            else if (key == KeyCode.UpKey)
            {
                _selectedIndex = (_selectedIndex - 1 + _actionButtons.Count) % _actionButtons.Count;
            }

            _actionButtons[_selectedIndex].IsSelected = true;
            _currentAction = (Action)_selectedIndex;
        }

        public override void HandleInput()
        {
            if(_type == "Skills")
            {
                
                HandleSkillsInput();
                return;
            }
            if(_type == "Switch")
            {
                HandleSwitchInput();
                return;
            }

            if(_type == "Items")
            {
                if(SplashKit.KeyTyped(KeyCode.XKey))
                {
                    _type = "";
                    SplashKit.Delay(100);
                }
                _inventory.HandleInput("Items", this);
                if (SplashKit.KeyTyped(KeyCode.ZKey))
                {
                    if (_inventory.GetTotalItems() == 0) return;
                    Items item = _inventory.SelectedItem;
                    if(item.GetType() == typeof(Candy))
                    {
                        Candy candy = (Candy)item;
                        candy.Use(_opponent[0], _inventory);
                        if(candy.Result)
                        {
                            ExitBattle();
                            SplashKit.Delay(100);
                        }
                        else
                            ExecuteOpponentTurn(_player[_currentIdimonIndex], _opponent[0]);
                    }
                    else if(item.GetType() == typeof(Potion))
                    {
                        Potion potion = (Potion)item;
                        potion.Use(_player[_currentIdimonIndex]);
                    }
                    else if(item.GetType() == typeof(Dope))
                    {
                        Dope dope = (Dope)item;
                        dope.Use(_player[_currentIdimonIndex]);
                    }
                }
                return;
            }

            if(SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
            {
                // Implement battle logic
                Select();
                SplashKit.Delay(100);
            }
            else if (SplashKit.KeyTyped(KeyCode.DownKey) || SplashKit.KeyTyped(KeyCode.UpKey))
            {
                Navigate(SplashKit.KeyTyped(KeyCode.DownKey) ? KeyCode.DownKey : KeyCode.UpKey);
            }
            // throw new NotImplementedException();
        }

        public void HandleSkillsInput()
        {
            if(SplashKit.KeyTyped(KeyCode.XKey))
            {
                _type = "";
                _selectedSkillIndex = 0;
                SplashKit.Delay(100);
            }
            else if(SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
            {
                ExecuteTurn(_selectedSkillIndex);
                _currentTurn = Turn.Opponent;
                _type = "";
                _selectedSkillIndex = 0;
            }
            else if(SplashKit.KeyTyped(KeyCode.DownKey) || SplashKit.KeyTyped(KeyCode.UpKey))
            {
                Navigate(SplashKit.KeyTyped(KeyCode.DownKey) ? KeyCode.DownKey : KeyCode.UpKey);
            }
            else if(SplashKit.KeyTyped(KeyCode.RightKey) || SplashKit.KeyTyped(KeyCode.LeftKey))
            {
                Navigate(SplashKit.KeyTyped(KeyCode.RightKey) ? KeyCode.RightKey : KeyCode.LeftKey);
            }
        }

        public void HandleSwitchInput()
        {
            if(SplashKit.KeyTyped(KeyCode.XKey))
            {
                _type = "";
                _selectedIdimonIndex = 0;
                SplashKit.Delay(100);
            }
            else if(SplashKit.KeyTyped(KeyCode.ReturnKey) || SplashKit.KeyTyped(KeyCode.ZKey))
            {
                SwitchPlayerIdimon(_selectedIdimonIndex);
                _currentTurn = Turn.Opponent;
                _type = "";
                _selectedIdimonIndex = 0;
            }
            else if(SplashKit.KeyTyped(KeyCode.DownKey) || SplashKit.KeyTyped(KeyCode.UpKey))
            {
                Navigate(SplashKit.KeyTyped(KeyCode.DownKey) ? KeyCode.DownKey : KeyCode.UpKey);
            }
            else if(SplashKit.KeyTyped(KeyCode.RightKey) || SplashKit.KeyTyped(KeyCode.LeftKey))
            {
                Navigate(SplashKit.KeyTyped(KeyCode.RightKey) ? KeyCode.RightKey : KeyCode.LeftKey);
            }
        }

        private void ExecuteTurn(int selectedSkillIndex)
        {
            Idimons playerIdimon = _player[_currentIdimonIndex];
            Idimons opponentIdimon = _opponent[0];

            if (_currentTurn == Turn.Player)
            {
                ExecutePlayerTurn(playerIdimon, opponentIdimon, selectedSkillIndex);
                if (opponentIdimon.IsFainted())
                {
                    opponentIdimon.Faint();
                    playerIdimon.GainExperience(opponentIdimon.Level * 10);
                    ExitBattle();
                    return;
                    // Logic for winning the battle
                }
                ExecuteOpponentTurn(playerIdimon, opponentIdimon);
                if (playerIdimon.IsFainted())
                {
                    playerIdimon.Faint();
                    foreach (var idimon in _player)
                    {
                        if (!idimon.IsFainted())
                        {
                            _type = "Switch";
                            return;
                        }
                    }
                    ExitBattle();
                    return;
                    // Logic to switch Idimons
                }
            }
            else
            {
                ExecuteOpponentTurn(playerIdimon, opponentIdimon);
                if (playerIdimon.IsFainted())
                {
                    playerIdimon.Faint();
                    foreach (var idimon in _player)
                    {
                        if (!idimon.IsFainted())
                        {
                            _type = "Switch";
                            return;
                        }
                    }
                    ExitBattle();
                    return;
                    // SwitchPlayerIdimon(_currentIdimonIndex + 1);
                    // Logic to switch Idimons
                }
                ExecutePlayerTurn(playerIdimon, opponentIdimon, selectedSkillIndex);
                if (opponentIdimon.IsFainted())
                {
                    opponentIdimon.Faint();
                    playerIdimon.GainExperience(opponentIdimon.Level * 10);
                    ExitBattle();
                    return;
                    // Logic for winning the battle
                }
            }
            // Check if battle ends or continues
        }

        private void ExecutePlayerTurn(Idimons playerIdimon, Idimons opponentIdimon, int selectedSkillIndex)
        {
            // Handle player's move, currently using a placeholder skill index
            playerIdimon.Draw(100, 100);
            playerIdimon.Attacking(opponentIdimon, selectedSkillIndex);
            SplashKit.Delay(1000);
            _currentTurn = Turn.Opponent;
        }

        private void ExecuteOpponentTurn(Idimons playerIdimon, Idimons opponentIdimon)
        {
            // Simple AI: randomly select a skill
            Random rand = new Random();
            int selectedSkillIndex = rand.Next(0, opponentIdimon.Skills.Count);
            opponentIdimon.Attacking(playerIdimon, selectedSkillIndex);
            _currentTurn = Turn.Player;
        }

        private Turn DetermineFirstTurn()
        {
            Idimons playerIdimon = _player[_currentIdimonIndex];
            Idimons opponentIdimon = _opponent[0];

            if (playerIdimon.Speed > opponentIdimon.Speed)
            {
                return Turn.Player;
            }
            else if (playerIdimon.Speed < opponentIdimon.Speed)
            {
                return Turn.Opponent;
            }
            else
            {
                // In case of a speed tie, randomly decide
                Random rand = new Random();
                return rand.Next(2) == 0 ? Turn.Player : Turn.Opponent;
            }
        }

        private void SwitchPlayerIdimon(int newIndex)
        {
            if (newIndex >= 0 && newIndex < _player.Count && newIndex != _currentIdimonIndex)
            {
                int pre = _currentIdimonIndex;
                _currentIdimonIndex = newIndex;
                if(!_player[pre].IsFainted())
                    ExecuteOpponentTurn(_player[_currentIdimonIndex], _opponent[0]);
            }
        }
    }
}