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
End Class
