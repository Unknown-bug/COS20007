using System;
using SplashKitSDK;

namespace Idimon
{
    public class Map
    {
        Window _window;
        private char[,] _objectMapData, _backgroundMapData;
        private Dictionary<char, Block> _blockTypes;
        private int _blockSize;

        public Map(char[,] objectMapData, char[,] backgroundMapData, Dictionary<char, Block> blockTypes, Window window, int blockSize = 64)
        {
            _objectMapData = objectMapData;
            _backgroundMapData = backgroundMapData;
            _blockTypes = blockTypes;
            _blockSize = blockSize;
            _window = window;
        }

        public bool CanMoveTo(int x, int y)
        {
            
            if (x < 0 || y < 0 || x >= _objectMapData.GetLength(0) || y >= _objectMapData.GetLength(1))
            {
                return false;
            }
            return !_blockTypes[_objectMapData[x, y]].IsSolid;
        }

        public void Draw(Player player)
        {
            int screenWidth = _window.Width;
            int screenHeight = _window.Height;

            int playerX = (int)(player.Position.X+32) / _blockSize + 1; 
            int playerY = (int)(player.Position.Y+32) / _blockSize + 1;

            int halfScreenWidth = screenWidth / (2 * _blockSize);
            int halfScreenHeight = screenHeight / (2 * _blockSize);

            int startX = playerX - halfScreenWidth;
            int startY = playerY - halfScreenHeight;
            for (int y = 0; y < screenHeight / _blockSize; y++)
            {
                for (int x = 0; x < screenWidth / _blockSize; x++)
                {
                    int mapX = startX + x;
                    int mapY = startY + y;
                    if (mapX >= 0 && mapY >= 0 && mapX < screenWidth / _blockSize && mapY < screenHeight / _blockSize)
                    {
                        char blockChar = _backgroundMapData[mapY, mapX];
                        char objectChar = _objectMapData[mapY, mapX];
                        if (_blockTypes.ContainsKey(blockChar))
                        {
                            _blockTypes[blockChar].Draw(_window, x * _blockSize, y * _blockSize);
                        }
                        if (_blockTypes.ContainsKey(objectChar))
                        {
                            _blockTypes[objectChar].Draw(_window, x * _blockSize, y * _blockSize);
                        }
                    }
                }
            }
            // _player.Draw(_player.X*64, _player.Y*64);
        }

        public void HandleEnvent(int x, int y, List<Idimons> myIdimons, Inventory myInventory)
        {
            if (x < 0 || y < 0 || x >= _objectMapData.GetLength(0) || y >= _objectMapData.GetLength(1))
            {
                return;
            }

            if(_blockTypes[_objectMapData[y, x]].Type == "Tall Grass")
            {
                TouchGrassEvent(myIdimons, myInventory);
            }
            else if(_blockTypes[_objectMapData[y, x]].Type == "Healer")
            {
                _blockTypes[_objectMapData[y, x]].Interact(myIdimons, myInventory);
            }
        }

        public void TouchGrassEvent(List<Idimons> myIdimon, Inventory myInventory)
        {
            List<Idimons> idimons = new List<Idimons>
            {
                new Beaver(),
                new tiger(),
                new Student(),
                new Idiot(),
                new Nigga(),
                new MultilevelSeller()
            };

            Idimons encounteredIdimon = PerformGacha(idimons, 0.30);
            if (encounteredIdimon != null)
            {
                Console.WriteLine($"You have encountered a wild {encounteredIdimon.Name}!");
                List<Idimons> opponent = new List<Idimons> {
                    encounteredIdimon
                };
                GameScreen pre = (GameScreen)Game.CurrentScreen; 
                BattleScreen newBattle = new BattleScreen(_window, myIdimon, opponent, myInventory, pre);
                Game.CurrentScreen = newBattle;
            }
            else
            {
                Console.WriteLine("No Idimon encountered this time.");
            }
        }

        public static Idimons PerformGacha(List<Idimons> idimons, double gachaChance)
        {
            Random random = new Random();
            if (random.NextDouble() > gachaChance)
            {
                return null; // Gacha event does not occur
            }
            Dictionary<string, double> rankRates = new Dictionary<string, double>
            {
                { "Legendary", 0.01 },
                { "Mythical", 0.30 },
                { "Normal", 0.69 }
            };
            string selectedRank;
            do
            {
                selectedRank = RollForRank(rankRates);
            }
            while(idimons.Where(i => i.Rank == selectedRank).Count() == 0);
            
            List<Idimons> idimonsOfSelectedRank = idimons.Where(i => i.Rank == selectedRank).ToList();

            return RollForIdimon(idimonsOfSelectedRank);
        }

        private static string RollForRank(Dictionary<string, double> rankRates)
        {
            Random random = new Random();
            double roll = random.NextDouble();
            double cumulative = 0.0;

            foreach (var rankRate in rankRates)
            {
                cumulative += rankRate.Value;
                if (roll < cumulative)
                {
                    return rankRate.Key;
                }
            }

            // In case no rank is selected due to rounding issues, return the last rank
            return RollForRank(rankRates);
        }

        private static Idimons RollForIdimon(List<Idimons> idimons)
        {
            Random random = new Random();
            int index = random.Next(0, idimons.Count);
            return idimons[index];
        }
    }
}
