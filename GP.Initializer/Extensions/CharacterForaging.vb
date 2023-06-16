Imports SPLORR.Game
Imports System.Runtime.CompilerServices

Public Module CharacterForaging
    <Extension>
    Public Sub SetForagingSkill(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticType.ForagingSkill, value)
    End Sub
    <Extension>
    Public Sub GenerateForage(character As ICharacter)
        character.SetStatistic(StatisticType.ForagingDifficulty, RNG.RollDice("1d50+-1d50"))
        character.SetStatistic(StatisticType.ForagingAbundance, RNG.RollDice("2d3+-2d1"))
    End Sub
    <Extension>
    Public Function CanForage(character As ICharacter) As Boolean
        Return character.GetStatistic(StatisticType.ForagingAbundance) > 0
    End Function
    <Extension>
    Public Function ForagingDifficulty(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType.ForagingDifficulty)
    End Function
    <Extension>
    Public Function ForagingSkill(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType.ForagingSkill)
    End Function
    <Extension>
    Public Function ForagingAbundance(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType.ForagingAbundance)
    End Function
    <Extension>
    Public Sub SetForagingAbundance(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticType.ForagingAbundance, Math.Max(value, 0))
    End Sub
    <Extension>
    Public Function Forage(character As ICharacter) As Boolean
        Return character.DoVerb(VerbType.Forage)
    End Function
End Module
