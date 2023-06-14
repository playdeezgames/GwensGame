Public Interface IWorld
    Sub Save(filename As String)
    Function CreateCharacter() As ICharacter
    Property Avatar As ICharacter
End Interface
