Friend Class Character
    Inherits CharacterDataClient
    Implements ICharacter

    Public Sub New(worldData As WorldData, value As Integer)
        MyBase.New(worldData, value)
    End Sub

    Public ReadOnly Property Id As Integer Implements ICharacter.Id
        Get
            Return CharacterId
        End Get
    End Property
End Class
