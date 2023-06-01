Friend Module Context
    Private ReadOnly _flags As New HashSet(Of String)
    Friend Property Flag(flagName As String) As Boolean
        Get
            Return _flags.Contains(flagName)
        End Get
        Set(value As Boolean)
            If value Then
                _flags.Add(flagName)
            Else
                _flags.Remove(flagName)
            End If
        End Set
    End Property
    Private ReadOnly _counters As New Dictionary(Of String, Integer)
    Friend Property Counter(counterName As String) As Integer
        Get
            If _counters.ContainsKey(counterName) Then
                Return _counters(counterName)
            End If
            Return 0
        End Get
        Set(value As Integer)
            _counters(counterName) = value
        End Set
    End Property
End Module
