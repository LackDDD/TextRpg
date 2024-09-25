namespace TextRPG2
{
    public class Character 
    {
        public string Name { get; }
        public string Job { get; } 
        public int Level { get; }
        public int Atk { get; }
        public int Def { get; }
        public int Hp { get; }
        public int Gold { get; set; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
      
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            Hp = hp;
            Gold = gold;
     
        }
    }

    public class Item 
    {
        public string Name { get; }
    
        public string Description { get; } 

        public int Type { get; } 

        public int Atk { get; } 

        public int Def { get; }

        public int Gold { get; }

        public bool IsEquipped { get; set; } 

        public bool IsPurchased { get; set; }

        public static int ItemCnt = 0;   
       
        public Item(string name, string description, int type, int atk, int def, int gold, bool isEquipped = false)
      
        {
            Name = name;
            Description = description;
            Type = type;
            Atk = atk;
            Def = def;
            IsEquipped = isEquipped;
        }

        public void PrintItemStatDescription(bool withNumber = false, int idx = 0, bool showPrice = true)
     
        {
            Console.Write("- ");

            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0} ", idx);
                Console.ResetColor();
            }


            if (IsEquipped) 
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("E"); 
                Console.ResetColor(); 
                Console.Write("]");
                Console.Write(PadRightForMixedText(Name, 9)); 
            }
            else
                Console.Write(PadRightForMixedText(Name, 12)); 
            Console.Write(" | ");

            if (Atk != 0) Console.Write($"Atk {(Atk >= 0 ? " + " : "")}{Atk}");  
            if (Def != 0) Console.Write($"Def {(Def >= 0 ? " + " : "")}{Def}");

            Console.Write(" | ");
            Console.WriteLine(Description); 
        }

        public static int GetPrintableLength(string str) 
        {
            int length = 0;
            foreach (char c in str)
            {
                if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
                {
                    length += 2; 
                }
                else
                {
                    length += 1; 
                }
            }
            return length;
        }
        public static string PadRightForMixedText(string str, int totalLength)
        {
            int currentLength = GetPrintableLength(str); 
            int padding = totalLength - currentLength; 
            return str.PadRight(str.Length + padding); 
        }


    }
    internal class Program
    {
        static Character _player; 
        static Item[] _items;    

        static void Main(string[] args) 
        {
            GameDataSetting(); 

            StartMenu(); 
        }
        private static void GameDataSetting() 
        {
            _player = new Character("chad", "전사", 1, 10, 5, 100, 1500); 
       

            _items = new Item[8]; 

   
            AddItem(new Item("무쇠 갑옷", "무쇠로 만들어진 튼튼한 갑옷입니다.", 0, 0, 5, 1000)); 
            AddItem(new Item("수련자 갑옷", "수련에 도움을 주는 갑옷입니다.", 0, 0, 9, 2000));
            AddItem(new Item("스파르타의 갑옷", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", 0, 0, 15, 3500));
            AddItem(new Item("가시 갑옷", "최선의 방어는 공격입니다.", 0, 0, 30, 5500)); 
            AddItem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 2, 0, 600));
            AddItem(new Item("청동 도끼", "쉽게 볼 수 있는 낡은 검입니다.", 1, 5, 0, 1500));
            AddItem(new Item("스파르타의 창", "스파르타의 전사들이 사용했다는 전설의 창입니다.", 1, 7, 0, 3000));
            AddItem(new Item("장미칼", "굉장히 예리합니다.", 1, 15, 0, 6000)); 
        }

        static void StartMenu() 
        {
            Console.Clear(); 
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■");
            Console.WriteLine(); 
            Console.WriteLine("");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("");

            switch (CheckValidInput(1, 3))

            {
                case 1:
                    StatusMenu(); 
                    break;
                case 2:
                    InventoryMenu(); 
                    break;
                case 3:
                    StoreMenu(); 
                    break;
                default:
                    Console.WriteLine("잘못된입력입니다");
                    break;
            }
        }

        private static void StatusMenu() 
        {
            
            Console.Clear();
            ShowHighlightText("■ 상태 보기 ■"); 
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            PrintTextWithHighlights("Lv ", _player.Level.ToString("00"));
            Console.WriteLine("");
            Console.WriteLine("{0} ({1})", _player.Name, _player.Job);

       
            int bonusAtk = getSumBonusAtk();
            int bonusDef = getSumBonusDef();
            PrintTextWithHighlights("공격력 : ", (_player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? string.Format(" (+{0})", bonusAtk) : "");
        
            PrintTextWithHighlights("방어력 : ", (_player.Def + bonusDef).ToString(), bonusDef > 0 ? string.Format(" (+{0})", bonusDef) : "");

            PrintTextWithHighlights("체력 : ", _player.Hp.ToString());
            PrintTextWithHighlights("골드 : ", _player.Gold.ToString());
            Console.WriteLine("");
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");

            switch (CheckValidInput(0, 0)) 
            {
                case 0:
                    StartMenu();
                    break;
            }
        }

        private static int getSumBonusAtk() 
        {
            int sum = 0; 
            for (int i = 0; i < Item.ItemCnt; i++)   
            {
                if (_items[i].IsEquipped) sum += _items[i].Atk;
            }
            return sum; 
        }
        private static int getSumBonusDef() 
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquipped) sum += _items[i].Def;
            }
            return sum;
        }


        private static void InventoryMenu() 
        {
            Console.Clear();
            ShowHighlightText("■ 인벤토리 ■");
            Console.WriteLine("보유중인 아이템을 관리 할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("");

            for (int i = 0; i < Item.ItemCnt; i++) 
            {
                _items[i].PrintItemStatDescription(true, i + 1); 
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("");

            switch (CheckValidInput(0, 1)) 
            {
                case 0:
                    StartMenu();
                    break;
                case 1:
                    EquipMenu(); 
                    break;
            }
        }

        private static void EquipMenu() 
        {
            Console.Clear();

            ShowHighlightText("■ 인벤토리 - 장착 관리 ■");
            Console.WriteLine("보유중인 아이템을 장착/해제 할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("");
  
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription(true, i + 1);
            }

            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            int keyInput = CheckValidInput(0, Item.ItemCnt);
            switch (keyInput)
            {
                case 0:
                    InventoryMenu();
                    break;
                default:
                    ToggleEquipStatus(keyInput - 1); 
                    EquipMenu();
                    break;

            }
        }

        private static void ToggleEquipStatus(int idx) 
        {
            _items[idx].IsEquipped = !_items[idx].IsEquipped; 
        }

        private static void ShowHighlightText(string text) 
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void PrintTextWithHighlights(string s1, string s2, string s3 = "") 
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow; 
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }





        private static int CheckValidInput(int min, int max) 
        {
            int keyInput; 
            bool result;  
            do 
            {
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                result = int.TryParse(Console.ReadLine(), out keyInput);
 
            }
            while (result == false || CheckIfVaild(keyInput, min, max) == false); 

            return keyInput;
        }

        private static bool CheckIfVaild(int keyInput, int min, int max) 
        {
            if (min <= keyInput && keyInput <= max) return true; 
            return false; 
        }

        static void AddItem(Item item) 
        {                              

            if (Item.ItemCnt == 8) return; 
            _items[Item.ItemCnt] = item; 
            Item.ItemCnt++;
        }
        private static void StoreMenu() 
        {
            Console.Clear();
            ShowHighlightText("■ 상 점 ■");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine("");
            PrintTextWithHighlights("", _player.Gold.ToString(), " G");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("");

            for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription(false, i + 1, true); 
            }


            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("");

            switch (CheckValidInput(0, 2))
            {
                case 0:
                    StartMenu();
                    break;
                case 1:
                    BuyItemMenu();
                    break;
                default:
                    Console.WriteLine("잘못된 입력입니다");
                    break;
            }
        }
        private static void BuyItemMenu() 
        {
            Console.Clear();
            ShowHighlightText("■ 상 점 ■");
            Console.WriteLine("필요한 아이템을 구매 할 수 있습니다.\n");
            Console.WriteLine("\n구매하고 싶은 아이템 번호를 입력 해주세요.");
            Console.WriteLine("0을 입력하면 상점으로 돌아갑니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            Console.WriteLine("");
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription(true, i + 1);
            }

            int choice = CheckValidInput(0, _items.Length);
            if (choice == 0) 
            {
                StoreMenu();
                return;
            }

            choice -= 1; 
            Item selectedItem = _items[choice];
            if (selectedItem.IsPurchased)   
            {
                Console.WriteLine("이미 구매한 아이템입니다.");
            }
            else if (_player.Gold >= selectedItem.Gold) 
            {
                selectedItem.IsPurchased = true; 
                _player.Gold -= selectedItem.Gold; 
                Console.WriteLine($"\n{selectedItem.Name} 구매를 완료했습니다.");
            }
            else 
            {
                Console.WriteLine("Gold가 부족합니다.");
            }
            Console.WriteLine("아무 키나 누르면, 상점으로 돌아갑니다.");
            Console.ReadKey();
            StoreMenu();
        }

    }
}
