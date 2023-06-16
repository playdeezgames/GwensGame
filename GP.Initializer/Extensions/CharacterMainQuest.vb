Imports System.Runtime.CompilerServices
Imports SPLORR.Game

Public Module CharacterMainQuest
    <Extension>
    Public Function DistanceRemaining(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.DistanceRemaining)
    End Function
    <Extension>
    Public Sub SetDistanceRemaining(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticTypes.DistanceRemaining, Math.Max(value, 0))
    End Sub
    <Extension>
    Public Sub KeepGoing(character As ICharacter, value As Integer)
        character.SetDistanceRemaining(character.DistanceRemaining - value)
        ApplyHunger(character, value)
        RefreshLocalArea(character)
    End Sub

    Private Sub RefreshLocalArea(character As ICharacter)
        character.GenerateForage()
        character.GenerateShortcut()
    End Sub

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
    Public Sub GenerateShortcut(character As ICharacter)
        character.SetStatistic(StatisticTypes.Shortcut, If(RNG.RollDice("1d4") = 1, RNG.RollDice("1d11+-1d11"), 0))
    End Sub
    <Extension>
    Public Function HasShortcut(character As ICharacter) As Boolean
        Return character.GetStatistic(StatisticTypes.Shortcut) <> 0
    End Function
    <Extension>
    Public Function Shortcut(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.Shortcut)
    End Function
    <Extension>
    Public Sub TakeShortcut(character As ICharacter)
        character.SetDistanceRemaining(character.DistanceRemaining + character.Shortcut)
        RefreshLocalArea(character)
    End Sub
End Module
