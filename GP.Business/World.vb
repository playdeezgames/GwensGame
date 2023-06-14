Imports System.IO

Public Class World
    Inherits WorldDataClient
    Implements IWorld
    Friend Sub New(data As WorldData)
        MyBase.New(data)
    End Sub
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
End Class
