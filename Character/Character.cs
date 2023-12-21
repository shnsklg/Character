using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Characters
{
    internal class Character
    {
        private string name; // имя перса
        private int maxHp; // максимально возможное количество хп
        private int curHp; // текущее количество хп
        private int attack; // сила атаки
        private bool team; // принадлежность к команде
        private int corX; // координата x
        private int corY; // координата y
        private int victory; // победы в битвах
        private int heal; // лечение

        /// <summary>
        /// Создание персонажа
        /// </summary>
        public Character()
        {
            this.name = "";
        }

        /// <summary>
        /// Выбор приватного метода
        /// </summary>
        /// <param name="aliveChar">Cписок живых персонажей</param>
        /// <param name="deadChar">Cписок мёртвых персонажей</param>
        public void Play(List<Character> aliveChar, List<Character> deadChar)
        {
            this.CharactersAppear(aliveChar, deadChar);
        }

        /// <summary>
        /// Создание персонажей
        /// </summary>
        /// <param name="aliveChar">Cписок живых персонажей</param>
        /// <param name="deadChar">Cписок мёртвых персонажей</param>
        private void CharactersAppear(List<Character> aliveChar, List<Character> deadChar)
        {
            if (aliveChar.Count < 2)
            {
                Console.WriteLine("Для начала игры создайте хотя бы двух персонажей.\n");
                while (aliveChar.Count < 2)
                {
                    this.CharacterAppear(aliveChar);
                }
            }
            Console.WriteLine("Чтобы начать игру, нажмите Enter. Если хотите создать ещё персонажа, введите '+' и нажмите Enter.");
            string answer = Console.ReadLine();
            Console.WriteLine();
            if (answer == "+")
            {
                this.CharacterAppear(aliveChar);
                this.CharactersAppear(aliveChar, deadChar);
            }
            else
            {
                Console.WriteLine("Игра началась! Удачи!\n");
                this.ChooseCharacter(aliveChar, deadChar);
            }
        }

        /// <summary>
        /// Создание персонажа
        /// </summary>
        /// <param name="aliveChar">Cписок живых персонажей</param>
        private void CharacterAppear(List<Character> aliveChar)
        {
            aliveChar.Add(new Character());
            Character last = aliveChar.Last();
            last.InfoIn(aliveChar);
            Console.WriteLine();
        }

        /// <summary>
        /// Заполнение информации о персонаже
        /// </summary>
        /// <param name="aliveChar">Cписок живых персонажей</param>
        private void InfoIn(List<Character> aliveChar)
        {
            this.NameInput(aliveChar);
            this.HpInput();
            this.AttackInput();
            this.TeamInput(aliveChar);
            this.InputCoordXY(aliveChar);
        }

        /// <summary>
        /// Заполнение имени персонажа
        /// </summary>
        /// <param name="aliveChar">Cписок живых персонажей</param>
        private void NameInput(List<Character> aliveChar)
        {
            Console.Write("Введите имя персонажа (обязательно): ");
            string name = Console.ReadLine();
            if (name == "")
            {
                this.NameInput(aliveChar);
            }
            int a = 0;
            foreach (Character character in aliveChar)
            {
                if (name == character.name)
                {
                    Console.WriteLine("Персонаж с таким именем уже существует. ");
                    a++;
                    this.NameInput(aliveChar);
                    break;
                }
            }
            if (a == 0)
            {
                this.name = name;
            }
        }

        /// <summary>
        /// Заполнение здоровья персонажа
        /// </summary>
        private void HpInput()
        {
            Console.Write("Введите здоровье персонажа (должно быть > 0): ");
            int xpMax = Convert.ToInt32(Console.ReadLine());
            if (xpMax <= 0)
            {
                this.HpInput();
            }
            else
            {
                this.maxHp = xpMax;
                this.curHp = xpMax;
            }
        }

        /// <summary>
        /// Заполнение силы удара персонажа
        /// </summary>
        private void AttackInput()
        {
            Console.Write("Введите силу удара персонажа (должна быть не больше уровня здоровья и > 0): ");
            int punch = Convert.ToInt32(Console.ReadLine());
            if (punch <= 0 || punch > this.maxHp)
            {
                this.AttackInput();
            }
            else
            {
                this.attack = punch;
            }
        }

        /// <summary>
        /// Заполнение команды персонажа
        /// </summary>
        /// <param name="aliveChar">Cписок живых персонажей</param>
        private void TeamInput(List<Character> aliveChar)
        {
            Console.WriteLine("Выберите команду: 1 или 2");
            int team0 = Convert.ToInt32(Console.ReadLine());
            bool team = true;
            switch (team0)
            {
                case 1:
                    team = true;
                    break;

                case 2:
                    team = false;
                    break;

                default:
                    this.TeamInput(aliveChar);
                    break;
            }
            if (aliveChar.Count == 2 && aliveChar[0].team == team)
            {
                Console.WriteLine("Для старта игры необходимо, чтобы было хотя бы по 1 персонажу с каждой из сторон.");
                this.TeamInput(aliveChar);
            }
            else
            {
                this.team = team;
            }
        }

        /// <summary>
        /// Заполнение координат расположения персонажа
        /// </summary>
        /// <param name="aliveChar">Cписок живых персонажей</param>
        private void InputCoordXY(List<Character> aliveChar)
        {
            Console.WriteLine("Расположите персонажа на поле: ");
            Console.Write("x: ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.Write("y: ");
            int y = Convert.ToInt32(Console.ReadLine());
            int a = 0;
            foreach (Character character in aliveChar)
            {
                if (character.team != this.team && character.corX == x && character.corY == y)
                {
                    Console.WriteLine("Это расположение занято противником. Необходимо выбрать другую точку.");
                    a++;
                    this.InputCoordXY(aliveChar);
                }
            }
            if (a == 0)
            {
                this.corX = x;
                this.corY = y;
            }
        }

        /// <summary>
        /// Выбор персонажа
        /// </summary>
        /// <param name="aliveChar">Cписок живых персонажей</param>
        /// <param name="deadChar">Cписок мёртвых персонажей</param>
        private void ChooseCharacter(List<Character> aliveChar, List<Character> deadChar)
        {
            Console.WriteLine("Выберите персонажа:\n");
            foreach (Character character in aliveChar)
            {
                Console.WriteLine((aliveChar.IndexOf(character) + 1) + ".");
                character.InfoOutput();
                Console.WriteLine();
            }
            int charChoice = Convert.ToInt32(Console.ReadLine()) - 1;
            foreach (Character character in aliveChar)
            {
                if (charChoice == aliveChar.IndexOf(character))
                {
                    Console.WriteLine();
                    character.Choose(aliveChar, deadChar);
                    break;
                }
            }
        }

        /// <summary>
        /// Вывод информации о персонаже
        /// </summary>
        private void InfoOutput()
        {
            Console.WriteLine("Имя: " + this.name);
            Console.WriteLine("Здоровье: " + this.curHp + "/" + this.maxHp);
            Console.WriteLine("Сила удара: " + this.attack);
            if (this.team == true)
            {
                Console.WriteLine("Команда: 1");
            }
            else
            {
                Console.WriteLine("Команда: 2");
            }
            Console.WriteLine("Победы: " + this.victory);
            if (this.curHp >= 0)
            {
                Console.WriteLine("Координаты местонахождения: " + this.corX + ";" + this.corY);
            }
            else
            {
                Console.WriteLine("Погиб.");
            }
        }

        /// <summary>
        /// Выбор действия
        /// </summary>
        /// <param name="aliveChar">Список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        private void Choose(List<Character> aliveChar, List<Character> deadChar)
        {
            this.TeamOutput(aliveChar);
            if (this.heal == 1)
            {
                this.heal = 0;
                this.TeamCheck(aliveChar, deadChar);
            }
            else
            {
                Console.WriteLine("\nВыберите действие:\n1 - Вывести информацию о персонаже\n2 - Переместиться по горизонтали\n3 - Переместиться по вертикали\n4 - Вылечить союзника\n5 - Сменить команду\n6 - Сменить персонажа\nEnter - Завершить игру\n");
                string actChoice = Console.ReadLine();
                Console.WriteLine();
                switch (actChoice)
                {
                    case "1":
                        this.InfoOutput();
                        this.Choose(aliveChar, deadChar);
                        break;

                    case "2":
                        this.MoveX(aliveChar, deadChar);
                        break;

                    case "3":
                        this.MoveY(aliveChar, deadChar);
                        break;

                    case "4":
                        this.WoundedSearch(aliveChar, deadChar);
                        break;

                    case "5":
                        this.TeamChange(aliveChar, deadChar);
                        break;

                    case "6":
                        this.ChooseCharacter(aliveChar, deadChar);
                        break;

                    default:
                        this.GameOver(aliveChar, deadChar);
                        break;
                }
            }
        }

        /// <summary>
        /// Вывод информации о своей команде
        /// </summary>
        /// <param name="aliveChar">Список живых персонажей</param>
        private void TeamOutput(List<Character> aliveChar)
        {
            int team = aliveChar.Count(c => c.team == this.team && c.name != this.name);
            if (team > 0)
            {
                int i = 1;
                Console.WriteLine("\nВаша команда:");
                foreach (Character character in aliveChar)
                {
                    if (character.team == this.team && character.name != this.name)
                    {
                        Console.WriteLine(i + ". " + character.name + "(" + character.attack + "), " + character.curHp + "/" + character.maxHp + ", " + character.corX + ";" + character.corY);
                        i++;
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Передвижение по горизонтали
        /// </summary>
        /// <param name="aliveChar">Список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        private void MoveX(List<Character> aliveChar, List<Character> deadChar)
        {
            this.SelfHealthCheck(aliveChar, deadChar);
            Console.Write("Переместиться по горизонтали на: ");
            int x = Convert.ToInt32(Console.ReadLine());
            this.corX += x;
            Console.WriteLine("Ваше новое расположение: " + this.corX + ";" + this.corY);
            this.TeamCheck(aliveChar, deadChar);
        }

        /// <summary>
        /// Передвижение по вертикали
        /// </summary>
        /// <param name="aliveChar">Список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        private void MoveY(List<Character> aliveChar, List<Character> deadChar)
        {
            this.SelfHealthCheck(aliveChar, deadChar);
            Console.Write("Переместиться по вертикали на: ");
            int y = Convert.ToInt32(Console.ReadLine());
            this.corY += y;
            Console.WriteLine("Ваше новое расположение: " + this.corX + ";" + this.corY);
            this.TeamCheck(aliveChar, deadChar);
        }

        /// <summary>
        /// Проверка здоровья персонажа
        /// </summary>
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">список мёртвых персонажей</param>
        private void SelfHealthCheck(List<Character> aliveChar, List<Character> deadChar)
        {
            if (this.curHp < 0)
            {
                Console.WriteLine("Ты мёртв. Действие невозможно.");
                this.Choose(aliveChar, deadChar);
            }
        }

        /// <summary>
        /// Проверка команды встреченного персонажа
        /// </summary>
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">список мёртвых персонажей</param>
        private void TeamCheck(List<Character> aliveChar, List<Character> deadChar)
        {
            List<Character> fightChar = new List<Character>();
            List<string> friendChar = new List<string>();
            foreach (Character character in aliveChar)
            {
                if (character.name != this.name && character.corX == this.corX && character.corY == this.corY)
                {
                    if (this.team == character.team)
                    {
                        friendChar.Add(character.name);
                    }
                    else
                    {
                        fightChar.Add(character);
                    }
                }
            }
            if (friendChar.Count > 0)
            {
                int i = 1;
                Console.WriteLine("Ваши союзники в этой точке:");
                foreach (string name in friendChar)
                {
                    Console.WriteLine(i + ". " + name);
                    i++;
                }
            }
            friendChar.Clear();
            if (fightChar.Count > 0)
            {
                int i = 1;
                Console.WriteLine("Ваши противники в этой точке:");
                foreach (Character character in fightChar)
                {
                    Console.WriteLine(i + ". " + character.name);
                    i++;
                }
                this.Fight(aliveChar, deadChar, fightChar);
            }
            else
            {
                Console.WriteLine("В этой точке нет врагов.");
                this.Choose(aliveChar, deadChar);
            }
        }

        /// <summary>
        /// Драка
        /// </summary>
        /// <param name="aliveChar">Список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        /// <param name="fightChar">Список персонажей для боя</param>
        private void Fight(List<Character> aliveChar, List<Character> deadChar, List<Character> fightChar)
        {
            Console.WriteLine("Чтобы начать битву, нажмите Enter. Если вы хотите избежать боя, введите '-' и нажмите Enter.");
            string answ = Console.ReadLine();
            if (answ != "-")
            {
                int punch = 0;
                foreach (Character character in fightChar)
                {
                    punch += character.attack;
                }
                foreach (Character character in fightChar)
                {
                    if (this.victory >= 10)
                    {
                        int a = this.victory;
                        this.UltDamage(character, aliveChar, deadChar);
                        int b = this.victory;
                        if (a > b)
                        {
                            break;
                        }
                    }
                    if (this.attack / fightChar.Count <= aliveChar.Find(c => c.name == character.name).curHp)
                    {
                        this.Damage(aliveChar.Find(c => c.name == character.name), fightChar.Count);
                    }
                    else
                    {
                        this.Kill(aliveChar, deadChar, aliveChar.Find(c => c.name == character.name), fightChar.Count);
                    }
                }
                for (int i = 0; i < fightChar.Count; i++)
                {
                    if (fightChar[i].curHp < 0)
                    {
                        fightChar.Remove(fightChar[i]);
                    }
                }
                this.curHp -= punch;
                if (this.curHp > 0)
                {
                    Console.WriteLine("Вы получили урон.");
                    if (fightChar.Count > 0)
                    {
                        this.Fight(aliveChar, deadChar, fightChar);
                    }
                    else
                    {
                        this.GameOverCheck(aliveChar, deadChar);
                    }
                }
                else if (this.curHp == 0)
                {
                    Console.WriteLine("Вы получили урон. Вы умерли.");
                    if (fightChar.Count > 0)
                    {
                        if (this.victory >= 5)
                        {
                            int a = this.victory;
                            this.PowerfulHealing();
                            int b = this.victory;
                            if (a > b)
                            {
                                this.Fight(aliveChar, deadChar, fightChar);
                            }
                        }
                        this.ChooseCharacter(aliveChar, deadChar);
                    }
                    else
                    {
                        this.GameOverCheck(aliveChar, deadChar);
                    }
                }
                else
                {
                    Console.WriteLine("Ты умер! Игра окончена!.");
                    deadChar.Add(aliveChar.Find(c => c.name == this.name));
                    aliveChar.Remove(aliveChar.Find(c => c.name == this.name));
                    this.GameOverCheck(aliveChar, deadChar);
                }
            }
            else
            {
                this.Run(aliveChar, deadChar);
            }
        }

        /// <summary>
        /// Ульта
        /// </summary>
        /// <param name="character">Противник</param>
        /// <param name="aliveChar">список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        private void UltDamage(Character character, List<Character> aliveChar, List<Character> deadChar)
        {
            Console.WriteLine("Можно нанести " + character.name + " урон ультой. Чтобы сделать это, нажмите Enter. Если хочешь приберечь ультмативную способность для более важного момента, введите '-' и нажмите Enter.");
            string answ = Console.ReadLine();
            if (answ != "-")
            {
                character.curHp = -1;
                deadChar.Add(character);
                aliveChar.Remove(character);
                this.victory -= 10;
            }
        }

        /// <summary>
        /// Нанесение урона
        /// </summary>
        /// <param name="character">Противник</param>
        /// <param name="div">Общее число противников</param>
        private void Damage(Character character, int div)
        {
            int punch = this.attack / div;
            character.curHp -= punch;
            Console.WriteLine(character.name + " получил урон.");
        }

        /// <summary>
        /// Убийство
        /// </summary>
        /// <param name="aliveChar">Список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        /// <param name="character">Противник</param>
        /// <param name="div">Общее число противников</param>
        private void Kill(List<Character> aliveChar, List<Character> deadChar, Character character, int div)
        {
            character.curHp -= this.attack / div;
            this.victory++;
            Console.WriteLine(character.name + " убит.");
            deadChar.Add(character);
            aliveChar.Remove(character);
        }

        /// <summary>
        /// Восстановление здоровья
        /// </summary>
        private void PowerfulHealing()
        {
            Console.WriteLine("Вы можете полностью излечиться. Чтобы сделать это, нажмите Enter. Если не хотите, введите '-' и нажмите Enter.");
            string answ = Console.ReadLine();
            if (answ != "-")
            {
                this.curHp = this.maxHp;
                this.victory -= 5;
                Console.WriteLine("Здоровье восстановлено.");
            }
        }

        /// <summary>
        /// Бегство
        /// </summary>
        /// <param name="aliveChar">Список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        private void Run(List<Character> aliveChar, List<Character> deadChar)
        {
            Console.WriteLine("Бегите.");
            Console.Write("Переместиться по горизонтали на: ");
            int x = Convert.ToInt32(Console.ReadLine());
            this.corX += x;
            Console.Write("Переместиться по вертикали на: ");
            int y = Convert.ToInt32(Console.ReadLine());
            this.corY += y;
            Console.WriteLine("Ваше новое расположение: " + this.corX + ";" + this.corY);
            TeamCheck(aliveChar, deadChar);
        }

        /// <summary>
        /// Проверка на окончание игры
        /// </summary>
        /// <param name="aliveChar">Список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        private void GameOverCheck(List<Character> aliveChar, List<Character> deadChar)
        {
            if (aliveChar.Count(c => c.team == true) == 0 || aliveChar.Count(c => c.team == false) == 0)
            {
                this.GameOver(aliveChar, deadChar);
            }
        }

        /// <summary>
        /// Завершение игры
        /// </summary>
        /// <param name="aliveChar">Список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        private void GameOver(List<Character> aliveChar, List<Character> deadChar)
        {
            if (aliveChar.Count(c => c.team == true) == 0 && aliveChar.Count(c => c.team == false) > 0)
            {
                Console.WriteLine("\nИгра окончена. Победила команда 2.\n");
            }
            else if (aliveChar.Count(c => c.team == true) > 0 && aliveChar.Count(c => c.team == false) == 0)
            {
                Console.WriteLine("\nИгра окончена. Победила команда 1.\n");
            }
            else if (aliveChar.Count(c => c.team == true) > 0 && aliveChar.Count(c => c.team == false) > 0)
            {
                Console.WriteLine("\nИгра окончена. Ничья.\n");
            }
            else
            {
                Console.WriteLine("\nИгра окончена.\n");
            }
            Console.WriteLine("Команда 1:");
            Console.ForegroundColor = ConsoleColor.Blue;
            this.Statistics(aliveChar, true);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            this.Statistics(deadChar, true);
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Команда 2:");
            Console.ForegroundColor = ConsoleColor.Blue;
            this.Statistics(aliveChar, false);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Red;
            this.Statistics(deadChar, false);
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("Для выхода нажмите Enter.");
            Console.ReadLine();
        }

        /// <summary>
        /// Статистика команды после окончания игры
        /// </summary>
        /// <param name="someChar">Список персонажей</param>
        /// <param name="team">Команда</param>
        private void Statistics(List<Character> someChar, bool team)
        {
            foreach (Character character in someChar)
            {
                if (character.team == team)
                {
                    Console.WriteLine(character.name);
                }
            }
        }

        /// <summary>
        /// Поиск раненых союзников
        /// </summary>
        /// <param name="aliveChar">Список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        private void WoundedSearch(List<Character> aliveChar, List<Character> deadChar)
        {
            this.SelfHealthCheck(aliveChar, deadChar);
            int wound = aliveChar.Count(c => c.team == this.team && c.name != this.name && c.curHp < c.maxHp);
            if (wound > 0)
            {
                Console.WriteLine("Выберите, кого собираетесь вылечить: ");
                int i = 1;
                foreach (Character character in aliveChar)
                {
                    if (character.team == this.team && character.name != this.name && character.curHp < character.maxHp)
                    {
                        Console.WriteLine(i + ". " + character.name);
                        i++;
                    }
                }
                int index = Convert.ToInt32(Console.ReadLine()) - 1;
                this.Heal(aliveChar[index], aliveChar, deadChar);
            }
            else
            {
                Console.WriteLine("Лечить некого.");
                this.Choose(aliveChar, deadChar);
            }
        }

        /// <summary>
        /// Лечение
        /// </summary>
        /// <param name="character">Союзник</param>
        /// <param name="aliveChar">Список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        private void Heal(Character character, List<Character> aliveChar, List<Character> deadChar)
        {
            Console.Write("Введите количество единиц лечения: ");
            int hp = Convert.ToInt32(Console.ReadLine());
            if (hp <= 0)
            {
                Console.WriteLine("Вы хотите лечить или покалечить?.");
                this.Heal(character, aliveChar, deadChar);
            }
            else if (hp > this.curHp)
            {
                Console.WriteLine("У Вас недостаточно единиц здоровья.");
                this.Heal(character, aliveChar, deadChar);
            }
            else if ((character.curHp + hp) > character.maxHp)
            {
                Console.WriteLine("Вы не можете лечить на большее количество единиц здоровья, чем это необходимо.");
                this.Heal(character, aliveChar, deadChar);
            }
            else
            {
                character.curHp += hp;
                character.heal++;
                this.curHp -= hp;
                Console.WriteLine(character.name + " получил лечение. Здоровье: " + character.curHp);
                this.Choose(aliveChar, deadChar);
            }
        }

        /// <summary>
        /// Смена команды
        /// </summary>
        /// <param name="aliveChar">Список живых персонажей</param>
        /// <param name="deadChar">Список мёртвых персонажей</param>
        private void TeamChange(List<Character> aliveChar, List<Character> deadChar)
        {
            if (this.team == true)
            {
                this.team = false;
            }
            else
            {
                this.team = true;
            }
            Console.WriteLine("Вы сменили команду.");
            TeamCheck(aliveChar, deadChar);
        }
    }
}

