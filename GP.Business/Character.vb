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

    Public ReadOnly Property World As IWorld Implements ICharacter.World
        Get
            Return New World(WorldData)
        End Get
    End Property

    Public Sub SetStatistic(statisticType As String, statisticValue As Integer) Implements ICharacter.SetStatistic
        CharacterData.Statistics(statisticType) = statisticValue
    End Sub

    Public Sub AddItem(item As IItem) Implements ICharacter.AddItem
        Throw New NotImplementedException()
    End Sub

    Public Function GetStatistic(statisticType As String) As Integer Implements ICharacter.GetStatistic
        Return CharacterData.Statistics(statisticType)
    End Function
End Class
