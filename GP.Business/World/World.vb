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

    Public Function CreateItem(itemType As String) As IItem Implements IWorld.CreateItem
        Dim itemData = New ItemData With
                            {
                                .ItemType = itemType,
                                .Destroyed = False
                            }
        Dim itemId = WorldData.Items.FindIndex(Function(x) x.Destroyed)
        If itemId > -1 Then
            WorldData.Items(itemId) = itemData
        Else
            itemId = WorldData.Items.Count
            WorldData.Items.Add(itemData)
        End If
        Return New Item(WorldData, itemId)
    End Function
End Class
