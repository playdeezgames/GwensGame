Public Class GPConfig
    Private Const Filename = "config.json"
    Public Property Volume As Single
    Public Property Scale As Integer
    Public Property FullScreen As Boolean
    Public Shared Function Load() As GPConfig
        Try
            Return JsonSerializer.Deserialize(Of GPConfig)(File.ReadAllText(Filename))
        Catch ex As Exception
            Return New GPConfig With
                {
                    .FullScreen = False,
                    .Scale = 1,
                    .Volume = 0.5
                }
        End Try
    End Function
    Public Sub Save()
        Try
            File.WriteAllText(Filename, JsonSerializer.Serialize(Me))
        Catch ex As Exception
            'nom
        End Try
    End Sub
End Class
