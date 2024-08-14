using System;
using SplashKitSDK;
using Idimon;

namespace Idimon
{
    public class GameScreen : Screens
    {
        private GameMenu _gameMenu;
        private Player _player;
        private Map _Map;
        private SplashKitSDK.Timer _timer;
        private char[,] _objectmapData = {
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', 'x', 'x', 'x', 'x', 'x', 'x', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', 'x', 'x', 'x', 'x', 'x', 'x', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', 'h', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' }
            };

        private char[,] _backgroudmapData = {
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' },
            { '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*', '*' }
        };
        private Dictionary<char, Block> _blockTypes = new Dictionary<char, Block>
        {
            { '*', new BackgroundBlocks("grass", "img/Grass.png", false) },
            { 'x', new BackgroundBlocks("Tall Grass", "img/TallGrass.png", false) },
            { 'h', new HealerBlocks("Healer", "img/NPC/Heal.png", true) }
        };
        private Dictionary<string, Items> _items = new Dictionary<string, Items>
        {
            { "Candy", new Candy("Candy", "Use to catch Idimon", "img\\items\\Candy2.png", 0.5) },
            { "Master Candy", new Candy("Master Candy", "Use to catch Idimon", "img\\items\\candy1.png", 1) }, 
            { "Potion", new Potion("Potion", "Heal 50 HP", "img\\items\\background.png", 50) },
            { "Dope", new Dope("Dope", "Level up Idimon", "img\\items\\dope.png") }
            // { "EXP Sharing", new Items("EXP Sharing", "Share EXP with all party members", "img\\items\\background.png", 1, true, true, "Key Items") }
        };

        private List<string> _characterImages = new List<string>
        {
            "img\\PlayerIMG\\down0.png",
            "img\\PlayerIMG\\down1.png",
            "img\\PlayerIMG\\down2.png",
            "img\\PlayerIMG\\down3.png",
        };

        public GameScreen(Window window) : base(window)
        {
            _Map = new Map(_objectmapData, _backgroudmapData, _blockTypes, _window);

            _player = Player.GetInstance("Ash", _characterImages, new Point2D() { X = 400, Y = 300 }, _window, _Map);
            _player.Inventory.AddItem(_items["Candy"]);
            _player.Inventory.AddItem(_items["Master Candy"]);
            _player.Inventory.AddItem(_items["Potion"]);
            _player.Inventory.AddItem(_items["Dope"]);

            _player.Inventory.AddIdimon(new Student());
            _player.Inventory.AddIdimon(new Beaver());
            _player.Inventory.Idimons[0].LevelUp();
            _player.Inventory.Idimons[0].LevelUp();

            _gameMenu = new GameMenu(_player, _window);

            _timer = new SplashKitSDK.Timer("GameTimer");
            _timer.Start();
        }

        public override void Update()
        {
            double deltaTime = _timer.Ticks / 1000.0;
            _timer.Reset();

            if (!_gameMenu.Visible)
            {
                _player.Update(deltaTime);
            }
        }

        public override void Draw()
        {
            _window.Clear(Color.White);
            _Map.Draw(_player);
            _player.Draw();
            for (int i=0; i<_player.Inventory.Idimons.Count; i++)
            {
                if (_player.Inventory.Idimons[i].CanEvolve)
                {
                    Console.WriteLine("Evolve");
                    _player.Inventory.Idimons[i] = _player.Inventory.Idimons[i].Evolve();
                }
            }
            _gameMenu.Draw();
            _window.Refresh(60);
        }

        public override void HandleInput()
        {
            _gameMenu.HandleInput();
        }
    }
}
