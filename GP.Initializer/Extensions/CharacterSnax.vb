Imports System.Runtime.CompilerServices

Public Module CharacterSnax
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
End Module
