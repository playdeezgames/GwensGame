Imports System.Runtime.CompilerServices
Imports SPLORR.Game

Public Module CharacterExtensions
    <Extension>
    Public Function DistanceRemaining(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.DistanceRemaining)
    End Function
    <Extension>
    Public Function Satiety(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.Satiety)
    End Function
    <Extension>
    Public Sub SetSatiety(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticTypes.Satiety, Math.Clamp(value, 0, character.MaximumSatiety))
    End Sub
    <Extension>
    Public Function Health(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.Health)
    End Function
    <Extension>
    Public Sub SetHealth(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticTypes.Health, Math.Clamp(value, 0, character.MaximumHealth))
    End Sub
    <Extension>
    Public Function MaximumSatiety(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.MaximumSatiety)
    End Function
    <Extension>
    Public Function MaximumHealth(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.MaximumHealth)
    End Function
    <Extension>
    Public Sub SetDistanceRemaining(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticTypes.DistanceRemaining, Math.Max(value, 0))
    End Sub
    <Extension>
    Public Sub KeepGoing(character As ICharacter, value As Integer)
        character.SetDistanceRemaining(character.DistanceRemaining - value)
        ApplyHunger(character, value)
        character.GenerateForage()
    End Sub
    <Extension>
    Friend Sub ApplyHunger(character As ICharacter, value As Integer)
        If value > character.Satiety Then
            value -= character.Satiety
            character.SetSatiety(0)
            character.SetHealth(character.Health - value)
        Else
            character.SetSatiety(character.Satiety - value)
        End If
    End Sub

    <Extension>
    Public Function IsDead(character As ICharacter) As Boolean
        Return character.Health = 0
    End Function
    <Extension>
    Public Function HasWon(character As ICharacter) As Boolean
        Return character.DistanceRemaining = 0
    End Function
    <Extension>
    Public Function Pace(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.Pace)
    End Function
    <Extension>
    Public Sub SetPace(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticTypes.Pace, Math.Clamp(value, MinimumPace, MaximumPace))
    End Sub
    <Extension>
    Public Function Snax(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.Snax)
    End Function
    <Extension>
    Public Sub SetSnax(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticTypes.Snax, Math.Max(value, 0))
    End Sub
    <Extension>
    Public Sub EatSnax(character As ICharacter)
        character.SetSnax(character.Snax - 1)
        character.SetSatiety(character.Satiety + SnaxSatietyBenefit)
    End Sub
    <Extension>
    Public Sub SetForagingSkill(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticTypes.ForagingSkill, value)
    End Sub
    <Extension>
    Public Sub GenerateForage(character As ICharacter)
        character.SetStatistic(StatisticTypes.ForagingDifficulty, RNG.RollDice("1d50+-1d50"))
        character.SetStatistic(StatisticTypes.ForagingAbundance, RNG.RollDice("2d3+-2d1"))
    End Sub
    <Extension>
    Public Function CanForage(character As ICharacter) As Boolean
        Return character.GetStatistic(StatisticTypes.ForagingAbundance) > 0
    End Function
    <Extension>
    Public Function ForagingDifficulty(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.ForagingDifficulty)
    End Function
    <Extension>
    Public Function ForagingSkill(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.ForagingSkill)
    End Function
    <Extension>
    Public Function ForagingAbundance(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.ForagingAbundance)
    End Function
    <Extension>
    Public Sub SetForagingAbundance(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticTypes.ForagingAbundance, Math.Max(value, 0))
    End Sub
    <Extension>
    Public Function Forage(character As ICharacter) As Boolean
        If Not character.CanForage Then
            Return False
        End If
        character.ApplyHunger(1)
        Dim roll = RNG.RollDice("1d100") + character.ForagingDifficulty
        If roll <= character.ForagingSkill Then
            character.SetForagingAbundance(character.ForagingAbundance - 1)
            character.SetSnax(character.Snax + 1)
            Return True
        End If
        Return False
    End Function
End Module
