Imports System.IO

Public Class World
    Inherits WorldDataClient
    Implements IWorld
    Friend Sub New(data As WorldData)
        MyBase.New(data)
    End Sub

    Public Property Avatar As ICharacter Implements IWorld.Avatar
        Get
            Return If(WorldData.AvatarCharacterId.HasValue, New Character(WorldData, WorldData.AvatarCharacterId.Value), Nothing)
        End Get
        Set(value As ICharacter)
            WorldData.AvatarCharacterId = value?.Id
        End Set
    End Property

    Public Sub Save(filename As String) Implements IWorld.Save
        File.WriteAllText(filename, JsonSerializer.Serialize(WorldData))
    End Sub
    Public Shared Function Create() As IWorld
        Return New World(New WorldData)
    End Function
    Public Shared Function Load(filename As String) As IWorld
        Try
            Return New World(JsonSerializer.Deserialize(Of WorldData)(File.ReadAllText(filename)))
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CreateCharacter() As ICharacter Implements IWorld.CreateCharacter
        Dim characterId = WorldData.Characters.Count
        WorldData.Characters.Add(New CharacterData)
        Return New Character(WorldData, characterId)
    End Function
End Class
