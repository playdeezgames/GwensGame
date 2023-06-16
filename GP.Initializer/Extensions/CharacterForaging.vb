﻿Imports SPLORR.Game
Imports System.Runtime.CompilerServices

Public Module CharacterForaging
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
