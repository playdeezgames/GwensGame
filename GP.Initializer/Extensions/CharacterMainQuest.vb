Imports System.Runtime.CompilerServices

Public Module CharacterMainQuest
    <Extension>
    Public Function DistanceRemaining(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType.DistanceRemaining)
    End Function
    <Extension>
    Public Sub SetDistanceRemaining(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticType.DistanceRemaining, Math.Max(value, 0))
    End Sub
    <Extension>
    Public Sub KeepGoing(character As ICharacter, value As Integer)
        character.SetDistanceRemaining(character.DistanceRemaining - value)
        ApplyHunger(character, value)
        RefreshLocalArea(character)
    End Sub

    Friend Sub RefreshLocalArea(character As ICharacter)
        character.GenerateForage()
        character.GenerateShortcut()
    End Sub

    <Extension>
    Public Function HasWon(character As ICharacter) As Boolean
        Return character.DistanceRemaining = 0
    End Function
    <Extension>
    Public Function Pace(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType.Pace)
    End Function
    <Extension>
    Public Sub SetPace(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticType.Pace, Math.Clamp(value, MinimumPace, MaximumPace))
    End Sub
End Module
