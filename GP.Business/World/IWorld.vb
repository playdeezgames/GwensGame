Public Interface IWorld
    Sub Save(filename As String)
    Function CreateCharacter() As ICharacter
    Function CreateItem(itemType As String) As IItem
    Property Avatar As ICharacter
End Interface
