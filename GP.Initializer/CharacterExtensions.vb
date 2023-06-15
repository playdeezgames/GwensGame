Imports System.Runtime.CompilerServices

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
End Module
