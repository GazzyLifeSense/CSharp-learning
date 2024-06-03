int monsterDef = 10, monsterHp = 20, playerAtk;

while(monsterHp > 0)
{
    Console.WriteLine("press any key to attack");
    Console.ReadKey(true);

    playerAtk = new Random().Next(8, 13);
    if(playerAtk > monsterDef)
    {
        monsterHp -= playerAtk - monsterDef;
        Console.WriteLine($"atk:{playerAtk}, monsterHp: {monsterHp}");
    }
    else
    {
        Console.WriteLine($"atk: {playerAtk}, no dmg");
    }
}
Console.WriteLine("you win");
