﻿Imports System.Runtime.CompilerServices

Public Module CharacterHealth
    <Extension>
    Public Function Health(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType.Health)
    End Function
    <Extension>
    Public Sub SetHealth(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticType.Health, Math.Clamp(value, 0, character.MaximumHealth))
    End Sub
    <Extension>
    Public Function MaximumHealth(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType.MaximumHealth)
    End Function
    <Extension>
    Public Function IsDead(character As ICharacter) As Boolean
        Return character.Health = 0
    End Function
End Module
