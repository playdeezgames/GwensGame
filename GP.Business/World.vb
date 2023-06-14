Public Class World
    Inherits WorldDataClient
    Implements IWorld
    Friend Sub New(data As WorldData)
        MyBase.New(data)
    End Sub
    Public Shared Function Create() As IWorld
        Dim data = New WorldData
        Return New World(data)
    End Function
    Public Shared Function Load(filename As String) As IWorld
        Throw New NotImplementedException
    End Function
End Class
