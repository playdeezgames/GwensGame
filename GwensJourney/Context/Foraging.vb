Friend Class Foraging
    Implements IForaging
    Private ReadOnly _context As IContext
    Sub New(context As IContext)
        _context = context
    End Sub
    Public Sub GenerateDifficulty() Implements IForaging.GenerateDifficulty
        _context.Counter(CounterNames.ForagingDifficulty) = D6() + D6() + D6() + D6()
    End Sub
    Public Sub GenerateAbundance() Implements IForaging.GenerateAbundance
        _context.Counter(CounterNames.ForagingAbundance) = D4() + D4()
    End Sub
    Public Sub Reset() Implements IForaging.Reset
        GenerateDifficulty()
        GenerateAbundance()
    End Sub

    Public Function DifficultyName() As String Implements IForaging.DifficultyName
        Select Case _context.Counter(CounterNames.ForagingDifficulty)
            Case Is < 5
                Return "[aqua]Very easy[/]"
            Case 5 To 9
                Return "[lime]Easy[/]"
            Case 10 To 14
                Return "[yellow]Challenging[/]"
            Case 15 To 19
                Return "[red]Difficult[/]"
            Case Is > 19
                Return "[maroon]Impossible[/]"
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function CanForage() As Boolean Implements IForaging.CanForage
        Return _context.Counter(CounterNames.ForagingAbundance) > 0
    End Function
End Class
