Imports System.Runtime.CompilerServices

Public Module CharacterHealth
    <Extension>
    Public Function Health(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.Health)
    End Function
    <Extension>
    Public Sub SetHealth(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticTypes.Health, Math.Clamp(value, 0, character.MaximumHealth))
    End Sub
    <Extension>
    Public Function MaximumHealth(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticTypes.MaximumHealth)
    End Function
    <Extension>
    Public Function IsDead(character As ICharacter) As Boolean
        Return character.Health = 0
    End Function
End Module
