Imports System.Runtime.CompilerServices

Public Module CharacterSatiety
    <Extension>
    Public Function Satiety(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType.Satiety)
    End Function
    <Extension>
    Public Sub SetSatiety(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticType.Satiety, Math.Clamp(value, 0, character.MaximumSatiety))
    End Sub
    <Extension>
    Public Function MaximumSatiety(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType.MaximumSatiety)
    End Function
    <Extension>
    Friend Sub ApplyHunger(character As ICharacter, value As Integer)
        If value > character.Satiety Then
            value -= character.Satiety
            character.SetSatiety(0)
            character.SetHealth(character.Health - value)
        Else
            character.SetSatiety(character.Satiety - value)
        End If
    End Sub
End Module
