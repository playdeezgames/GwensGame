Imports Microsoft.Data.Sqlite

Module Data
    Const playerId As Integer = 1
    Private connection As SqliteConnection
    Public Function GetX() As Integer
        Dim command As SqliteCommand = connection.CreateCommand()
        command.CommandText = "SELECT [X] FROM [Players] WHERE [PlayerId]=$PlayerId;"
        command.Parameters.AddWithValue("$PlayerId", playerId)
        Dim reader As SqliteDataReader = command.ExecuteReader()
        reader.Read()
        Return reader("X")
    End Function
    Public Function GetY() As Integer
        Dim command As SqliteCommand = connection.CreateCommand()
        command.CommandText = "SELECT [Y] FROM [Players] WHERE [PlayerId]=$PlayerId;"
        command.Parameters.AddWithValue("$PlayerId", playerId)
        Dim reader As SqliteDataReader = command.ExecuteReader()
        reader.Read()
        Return reader("Y")
    End Function
    Public Sub SetXY(x As Integer, y As Integer)
        Dim command As SqliteCommand = connection.CreateCommand()
        command.CommandText = "UPDATE [Players] SET [X]=$X, [Y]=$Y WHERE [PlayerId]=$PlayerId;"
        command.Parameters.AddWithValue("$PlayerId", playerId)
        command.Parameters.AddWithValue("$X", x)
        command.Parameters.AddWithValue("$Y", y)
        command.ExecuteNonQuery()
    End Sub
    Public Function GetFacing() As Direction
        Dim command As SqliteCommand = connection.CreateCommand()
        command.CommandText = "SELECT [Facing] FROM [Players] WHERE [PlayerId]=$PlayerId;"
        command.Parameters.AddWithValue("$PlayerId", playerId)
        Dim reader As SqliteDataReader = command.ExecuteReader()
        reader.Read()
        Return reader("Facing")
    End Function
    Public Sub SetFacing(facing As Direction)
        Dim command As SqliteCommand = connection.CreateCommand()
        command.CommandText = "UPDATE [Players] SET [Facing]=$FacingWHERE [PlayerId]=$PlayerId;"
        command.Parameters.AddWithValue("$PlayerId", playerId)
        command.Parameters.AddWithValue("$Facing", facing)
        command.ExecuteNonQuery()
    End Sub
    Public Sub CloseConnection()
        If connection IsNot Nothing Then
            connection.Close()
            connection = Nothing
        End If
    End Sub
    Private Sub OpenConnection()
        CloseConnection()
        connection = New SqliteConnection("Data Source=:memory:")
        connection.Open()
    End Sub
    Private Sub ScaffoldData()
        Dim command As SqliteCommand = connection.CreateCommand()
        command.CommandText = "CREATE TABLE IF NOT EXISTS [Players]([PlayerId] INT NOT NULL, [X] INT NOT NULL,[Y] INT NOT NULL,[Facing] INT);"
        command.ExecuteNonQuery()

        command = connection.CreateCommand()
        command.CommandText = "REPLACE INTO [Players]([PlayerId],[X],[Y],[Facing]) VALUES ($PlayerId, $X, $Y, $Facing);"
        command.Parameters.AddWithValue("$PlayerId", playerId)
        command.Parameters.AddWithValue("$X", 0)
        command.Parameters.AddWithValue("$Y", 0)
        command.Parameters.AddWithValue("$Facing", 0)
        command.ExecuteNonQuery()
    End Sub
    Public Sub ResetData()
        OpenConnection()
        ScaffoldData()
    End Sub
    Public Function IsInPlay() As Boolean
        Return connection IsNot Nothing AndAlso connection.State = System.Data.ConnectionState.Open
    End Function
End Module
