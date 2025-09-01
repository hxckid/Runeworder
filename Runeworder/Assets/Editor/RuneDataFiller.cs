using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Rune_SO))]
public class RuneDataFiller : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Rune_SO rune = (Rune_SO)target;
        
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Quick Data Fill", EditorStyles.boldLabel);
        
        if (GUILayout.Button("Fill Rune Data"))
        {
            FillRuneData(rune);
        }
    }
    
    private void FillRuneData(Rune_SO rune)
    {
        // Получаем базу данных камней
        GemsDB_SO gemsDB = GetGemsDatabase();
        
        // Отладочная информация
        if (gemsDB == null)
        {
            Debug.LogError("GemsDB_SO не найдена! Создайте базу данных камней.");
            return;
        }
        else
        {
            Debug.Log($"Найдена база данных камней с {gemsDB.gems.Count} камнями");
        }
        
        // Очищаем существующие рецепты
        rune.craftingRunes.Clear();
        rune.craftingGems.Clear();
        
        Debug.Log($"Заполняем данные для руны: {rune.runeType}");
        
        // Здесь будем заполнять данные конкретной руны
        switch (rune.runeType)
        {
            case RunesEn.El:
                rune.runeLevel = 11;
                rune.weaponBonus = "+50 to Attack Rating\n+1 to Light Radius";
                rune.armorBonus = "+15 Defense\n+1 to Light Radius";
                rune.helmetBonus = "+15 Defense\n+1 to Light Radius";
                rune.shieldBonus = "+15 Defense\n+1 to Light Radius";
                // El не крафтится
                break;
                
            case RunesEn.Eld:
                rune.runeLevel = 11;
                rune.weaponBonus = "+75% Damage to Undead\n+50 to Attack Rating Against Undead";
                rune.armorBonus = "15% Slower Stamina Drain\n+1 to Light Radius";
                rune.helmetBonus = "15% Slower Stamina Drain\n+1 to Light Radius";
                rune.shieldBonus = "7% Increased Chance of Blocking\n+1 to Light Radius";
                // 3x El = Eld
                rune.craftingRunes.Add(RunesEn.El);
                rune.craftingRunes.Add(RunesEn.El);
                rune.craftingRunes.Add(RunesEn.El);
                break;
                
            case RunesEn.Tir:
                rune.runeLevel = 13;
                rune.weaponBonus = "+2 to Mana after each Kill";
                rune.armorBonus = "+2 to Mana after each Kill";
                rune.helmetBonus = "+2 to Mana after each Kill";
                rune.shieldBonus = "+2 to Mana after each Kill";
                // 3x Eld = Tir
                rune.craftingRunes.Add(RunesEn.Eld);
                rune.craftingRunes.Add(RunesEn.Eld);
                rune.craftingRunes.Add(RunesEn.Eld);
                break;
                
            case RunesEn.Nef:
                rune.runeLevel = 13;
                rune.weaponBonus = "Knockback";
                rune.armorBonus = "+30 Defense vs. Missile";
                rune.helmetBonus = "+30 Defense vs. Missile";
                rune.shieldBonus = "+30 Defense vs. Missile";
                // 3x Tir = Nef
                rune.craftingRunes.Add(RunesEn.Tir);
                rune.craftingRunes.Add(RunesEn.Tir);
                rune.craftingRunes.Add(RunesEn.Tir);
                break;
                
            case RunesEn.Eth:
                rune.runeLevel = 15;
                rune.weaponBonus = "-25% Target Defense";
                rune.armorBonus = "Regenerate Mana 15%";
                rune.helmetBonus = "Regenerate Mana 15%";
                rune.shieldBonus = "Regenerate Mana 15%";
                // 3x Nef = Eth
                rune.craftingRunes.Add(RunesEn.Nef);
                rune.craftingRunes.Add(RunesEn.Nef);
                rune.craftingRunes.Add(RunesEn.Nef);
                break;
                
            case RunesEn.Ith:
                rune.runeLevel = 15;
                rune.weaponBonus = "+9 to Maximum Damage";
                rune.armorBonus = "15% Damage Taken Goes to Mana";
                rune.helmetBonus = "15% Damage Taken Goes to Mana";
                rune.shieldBonus = "15% Damage Taken Goes to Mana";
                // 3x Eth = Ith
                rune.craftingRunes.Add(RunesEn.Eth);
                rune.craftingRunes.Add(RunesEn.Eth);
                rune.craftingRunes.Add(RunesEn.Eth);
                break;
                
            case RunesEn.Tal:
                rune.runeLevel = 17;
                rune.weaponBonus = "+75 Poison Damage Over 5 Seconds";
                rune.armorBonus = "Poison Resist +30%";
                rune.helmetBonus = "Poison Resist +30%";
                rune.shieldBonus = "Poison Resist +35%";
                // 3x Ith = Tal
                rune.craftingRunes.Add(RunesEn.Ith);
                rune.craftingRunes.Add(RunesEn.Ith);
                rune.craftingRunes.Add(RunesEn.Ith);
                break;
                
            case RunesEn.Ral:
                rune.runeLevel = 19;
                rune.weaponBonus = "Adds 5-30 Fire Damage";
                rune.armorBonus = "Fire Resist +30%";
                rune.helmetBonus = "Fire Resist +30%";
                rune.shieldBonus = "Fire Resist +35%";
                // 3x Tal = Ral
                rune.craftingRunes.Add(RunesEn.Tal);
                rune.craftingRunes.Add(RunesEn.Tal);
                rune.craftingRunes.Add(RunesEn.Tal);
                break;
                
            case RunesEn.Ort:
                rune.runeLevel = 21;
                rune.weaponBonus = "Adds 1-50 Lightning Damage";
                rune.armorBonus = "Lightning Resist +30%";
                rune.helmetBonus = "Lightning Resist +30%";
                rune.shieldBonus = "Lightning Resist +35%";
                // 3x Ral = Ort
                rune.craftingRunes.Add(RunesEn.Ral);
                rune.craftingRunes.Add(RunesEn.Ral);
                rune.craftingRunes.Add(RunesEn.Ral);
                break;
                
            case RunesEn.Thul:
                rune.runeLevel = 23;
                rune.weaponBonus = "Adds 3-14 Cold Damage";
                rune.armorBonus = "Cold Resist +30%";
                rune.helmetBonus = "Cold Resist +30%";
                rune.shieldBonus = "Cold Resist +35%";
                // 3x Ort = Thul
                rune.craftingRunes.Add(RunesEn.Ort);
                rune.craftingRunes.Add(RunesEn.Ort);
                rune.craftingRunes.Add(RunesEn.Ort);
                break;
                
            case RunesEn.Amn:
                rune.runeLevel = 25;
                rune.weaponBonus = "7% Life Stolen per Hit";
                rune.armorBonus = "Attacker Takes Damage of 14";
                rune.helmetBonus = "Attacker Takes Damage of 14";
                rune.shieldBonus = "Attacker Takes Damage of 14";
                // 3x Thul + Chipped Topaz = Amn
                rune.craftingRunes.Add(RunesEn.Thul);
                rune.craftingRunes.Add(RunesEn.Thul);
                rune.craftingRunes.Add(RunesEn.Thul);
                Debug.Log($"Добавлено {rune.craftingRunes.Count} рун в рецепт Amn");
                if (gemsDB != null)
                {
                    Gem_SO chippedTopaz = GetGem(gemsDB, GemType.Topaz, GemQuality.Chipped);
                    if (chippedTopaz != null)
                    {
                        rune.craftingGems.Add(chippedTopaz);
                        Debug.Log("Добавлен Chipped Topaz в рецепт Amn");
                    }
                    else
                    {
                        Debug.LogWarning("Chipped Topaz не найден в базе данных");
                    }
                }
                break;
                
            case RunesEn.Sol:
                rune.runeLevel = 27;
                rune.weaponBonus = "+9 to Minimum Damage";
                rune.armorBonus = "Damage Reduced by 7";
                rune.helmetBonus = "Damage Reduced by 7";
                rune.shieldBonus = "Damage Reduced by 7";
                // 3x Amn + Chipped Amethyst = Sol
                rune.craftingRunes.Add(RunesEn.Amn);
                rune.craftingRunes.Add(RunesEn.Amn);
                rune.craftingRunes.Add(RunesEn.Amn);
                if (gemsDB != null)
                {
                    Gem_SO chippedAmethyst = GetGem(gemsDB, GemType.Amethyst, GemQuality.Chipped);
                    if (chippedAmethyst != null)
                        rune.craftingGems.Add(chippedAmethyst);
                }
                break;
                
            case RunesEn.Shael:
                rune.runeLevel = 29;
                rune.weaponBonus = "20% Increased Attack Speed";
                rune.armorBonus = "20% Faster Hit Recovery";
                rune.helmetBonus = "20% Faster Hit Recovery";
                rune.shieldBonus = "20% Faster Block Rate";
                // 3x Sol + Chipped Sapphire = Shael
                rune.craftingRunes.Add(RunesEn.Sol);
                rune.craftingRunes.Add(RunesEn.Sol);
                rune.craftingRunes.Add(RunesEn.Sol);
                if (gemsDB != null)
                {
                    Gem_SO chippedSapphire = GetGem(gemsDB, GemType.Sapphire, GemQuality.Chipped);
                    if (chippedSapphire != null)
                        rune.craftingGems.Add(chippedSapphire);
                }
                break;
                
            case RunesEn.Dol:
                rune.runeLevel = 31;
                rune.weaponBonus = "Hit Causes Monster to Flee 25%";
                rune.armorBonus = "Replenish Life +7";
                rune.helmetBonus = "Replenish Life +7";
                rune.shieldBonus = "Replenish Life +7";
                // 3x Shael + Chipped Ruby = Dol
                rune.craftingRunes.Add(RunesEn.Shael);
                rune.craftingRunes.Add(RunesEn.Shael);
                rune.craftingRunes.Add(RunesEn.Shael);
                if (gemsDB != null)
                {
                    Gem_SO chippedRuby = GetGem(gemsDB, GemType.Ruby, GemQuality.Chipped);
                    if (chippedRuby != null)
                        rune.craftingGems.Add(chippedRuby);
                }
                break;
                
            case RunesEn.Hel:
                rune.runeLevel = 33;
                rune.weaponBonus = "Requirements -20%";
                rune.armorBonus = "Requirements -15%";
                rune.helmetBonus = "Requirements -15%";
                rune.shieldBonus = "Requirements -15%";
                // 3x Dol + Chipped Emerald = Hel
                rune.craftingRunes.Add(RunesEn.Dol);
                rune.craftingRunes.Add(RunesEn.Dol);
                rune.craftingRunes.Add(RunesEn.Dol);
                if (gemsDB != null)
                {
                    Gem_SO chippedEmerald = GetGem(gemsDB, GemType.Emerald, GemQuality.Chipped);
                    if (chippedEmerald != null)
                        rune.craftingGems.Add(chippedEmerald);
                }
                break;
                
            case RunesEn.Io:
                rune.runeLevel = 35;
                rune.weaponBonus = "+10 to Vitality";
                rune.armorBonus = "+10 to Vitality";
                rune.helmetBonus = "+10 to Vitality";
                rune.shieldBonus = "+10 to Vitality";
                // 3x Hel + Chipped Diamond = Io
                rune.craftingRunes.Add(RunesEn.Hel);
                rune.craftingRunes.Add(RunesEn.Hel);
                rune.craftingRunes.Add(RunesEn.Hel);
                if (gemsDB != null)
                {
                    Gem_SO chippedDiamond = GetGem(gemsDB, GemType.Diamond, GemQuality.Chipped);
                    if (chippedDiamond != null)
                        rune.craftingGems.Add(chippedDiamond);
                }
                break;
                
            case RunesEn.Lum:
                rune.runeLevel = 37;
                rune.weaponBonus = "+10 to Energy";
                rune.armorBonus = "+10 to Energy";
                rune.helmetBonus = "+10 to Energy";
                rune.shieldBonus = "+10 to Energy";
                // 3x Io + Flawed Topaz = Lum
                rune.craftingRunes.Add(RunesEn.Io);
                rune.craftingRunes.Add(RunesEn.Io);
                rune.craftingRunes.Add(RunesEn.Io);
                if (gemsDB != null)
                {
                    Gem_SO flawedTopaz = GetGem(gemsDB, GemType.Topaz, GemQuality.Flawed);
                    if (flawedTopaz != null)
                        rune.craftingGems.Add(flawedTopaz);
                }
                break;
                
            case RunesEn.Ko:
                rune.runeLevel = 39;
                rune.weaponBonus = "+10 to Dexterity";
                rune.armorBonus = "+10 to Dexterity";
                rune.helmetBonus = "+10 to Dexterity";
                rune.shieldBonus = "+10 to Dexterity";
                // 3x Lum + Flawed Amethyst = Ko
                rune.craftingRunes.Add(RunesEn.Lum);
                rune.craftingRunes.Add(RunesEn.Lum);
                rune.craftingRunes.Add(RunesEn.Lum);
                if (gemsDB != null)
                {
                    Gem_SO flawedAmethyst = GetGem(gemsDB, GemType.Amethyst, GemQuality.Flawed);
                    if (flawedAmethyst != null)
                        rune.craftingGems.Add(flawedAmethyst);
                }
                break;
                
            case RunesEn.Fal:
                rune.runeLevel = 41;
                rune.weaponBonus = "+10 to Strength";
                rune.armorBonus = "+10 to Strength";
                rune.helmetBonus = "+10 to Strength";
                rune.shieldBonus = "+10 to Strength";
                // 3x Ko + Flawed Sapphire = Fal
                rune.craftingRunes.Add(RunesEn.Ko);
                rune.craftingRunes.Add(RunesEn.Ko);
                rune.craftingRunes.Add(RunesEn.Ko);
                if (gemsDB != null)
                {
                    Gem_SO flawedSapphire = GetGem(gemsDB, GemType.Sapphire, GemQuality.Flawed);
                    if (flawedSapphire != null)
                        rune.craftingGems.Add(flawedSapphire);
                }
                break;
                
            case RunesEn.Lem:
                rune.runeLevel = 43;
                rune.weaponBonus = "75% Extra Gold from Monsters";
                rune.armorBonus = "50% Extra Gold from Monsters";
                rune.helmetBonus = "50% Extra Gold from Monsters";
                rune.shieldBonus = "50% Extra Gold from Monsters";
                // 3x Fal + Flawed Ruby = Lem
                rune.craftingRunes.Add(RunesEn.Fal);
                rune.craftingRunes.Add(RunesEn.Fal);
                rune.craftingRunes.Add(RunesEn.Fal);
                if (gemsDB != null)
                {
                    Gem_SO flawedRuby = GetGem(gemsDB, GemType.Ruby, GemQuality.Flawed);
                    if (flawedRuby != null)
                        rune.craftingGems.Add(flawedRuby);
                }
                break;
                
            case RunesEn.Pul:
                rune.runeLevel = 45;
                rune.weaponBonus = "+75% Damage to Demons\n+100 to Attack Rating Against Demons";
                rune.armorBonus = "+30% Enhanced Defense";
                rune.helmetBonus = "+30% Enhanced Defense";
                rune.shieldBonus = "+30% Enhanced Defense";
                // 3x Lem + Flawed Emerald = Pul
                rune.craftingRunes.Add(RunesEn.Lem);
                rune.craftingRunes.Add(RunesEn.Lem);
                rune.craftingRunes.Add(RunesEn.Lem);
                if (gemsDB != null)
                {
                    Gem_SO flawedEmerald = GetGem(gemsDB, GemType.Emerald, GemQuality.Flawed);
                    if (flawedEmerald != null)
                        rune.craftingGems.Add(flawedEmerald);
                }
                break;
                
            case RunesEn.Um:
                rune.runeLevel = 47;
                rune.weaponBonus = "25% Chance of Open Wounds";
                rune.armorBonus = "All Resistances +15";
                rune.helmetBonus = "All Resistances +15";
                rune.shieldBonus = "All Resistances +22";
                // 2x Pul + Flawed Diamond = Um
                rune.craftingRunes.Add(RunesEn.Pul);
                rune.craftingRunes.Add(RunesEn.Pul);
                if (gemsDB != null)
                {
                    Gem_SO flawedDiamond = GetGem(gemsDB, GemType.Diamond, GemQuality.Flawed);
                    if (flawedDiamond != null)
                        rune.craftingGems.Add(flawedDiamond);
                }
                break;
                
            case RunesEn.Mal:
                rune.runeLevel = 49;
                rune.weaponBonus = "Prevent Monster Heal";
                rune.armorBonus = "Magic Damage Reduced by 7";
                rune.helmetBonus = "Magic Damage Reduced by 7";
                rune.shieldBonus = "Magic Damage Reduced by 7";
                // 2x Um + Topaz = Mal
                rune.craftingRunes.Add(RunesEn.Um);
                rune.craftingRunes.Add(RunesEn.Um);
                if (gemsDB != null)
                {
                    Gem_SO topaz = GetGem(gemsDB, GemType.Topaz, GemQuality.Normal);
                    if (topaz != null)
                        rune.craftingGems.Add(topaz);
                }
                break;
                
            case RunesEn.Ist:
                rune.runeLevel = 51;
                rune.weaponBonus = "30% Better Chance of Getting Magic Items";
                rune.armorBonus = "25% Better Chance of Getting Magic Items";
                rune.helmetBonus = "25% Better Chance of Getting Magic Items";
                rune.shieldBonus = "25% Better Chance of Getting Magic Items";
                // 2x Mal + Amethyst = Ist
                rune.craftingRunes.Add(RunesEn.Mal);
                rune.craftingRunes.Add(RunesEn.Mal);
                if (gemsDB != null)
                {
                    Gem_SO amethyst = GetGem(gemsDB, GemType.Amethyst, GemQuality.Normal);
                    if (amethyst != null)
                        rune.craftingGems.Add(amethyst);
                }
                break;
                
            case RunesEn.Gul:
                rune.runeLevel = 53;
                rune.weaponBonus = "20% Bonus to Attack Rating";
                rune.armorBonus = "5% to Maximum Poison Resist";
                rune.helmetBonus = "5% to Maximum Poison Resist";
                rune.shieldBonus = "5% to Maximum Poison Resist";
                // 2x Ist + Sapphire = Gul
                rune.craftingRunes.Add(RunesEn.Ist);
                rune.craftingRunes.Add(RunesEn.Ist);
                if (gemsDB != null)
                {
                    Gem_SO sapphire = GetGem(gemsDB, GemType.Sapphire, GemQuality.Normal);
                    if (sapphire != null)
                        rune.craftingGems.Add(sapphire);
                }
                break;
                
            case RunesEn.Vex:
                rune.runeLevel = 55;
                rune.weaponBonus = "7% Mana Stolen per Hit";
                rune.armorBonus = "5% to Maximum Fire Resist";
                rune.helmetBonus = "5% to Maximum Fire Resist";
                rune.shieldBonus = "5% to Maximum Fire Resist";
                // 2x Gul + Ruby = Vex
                rune.craftingRunes.Add(RunesEn.Gul);
                rune.craftingRunes.Add(RunesEn.Gul);
                if (gemsDB != null)
                {
                    Gem_SO ruby = GetGem(gemsDB, GemType.Ruby, GemQuality.Normal);
                    if (ruby != null)
                        rune.craftingGems.Add(ruby);
                }
                break;
                
            case RunesEn.Ohm:
                rune.runeLevel = 57;
                rune.weaponBonus = "+50% Enhanced Damage";
                rune.armorBonus = "5% to Maximum Cold Resist";
                rune.helmetBonus = "5% to Maximum Cold Resist";
                rune.shieldBonus = "5% to Maximum Cold Resist";
                // 2x Vex + Emerald = Ohm
                rune.craftingRunes.Add(RunesEn.Vex);
                rune.craftingRunes.Add(RunesEn.Vex);
                if (gemsDB != null)
                {
                    Gem_SO emerald = GetGem(gemsDB, GemType.Emerald, GemQuality.Normal);
                    if (emerald != null)
                        rune.craftingGems.Add(emerald);
                }
                break;
                
            case RunesEn.Lo:
                rune.runeLevel = 59;
                rune.weaponBonus = "20% Deadly Strike";
                rune.armorBonus = "5% to Maximum Lightning Resist";
                rune.helmetBonus = "5% to Maximum Lightning Resist";
                rune.shieldBonus = "5% to Maximum Lightning Resist";
                // 2x Ohm + Diamond = Lo
                rune.craftingRunes.Add(RunesEn.Ohm);
                rune.craftingRunes.Add(RunesEn.Ohm);
                if (gemsDB != null)
                {
                    Gem_SO diamond = GetGem(gemsDB, GemType.Diamond, GemQuality.Normal);
                    if (diamond != null)
                        rune.craftingGems.Add(diamond);
                }
                break;
                
            case RunesEn.Sur:
                rune.runeLevel = 61;
                rune.weaponBonus = "Hit Blinds Target";
                rune.armorBonus = "Maximum Mana 5%";
                rune.helmetBonus = "Maximum Mana 5%";
                rune.shieldBonus = "+50 to Mana";
                // 2x Lo + Flawless Topaz = Sur
                rune.craftingRunes.Add(RunesEn.Lo);
                rune.craftingRunes.Add(RunesEn.Lo);
                if (gemsDB != null)
                {
                    Gem_SO flawlessTopaz = GetGem(gemsDB, GemType.Topaz, GemQuality.Flawless);
                    if (flawlessTopaz != null)
                        rune.craftingGems.Add(flawlessTopaz);
                }
                break;
                
            case RunesEn.Ber:
                rune.runeLevel = 63;
                rune.weaponBonus = "20% Chance of Crushing Blow";
                rune.armorBonus = "Damage Reduced by 8%";
                rune.helmetBonus = "Damage Reduced by 8%";
                rune.shieldBonus = "Damage Reduced by 8%";
                // 2x Sur + Flawless Amethyst = Ber
                rune.craftingRunes.Add(RunesEn.Sur);
                rune.craftingRunes.Add(RunesEn.Sur);
                if (gemsDB != null)
                {
                    Gem_SO flawlessAmethyst = GetGem(gemsDB, GemType.Amethyst, GemQuality.Flawless);
                    if (flawlessAmethyst != null)
                        rune.craftingGems.Add(flawlessAmethyst);
                }
                break;
                
            case RunesEn.Jah:
                rune.runeLevel = 65;
                rune.weaponBonus = "Ignore Target's Defense";
                rune.armorBonus = "Increase Maximum Life 5%";
                rune.helmetBonus = "Increase Maximum Life 5%";
                rune.shieldBonus = "+50 to Life";
                // 2x Ber + Flawless Sapphire = Jah
                rune.craftingRunes.Add(RunesEn.Ber);
                rune.craftingRunes.Add(RunesEn.Ber);
                if (gemsDB != null)
                {
                    Gem_SO flawlessSapphire = GetGem(gemsDB, GemType.Sapphire, GemQuality.Flawless);
                    if (flawlessSapphire != null)
                        rune.craftingGems.Add(flawlessSapphire);
                }
                break;
                
            case RunesEn.Cham:
                rune.runeLevel = 67;
                rune.weaponBonus = "Freezes Target +3";
                rune.armorBonus = "Cannot be Frozen";
                rune.helmetBonus = "Cannot be Frozen";
                rune.shieldBonus = "Cannot be Frozen";
                // 2x Jah + Flawless Ruby = Cham
                rune.craftingRunes.Add(RunesEn.Jah);
                rune.craftingRunes.Add(RunesEn.Jah);
                if (gemsDB != null)
                {
                    Gem_SO flawlessRuby = GetGem(gemsDB, GemType.Ruby, GemQuality.Flawless);
                    if (flawlessRuby != null)
                        rune.craftingGems.Add(flawlessRuby);
                }
                break;
                
            case RunesEn.Zod:
                rune.runeLevel = 69;
                rune.weaponBonus = "Indestructible";
                rune.armorBonus = "Indestructible";
                rune.helmetBonus = "Indestructible";
                rune.shieldBonus = "Indestructible";
                // 2x Cham + Flawless Emerald = Zod
                rune.craftingRunes.Add(RunesEn.Cham);
                rune.craftingRunes.Add(RunesEn.Cham);
                if (gemsDB != null)
                {
                    Gem_SO flawlessEmerald = GetGem(gemsDB, GemType.Emerald, GemQuality.Flawless);
                    if (flawlessEmerald != null)
                        rune.craftingGems.Add(flawlessEmerald);
                }
                break;
                
            default:
                Debug.Log($"No data available for rune: {rune.runeType}");
                break;
        }
        
        EditorUtility.SetDirty(rune);
        Debug.Log($"Filled data for rune: {rune.runeType}");
    }
    
    // Статический метод для вызова из Rune_SO.OnValidate()
    public static void FillRuneDataStatic(Rune_SO rune)
    {
        // Получаем базу данных камней
        GemsDB_SO gemsDB = GetGemsDatabaseStatic();
        
        // Очищаем существующие рецепты
        rune.craftingRunes.Clear();
        rune.craftingGems.Clear();
        
        // Заполняем данные в зависимости от типа руны
        FillRuneDataInternal(rune, gemsDB);
        
        EditorUtility.SetDirty(rune);
    }
    
    private static GemsDB_SO GetGemsDatabaseStatic()
    {
        // Ищем базу данных камней в проекте
        string[] guids = AssetDatabase.FindAssets("t:GemsDB_SO");
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<GemsDB_SO>(path);
        }
        return null;
    }
    
    private static void FillRuneDataInternal(Rune_SO rune, GemsDB_SO gemsDB)
    {
        // Здесь копируем всю логику заполнения данных из FillRuneData
        switch (rune.runeType)
        {
            case RunesEn.El:
                rune.runeLevel = 11;
                rune.weaponBonus = "+50 to Attack Rating\n+1 to Light Radius";
                rune.armorBonus = "+15 Defense\n+1 to Light Radius";
                rune.helmetBonus = "+15 Defense\n+1 to Light Radius";
                rune.shieldBonus = "+15 Defense\n+1 to Light Radius";
                // El не крафтится
                break;
                
            case RunesEn.Eld:
                rune.runeLevel = 11;
                rune.weaponBonus = "+75% Damage to Undead\n+50 to Attack Rating Against Undead";
                rune.armorBonus = "15% Slower Stamina Drain\n+1 to Light Radius";
                rune.helmetBonus = "15% Slower Stamina Drain\n+1 to Light Radius";
                rune.shieldBonus = "7% Increased Chance of Blocking\n+1 to Light Radius";
                // 3x El = Eld
                rune.craftingRunes.Add(RunesEn.El);
                rune.craftingRunes.Add(RunesEn.El);
                rune.craftingRunes.Add(RunesEn.El);
                break;
                
            case RunesEn.Tir:
                rune.runeLevel = 13;
                rune.weaponBonus = "+2 to Mana after each Kill";
                rune.armorBonus = "+2 to Mana after each Kill";
                rune.helmetBonus = "+2 to Mana after each Kill";
                rune.shieldBonus = "+2 to Mana after each Kill";
                // 3x Eld = Tir
                rune.craftingRunes.Add(RunesEn.Eld);
                rune.craftingRunes.Add(RunesEn.Eld);
                rune.craftingRunes.Add(RunesEn.Eld);
                break;
                
            case RunesEn.Nef:
                rune.runeLevel = 13;
                rune.weaponBonus = "Knockback";
                rune.armorBonus = "+30 Defense vs. Missile";
                rune.helmetBonus = "+30 Defense vs. Missile";
                rune.shieldBonus = "+30 Defense vs. Missile";
                // 3x Tir = Nef
                rune.craftingRunes.Add(RunesEn.Tir);
                rune.craftingRunes.Add(RunesEn.Tir);
                rune.craftingRunes.Add(RunesEn.Tir);
                break;
                
            case RunesEn.Eth:
                rune.runeLevel = 15;
                rune.weaponBonus = "-25% Target Defense";
                rune.armorBonus = "Regenerate Mana 15%";
                rune.helmetBonus = "Regenerate Mana 15%";
                rune.shieldBonus = "Regenerate Mana 15%";
                // 3x Nef = Eth
                rune.craftingRunes.Add(RunesEn.Nef);
                rune.craftingRunes.Add(RunesEn.Nef);
                rune.craftingRunes.Add(RunesEn.Nef);
                break;
                
            case RunesEn.Ith:
                rune.runeLevel = 15;
                rune.weaponBonus = "+9 to Maximum Damage";
                rune.armorBonus = "15% Damage Taken Goes to Mana";
                rune.helmetBonus = "15% Damage Taken Goes to Mana";
                rune.shieldBonus = "15% Damage Taken Goes to Mana";
                // 3x Eth = Ith
                rune.craftingRunes.Add(RunesEn.Eth);
                rune.craftingRunes.Add(RunesEn.Eth);
                rune.craftingRunes.Add(RunesEn.Eth);
                break;
                
            case RunesEn.Tal:
                rune.runeLevel = 17;
                rune.weaponBonus = "+75 Poison Damage Over 5 Seconds";
                rune.armorBonus = "Poison Resist +30%";
                rune.helmetBonus = "Poison Resist +30%";
                rune.shieldBonus = "Poison Resist +35%";
                // 3x Ith = Tal
                rune.craftingRunes.Add(RunesEn.Ith);
                rune.craftingRunes.Add(RunesEn.Ith);
                rune.craftingRunes.Add(RunesEn.Ith);
                break;
                
            case RunesEn.Ral:
                rune.runeLevel = 19;
                rune.weaponBonus = "Adds 5-30 Fire Damage";
                rune.armorBonus = "Fire Resist +30%";
                rune.helmetBonus = "Fire Resist +30%";
                rune.shieldBonus = "Fire Resist +35%";
                // 3x Tal = Ral
                rune.craftingRunes.Add(RunesEn.Tal);
                rune.craftingRunes.Add(RunesEn.Tal);
                rune.craftingRunes.Add(RunesEn.Tal);
                break;
                
            case RunesEn.Ort:
                rune.runeLevel = 21;
                rune.weaponBonus = "Adds 1-50 Lightning Damage";
                rune.armorBonus = "Lightning Resist +30%";
                rune.helmetBonus = "Lightning Resist +30%";
                rune.shieldBonus = "Lightning Resist +35%";
                // 3x Ral = Ort
                rune.craftingRunes.Add(RunesEn.Ral);
                rune.craftingRunes.Add(RunesEn.Ral);
                rune.craftingRunes.Add(RunesEn.Ral);
                break;
                
            case RunesEn.Thul:
                rune.runeLevel = 23;
                rune.weaponBonus = "Adds 3-14 Cold Damage";
                rune.armorBonus = "Cold Resist +30%";
                rune.helmetBonus = "Cold Resist +30%";
                rune.shieldBonus = "Cold Resist +35%";
                // 3x Ort = Thul
                rune.craftingRunes.Add(RunesEn.Ort);
                rune.craftingRunes.Add(RunesEn.Ort);
                rune.craftingRunes.Add(RunesEn.Ort);
                break;
                
            case RunesEn.Amn:
                rune.runeLevel = 25;
                rune.weaponBonus = "7% Life Stolen per Hit";
                rune.armorBonus = "Attacker Takes Damage of 14";
                rune.helmetBonus = "Attacker Takes Damage of 14";
                rune.shieldBonus = "Attacker Takes Damage of 14";
                // 3x Thul + Chipped Topaz = Amn
                rune.craftingRunes.Add(RunesEn.Thul);
                rune.craftingRunes.Add(RunesEn.Thul);
                rune.craftingRunes.Add(RunesEn.Thul);
                if (gemsDB != null)
                {
                    Gem_SO chippedTopaz = GetGemStatic(gemsDB, GemType.Topaz, GemQuality.Chipped);
                    if (chippedTopaz != null)
                        rune.craftingGems.Add(chippedTopaz);
                }
                break;
                
            default:
                // Для остальных рун пока оставляем пустым
                break;
        }
    }
    
    private static Gem_SO GetGemStatic(GemsDB_SO gemsDB, GemType gemType, GemQuality gemQuality)
    {
        if (gemsDB == null) return null;
        
        foreach (var gem in gemsDB.gems)
        {
            if (gem.gemType == gemType && gem.gemQuality == gemQuality)
                return gem;
        }
        return null;
    }
    
    private GemsDB_SO GetGemsDatabase()
    {
        // Ищем базу данных камней в проекте
        string[] guids = AssetDatabase.FindAssets("t:GemsDB_SO");
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<GemsDB_SO>(path);
        }
        Debug.LogWarning("GemsDB_SO not found! Please create a Gems Database.");
        return null;
    }
    
    private Gem_SO GetGem(GemsDB_SO gemsDB, GemType gemType, GemQuality gemQuality)
    {
        if (gemsDB == null) return null;
        
        foreach (var gem in gemsDB.gems)
        {
            if (gem.gemType == gemType && gem.gemQuality == gemQuality)
            {
                Debug.Log($"Найден камень: {gemType} {gemQuality}");
                return gem;
            }
        }
        
        Debug.LogWarning($"Камень не найден: {gemType} {gemQuality}");
        return null;
    }
} 