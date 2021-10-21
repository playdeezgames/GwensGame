Module Scenes
    Private _scenes As New Dictionary(Of Integer, Dictionary(Of Integer, IScene)) From
    {
        {0, New Dictionary(Of Integer, IScene) From
            {
                {0, New HomeScene()}
            }}
    }
    Private _defaultScene As IScene = New DefaultScene()
    Public Function GetScene(x As Integer, y As Integer) As IScene
        Dim sceneColumn As New Dictionary(Of Integer, IScene)
        If _scenes.TryGetValue(x, sceneColumn) Then
            If sceneColumn.ContainsKey(y) Then
                Return sceneColumn(y)
            End If
        End If
        Return _defaultScene
    End Function
End Module
