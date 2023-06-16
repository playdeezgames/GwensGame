Imports System.Runtime.CompilerServices
Imports SPLORR.Game

Public Module CharacterDistanceRemaining
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
        character.GenerateForage()
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
End Module
