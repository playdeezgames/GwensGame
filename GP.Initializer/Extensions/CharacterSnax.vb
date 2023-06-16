Imports System.Runtime.CompilerServices

Public Module CharacterSnax
    <Extension>
    Public Function Snax(character As ICharacter) As Integer
        Return character.Items.Count(Function(x) x.ItemType = ItemType.Snax)
    End Function
    <Extension>
    Public Sub SetSnax(character As ICharacter, value As Integer)
        value -= character.Snax
        If value < 0 Then
            Dim items = character.Items.Where(Function(x) x.ItemType = ItemType.Snax).Take(-value)
            For Each item In items
                character.RemoveItem(item)
                item.Destroy()
            Next
        End If
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
