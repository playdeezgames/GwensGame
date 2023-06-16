Imports SPLORR.Game
Imports System.Runtime.CompilerServices

Public Module CharacterShortcut
    <Extension>
    Public Sub GenerateShortcut(character As ICharacter)
        character.SetStatistic(StatisticType.Shortcut, If(RNG.RollDice("1d4") = 1, RNG.RollDice("1d11+-1d11"), 0))
    End Sub
    <Extension>
    Public Function HasShortcut(character As ICharacter) As Boolean
        Return character.GetStatistic(StatisticType.Shortcut) <> 0
    End Function
    <Extension>
    Public Function Shortcut(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType.Shortcut)
    End Function
    <Extension>
    Public Sub TakeShortcut(character As ICharacter)
        character.DoVerb(VerbType.TakeShortcut)
    End Sub
End Module
