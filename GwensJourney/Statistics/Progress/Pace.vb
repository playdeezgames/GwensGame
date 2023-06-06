Friend Class Pace
    Implements IPace
    Private _context As Context
    Public Sub New(context As Context)
        Me._context = context
    End Sub
    Private Const INITIAL_PACE As Integer = 3
    Friend Const MinimumPace = 1
    Friend Const MaximumPace = 5
    Private ReadOnly paceNames As IReadOnlyDictionary(Of Integer, String) =
        New Dictionary(Of Integer, String) From
        {
            {1, "[green]Slowest[/]"},
            {2, "[aqua]Slow[/]"},
            {3, "[blue]Medium[/]"},
            {4, "[purple]Fast[/]"},
            {5, "[fuchsia]Fastest[/]"}
        }
    Friend Function Read() As Integer Implements IPace.Read
        Return _context.Counter(CounterNames.Pace)
    End Function
    Friend Sub Write(pace As Integer) Implements IPace.Write
        _context.Counter(CounterNames.Pace) = Math.Clamp(pace, MinimumPace, MaximumPace)
    End Sub
    Friend Function Name() As String Implements IPace.Name
        Return paceNames(_context.Counter(CounterNames.Pace))
    End Function
    Friend Function Name(index As Integer) As String Implements IPace.Name
        Return paceNames(Math.Clamp(index, MinimumPace, MaximumPace))
    End Function
    Friend Sub Reset() Implements IPace.Reset
        Write(INITIAL_PACE)
    End Sub
End Class
