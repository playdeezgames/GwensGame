Imports System.Runtime.CompilerServices

Public Module CharacterSnax
    <Extension>
    Public Function Snax(character As ICharacter) As Integer
        Return character.GetStatistic(StatisticType.Snax)
    End Function
    <Extension>
    Public Sub SetSnax(character As ICharacter, value As Integer)
        character.SetStatistic(StatisticType.Snax, Math.Max(value, 0))
        While value > 0
            character.AddItem(character.World.CreateItem(ItemType.Snax))
            value -= 1
        End While
    End Sub
    <Extension>
    Public Sub EatSnax(character As ICharacter)
        character.DoVerb(VerbType.Eat, (StatisticType.Satiety, SnaxSatietyBenefit))
    End Sub
End Module
